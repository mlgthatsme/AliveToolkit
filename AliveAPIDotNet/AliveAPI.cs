using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace AliveAPIDotNet
{
    public class AlivePlugin
    {
        static DebugWindow window;
        public static void Initialize()
        {
            Application.EnableVisualStyles();

            Console.WriteLine(".Net lib is init!");

            window = new AliveAPIDotNet.DebugWindow();
            window.Show();
        }
    }

    public class MemAllocEventArgs : EventArgs
    {
        public MemoryAllocation Allocation;
    }

    public static class AliveAPI
    {
        const string DLLFileName = "TestHook.dll";

        public static event EventHandler<MemAllocEventArgs> OnMemoryAllocate;
        public static void FireOnMemoryAllocate(MemoryAllocation allocation)
        {
            OnMemoryAllocate?.Invoke(null, new MemAllocEventArgs() { Allocation = allocation });
        }

        public static event EventHandler<EventArgs> GameTick;
        public static void FireOnGameTick()
        {
            GameTick?.Invoke(null, null);
        }

        [DllImport(DLLFileName, EntryPoint = "Ae_LoadResource")]
        static extern void Ae_LoadResource(string file);

        public static void LoadResource(string file)
        {
            Console.WriteLine("Loading " + file);
            Ae_LoadResource(file);
        }

        [DllImport(DLLFileName, EntryPoint = "Ae_PlaySound")]
        public static extern void PlaySound(int id, int volume, float pitch, int a4);

        [DllImport(DLLFileName, EntryPoint = "UpdateAllocationList")]
        public static extern void UpdateAllocationList();

        [DllImport(DLLFileName, EntryPoint = "Ae_CreateObject")]
        static extern IntPtr Ae_CreateObject(int id, IntPtr param);

        public static AliveObject CreateObject(int id, short x, short y, short width, short height, byte[] param)
        {
            MemoryStream str = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(str);
            writer.Write((short)0);
            writer.Write((short)0);
            writer.Write((short)id);
            writer.Write((short)0);
            writer.Write(x);
            writer.Write(y);
            writer.Write((short)(x + width));
            writer.Write((short)(y + height));
            writer.Write(param);

            byte[] finalData = str.ToArray();
            str.Close();

            IntPtr mem = Marshal.AllocHGlobal(finalData.Length);
            Marshal.Copy(finalData, 0, mem, finalData.Length);
            var o =  new AliveAPIDotNet.AliveObject(Ae_CreateObject(id, mem));
            Marshal.FreeHGlobal(mem);
            return o;
        }

        public static List<MemoryAllocation> MemoryAllocations = new List<MemoryAllocation>();

        public static AliveObject GetPlayerObject()
        {
            IntPtr playerAddr = Marshal.ReadIntPtr(new IntPtr(0x5C1B8C));
            if (playerAddr.ToInt32() == 0)
                return null;

            return new AliveObject(playerAddr);
        }

        public static void SetPlayerObject(AliveObject obj)
        {
            Marshal.WriteIntPtr(new IntPtr(0x5C1B8C), obj.Pointer);
        }

        public static byte[] GetSwitchStates()
        {
            IntPtr switchAddr = new IntPtr(0x005C1A28);
            byte[] switchStates = new byte[256];
            Marshal.Copy(switchAddr, switchStates, 0, 256);
            return switchStates;
        }

        public static AliveObjectList ObjectList = new AliveObjectList(new IntPtr(0x00BB47C4));
        public static AliveObjectList ObjectListActive = new AliveObjectList(new IntPtr(0x005C1124));
    }
}
