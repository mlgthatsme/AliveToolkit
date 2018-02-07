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
using AliveAPIDotNet.Input;

namespace AliveAPIDotNet
{
    public class AlivePlugin
    {
        static DebugWindow window;
        public static void Initialize(GameTypes gameType)
        {
            GameConfiguration.Instance = new GameConfiguration();

            GameConfiguration.Instance.GameType = gameType;

            if (gameType == GameTypes.Oddysee)
            {
                GameConfiguration.Instance.HeroTypeID = 43;

                GameConfiguration.Instance.AddressObjectList = 0x009F2DF0;
                GameConfiguration.Instance.AddressScreenInfo = 0x004FF7C8;
                GameConfiguration.Instance.AddressPlayerPointer = 0x0050767C;
                GameConfiguration.Instance.AddressPathData = 0x504C6C;
                GameConfiguration.Instance.AddressSwitches = 0x00505568;

                GameConfiguration.Instance.OffsetObjectPositionX = 0xA8;
                GameConfiguration.Instance.OffsetObjectPositionY = 0xAC;
                GameConfiguration.Instance.OffsetObjectVelocityX = 0xB4;
                GameConfiguration.Instance.OffsetObjectVelocityY = 0xB8;
                GameConfiguration.Instance.OffsetObjectScale = 0xBC;
                GameConfiguration.Instance.OffsetObjectCurrentFloor = 0xF4;
                GameConfiguration.Instance.OffsetObjectAliveState = 0xFC;

                List<SpawnEntry> spawnEntries = new List<AliveAPIDotNet.SpawnEntry>();
                for(int i = 0; i <= 115;i++)
                {
                    spawnEntries.Add(new AliveAPIDotNet.SpawnEntry(i, "Object " + i.ToString(), null));
                }

                GameConfiguration.Instance.SpawnEntries = spawnEntries.ToArray();
            }

            Application.EnableVisualStyles();

            Console.WriteLine("API Loaded for game: " + gameType.ToString());

            AliveAPI.InitializeAPI();
            DebugHelpers.OnScreenRenderManager.Init();

            Thread thread = new Thread(new ThreadStart(delegate
            {
                window = new AliveAPIDotNet.DebugWindow();
                Application.Run(window);
                
            }));
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }
    }

    public class MemAllocEventArgs : EventArgs
    {
        public MemoryAllocation Allocation;
    }

    public class InputEventArgs : EventArgs
    {
        public int Index;
        public InputPad Pad { get { return AliveAPI.Input.Pads[Index]; }}
    }

    public struct RaycastHit
    {
        public bool Hit;
        public int X1;
        public int Y1;
        public int X2;
        public int Y2;
        public int CX;
        public int CY;
        public int CollidedObject;
        public int Mode;
    }

    public static class AliveAPI
    {
        const string DLLFileName = "TestHook.dll";

        public static AliveObjectList ObjectList;
        public static AliveObjectList ObjectListActive;
        public static PathObjectList PathData;
        public static LevelEntryList Levels;
        public static IntPtr ScreenHdc;
        public static InputObject Input;
        public static bool MusicEnabled = false;

        public static List<RaycastHit> RaycastHits = new List<RaycastHit>();

        public static void InitializeAPI()
        {
            ObjectList = new AliveObjectList(new IntPtr(GameConfiguration.Instance.AddressObjectList), true);
            ObjectListActive = new AliveObjectList(new IntPtr(GameConfiguration.Instance.AddressObjectListActive), true);
            PathData = new PathObjectList(new IntPtr(GameConfiguration.Instance.AddressPathData));
            Levels = new LevelEntryList(new IntPtr(GameConfiguration.Instance.AddressLevelConfigs));
            Input = new InputObject(new IntPtr(GameConfiguration.Instance.AddressInputObject));
        }

        public static event EventHandler<MemAllocEventArgs> OnMemoryAllocate;
        public static void FireOnMemoryAllocate(MemoryAllocation allocation)
        {
            OnMemoryAllocate?.Invoke(null, new MemAllocEventArgs() { Allocation = allocation });
        }

        public static event EventHandler<EventArgs> OnDebugDraw;
        public static void FireOnDebugDraw()
        {
            OnDebugDraw?.Invoke(null, null);
        }

