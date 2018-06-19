using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliveAPIDotNet.Databases
{
    public class VTableDB
    {
        Dictionary<int, VTable> mVTables = new Dictionary<int, VTable>();

        public void ClearEntries()
        {
            mVTables.Clear();
        }

        public string GetVTableFriendlyName(int address)
        {
            if (mVTables.ContainsKey(address))
                return mVTables[address].FriendlyName;

            return address.ToString("X8");
        }

        public void LoadFromString(string data)
        {
            string[] split = data.Split('\n');

            foreach(string s in split)
            {
                // Comment
                if (s.StartsWith("#"))
                    continue;

                string[] paramSplit = s.Split(new char[] { '=' }, 2, StringSplitOptions.None);

                int address = int.Parse(paramSplit[0].Substring(2, paramSplit[0].Length - 2), System.Globalization.NumberStyles.HexNumber);

                string[] nameSplit = paramSplit[1].Split(',');

                mVTables.Add(address, new VTable() { Address = address, Name = nameSplit[0], FriendlyName = nameSplit[1] });
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
