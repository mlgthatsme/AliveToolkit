using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace AliveAPIDotNet
{
    public class AlivePlugin
    {
        static DebugWindow window;
        public static void Initialize()
        {
            Application.EnableVisualStyles();

            Console.WriteLine("API Loaded");

            Thread thread = new Thread(new ThreadStart(delegate
            {
                window = new AliveAPIDotNet.DebugWindow();
                Application.Run(window);
            }));
            thread.SetApartmentState(ApartmentState.MTA);
            thread.Start();
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
            MouseLeftPressed = (GetAsyncKeyState(1) & 1) == 1;
            MouseRightPressed = (GetAsyncKeyState(2) & 1) == 1;
            MouseLeftDown = (GetAsyncKeyState(1) & 0x8000) == 0x8000;
            MouseRightDown = (GetAsyncKeyState(2) & 0x8000) == 0x8000;
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

        [DllImport(DLLFileName, EntryPoint = "Ae_QuikLoad")]
        static extern void Ae_QuikLoad(byte[] saveData);

        [DllImport(DLLFileName, EntryPoint = "Ae_QuikSave")]
        static extern IntPtr Ae_QuikSave();

        public static QuikSave QuikSave()
        {
            IntPtr quickSavePtr = Ae_QuikSave();
            byte[] saveData = new byte[8192];
            Marshal.Copy(quickSavePtr, saveData, 0, saveData.Length);
            return new QuikSave(saveData);
        }

        public static void QuikLoad(QuikSave save)
        {
            Ae_QuikLoad(save.Data);
        }

        public static AliveObject CreateObject(int id, short x, short y, short width, short height, byte[] param)
        {
            MemoryStream str = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(str);
            writer.Write((short)0);
            writer.Write((short)0);
            writer.Write((short)id);
            writer.Write((short)0);
            writer.Write((short)(x - (width / 2)));
            writer.Write(y);
            writer.Write((short)(x + (width / 2)));
            writer.Write((short)(y + height));
            writer.Write(param);

            byte[] finalData = str.ToArray();
            str.Close();

            IntPtr mem = Marshal.AllocHGlobal(finalData.Length);
            Marshal.Copy(finalData, 0, mem, finalData.Length);
            var o = new AliveAPIDotNet.AliveObject(Ae_CreateObject(id, mem));
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

        public static AliveObjectList ObjectList = new AliveObjectList(new IntPtr(0x00BB47C4), true);
        public static AliveObjectList ObjectListActive = new AliveObjectList(new IntPtr(0x005C1124), true);

        public static PathObjectList PathData = new PathObjectList(new IntPtr(0x005C1128));

        public static float CameraOffsetX
        {
            get
            {
                return AliveObject.HalfFloatToFloat(Marshal.ReadInt32(new IntPtr(Marshal.ReadInt32(new IntPtr(Marshal.ReadInt32(new IntPtr(0x5BB5F4)) + 32)))));
            }
        }

        public static float CameraOffsetY
        {
            get
            {
                return AliveObject.HalfFloatToFloat(Marshal.ReadInt32(new IntPtr(Marshal.ReadInt32(new IntPtr(Marshal.ReadInt32(new IntPtr(0x5BB5F4)) + 32)) + 4)));
            }
        }

        [DllImport("user32.dll")]
        static extern short GetAsyncKeyState(int vKey);

        [DllImport("user32.dll")]
        static extern bool ScreenToClient(IntPtr hWnd, ref Point lpPoint);

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        static extern IntPtr GetActiveWindow();

        public static bool IsGameWindowActive { get { return GetActiveWindow() == GameWindowHWND; } }

        static IntPtr hwnd = IntPtr.Zero;
        public static IntPtr GameWindowHWND
        {
            get
            {
                if (hwnd == IntPtr.Zero)
                    hwnd = FindWindow("ABE_WINCLASS", null);

                return hwnd;
            }
        }

        public static float MouseX
        {
            get
            {
                Point p = Cursor.Position;
                ScreenToClient(GameWindowHWND, ref p);
                return p.X / (float)GetGameWindowSize.Width;
            }
        }
        public static float MouseY
        {
            get
            {
                Point p = Cursor.Position;
                ScreenToClient(GameWindowHWND, ref p);
                return p.Y / (float)GetGameWindowSize.Height;
            }
        }

        public static Size GetGameWindowSize
        {
            get
            {
                Win32Native.Rect rect = new Win32Native.Rect();
                Win32Native.GetWindowRect(GameWindowHWND, ref rect);
                return new Size(rect.Right - rect.Left, rect.Bottom - rect.Top);
            }
        }

        public static bool MouseLeftPressed { get; private set; }
        public static bool MouseRightPressed { get; private set; }

        public static bool MouseLeftDown { get; private set; }
        public static bool MouseRightDown { get; private set; }
    }
}