        public static event EventHandler<InputEventArgs> OnInput;
        public static void FireOnInput(int index)
        {
            OnInput?.Invoke(null, new InputEventArgs() { Index = index });
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

        [DllImport(DLLFileName, EntryPoint = "Lazors")]
        public static extern void Lazors();

        [DllImport(DLLFileName, EntryPoint = "UpdateAllocationList")]
        public static extern void UpdateAllocationList();

        [DllImport(DLLFileName, EntryPoint = "Ae_CreateObject")]
        static extern IntPtr Ae_CreateObject(int id, IntPtr param);

        [DllImport(DLLFileName, EntryPoint = "Ae_QuikLoad")]
        static extern void Ae_QuikLoad();

        [DllImport(DLLFileName, EntryPoint = "Ae_QuikSave")]
        static extern IntPtr Ae_QuikSave();

        [DllImport(DLLFileName, EntryPoint = "Ae_SetMusic")]
        static extern IntPtr Ae_SetMusic(int song, IntPtr obj, short unknown1, byte unknown2);

        public static void SetMusic(int song, AliveObject obj)
        {
            Ae_SetMusic(song, obj.mAddress, 0, 0);
        }

        public static QuikSave QuikSave()
        {
            IntPtr quickSavePtr = Ae_QuikSave();
            byte[] saveData = new byte[8192];
            Marshal.Copy(quickSavePtr, saveData, 0, saveData.Length);
            return new QuikSave(saveData);
        }

        public static void QuikLoad(QuikSave save)
        {
            Marshal.Copy(save.Data, 0, new IntPtr(0x00BAF7F8), save.Data.Length);
            Ae_QuikLoad();
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
            IntPtr playerAddr = Marshal.ReadIntPtr(new IntPtr(GameConfiguration.Instance.AddressPlayerPointer));
            if (playerAddr.ToInt32() == 0)
                return null;

            return new AliveObject(playerAddr);
        }

        public static void SetPlayerObject(AliveObject obj)
        {
            Marshal.WriteIntPtr(new IntPtr(GameConfiguration.Instance.AddressPlayerPointer), obj.Pointer);
        }

        public static byte[] GetSwitchStates()
        {
            IntPtr switchAddr = new IntPtr(GameConfiguration.Instance.AddressSwitches);
            byte[] switchStates = new byte[256];
            Marshal.Copy(switchAddr, switchStates, 0, 256);
            return switchStates;
        }

        public static int gnFrame
        {
            get
            {
                return Marshal.ReadInt32(new IntPtr(GameConfiguration.Instance.AddressGNFrame));
            }
            set
            {
                Marshal.WriteInt32(new IntPtr(GameConfiguration.Instance.AddressGNFrame), value);
            }
        }

        public static int RandomSeed
        {
            get
            {
                return Marshal.ReadInt32(new IntPtr(GameConfiguration.Instance.AddressRandomSeed));
            }
            set
            {
                Marshal.WriteInt32(new IntPtr(GameConfiguration.Instance.AddressRandomSeed), value);
            }
        }

        public static int CurrentLevel
        {
            get
            {
                return Marshal.ReadInt16(new IntPtr(GameConfiguration.Instance.AddressLevelPathCam));
            }
        }

        public static int CurrentPath
        {
            get
            {
                return Marshal.ReadInt16(new IntPtr(GameConfiguration.Instance.AddressLevelPathCam + 2));
            }
        }

        public static int CurrentCam
        {
            get
            {
                return Marshal.ReadInt16(new IntPtr(GameConfiguration.Instance.AddressLevelPathCam + 4));
            }
        }

        public static float CameraOffsetX
        {
            get
            {
                if (GameConfiguration.Instance.GameType == GameTypes.Oddysee)
                    return UnmanagedObject.HalfFloatToFloat(Marshal.ReadInt32(new IntPtr(Marshal.ReadInt32(new IntPtr(Marshal.ReadInt32(new IntPtr(GameConfiguration.Instance.AddressScreenInfo)) + 16))))) - (368 / 2);

                return UnmanagedObject.HalfFloatToFloat(Marshal.ReadInt32(new IntPtr(Marshal.ReadInt32(new IntPtr(Marshal.ReadInt32(new IntPtr(GameConfiguration.Instance.AddressScreenInfo)) + 32)))));
            }
        }

        public static float CameraOffsetY
        {
            get
            {
                if (GameConfiguration.Instance.GameType == GameTypes.Oddysee)
                    return UnmanagedObject.HalfFloatToFloat(Marshal.ReadInt32(new IntPtr(Marshal.ReadInt32(new IntPtr(Marshal.ReadInt32(new IntPtr(GameConfiguration.Instance.AddressScreenInfo)) + 16)) + 4))) - (240 / 2);

                return UnmanagedObject.HalfFloatToFloat(Marshal.ReadInt32(new IntPtr(Marshal.ReadInt32(new IntPtr(Marshal.ReadInt32(new IntPtr(GameConfiguration.Instance.AddressScreenInfo)) + 32)) + 4)));
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
