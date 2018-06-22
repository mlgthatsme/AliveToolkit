using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliveAPIDotNet.Databases
{
    public class VTableDB
    {
        public VTableDB()
        {
            ReloadInfs();
        }

        public void ReloadInfs()
        {
            mVTables.Clear();
            mUserVTables.Clear();

            if (File.Exists("vtables.inf"))
            {
                LoadFromString(File.ReadAllText("vtables.inf"));
            }
            if (File.Exists("user_vtables.inf"))
            {
                LoadFromString(File.ReadAllText("user_vtables.inf"), true);
            }
        }

        Dictionary<int, VTable> mVTables = new Dictionary<int, VTable>();
        Dictionary<int, VTable> mUserVTables = new Dictionary<int, VTable>();

        public void ClearEntries()
        {
            mVTables.Clear();
        }

        public string GetVTableFriendlyName(int address)
        {
            if (mVTables.ContainsKey(address))
                return mVTables[address].FriendlyName;

            if (mUserVTables.ContainsKey(address))
                return mUserVTables[address].FriendlyName;

            return address.ToString("X8");
        }

        public void AddUserVTable(int address, string name)
        {
            string ida_name = $"vTbl_{name.Replace(' ', '_')}_{address.ToString("X8")}";
            var newVTable = new VTable() { Address = address, FriendlyName = name, Name = ida_name };

            if (mUserVTables.ContainsKey(address))
                mUserVTables[address] = newVTable;
            else
                mUserVTables.Add(address, newVTable);

            RegenerateUserVTableInf();
        }

        public void RemoveUserVTable(int address)
        {
            if (mUserVTables.ContainsKey(address))
            {
                mUserVTables.Remove(address);
                RegenerateUserVTableInf();
            }
        }

        public bool IsVTableUserDefined(int address)
        {
            return mUserVTables.ContainsKey(address);
        }

        void RegenerateUserVTableInf()
        {
            StringBuilder strBuilder = new StringBuilder();

            foreach(var t in mUserVTables)
            {
                strBuilder.AppendLine($"0x{t.Value.Address.ToString("X8")}={t.Value.Name},{t.Value.FriendlyName}");
            }

            File.WriteAllText("user_vtables.inf", strBuilder.ToString());
        }

        public bool DoesVTableDefinitionExist(int address)
        {
            return mVTables.ContainsKey(address) || mUserVTables.ContainsKey(address);
        }

        public void LoadFromString(string data, bool userDefs = false)
        {
            string[] split = data.Split('\n');

            foreach(string s in split)
            {
                // Comment
                if (s.StartsWith("#"))
                    continue;

                if (s == "")
                    continue;

                string[] paramSplit = s.Split(new char[] { '=' }, 2, StringSplitOptions.None);

                int address = int.Parse(paramSplit[0].Substring(2, paramSplit[0].Length - 2), System.Globalization.NumberStyles.HexNumber);

                string[] nameSplit = paramSplit[1].Split(',');

                if (userDefs)
                {
                    if (!mVTables.ContainsKey(address))
                    {
                        mUserVTables.Add(address, new VTable() { Address = address, Name = nameSplit[0], FriendlyName = nameSplit[1] });
                    }
                }
                else
                {
                    mVTables.Add(address, new VTable() { Address = address, Name = nameSplit[0], FriendlyName = nameSplit[1] });
                }
            }
        }
    }

    public class VTable
    {
        public int Address { get; set; }
        public string Name { get; set; }
        public string FriendlyName { get; set; }
    }
}
