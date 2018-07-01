using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Interop;
using AliveAPIDotNet.Forms;
using AliveAPIDotNet.Unmanaged;
using Binarysharp.MemoryManagement;
using System.IO;
using Binarysharp.MemoryManagement.Assembly.CallingConvention;
using AliveAPIDotNet.Helpers;

namespace AliveAPIDotNet.Forms
{
    public partial class DebugWindow : Form
    {
        public DebugWindow()
        {
            InitializeComponent();

            AliveAPI.OnMemoryAllocate += AliveAPI_OnMemoryAllocate;

            foreach(var v in GameConfiguration.Instance.SpawnEntries.OrderBy(x=>x.PathID))
            {
                spawnListBox.Items.Add(v);
            }

            if (GameConfiguration.Instance.GameType != GameTypes.Exoddus)
            {
                toolStripButtonRecord.Dispose();
                toolStripSeparator5.Dispose();
                toolStripButtonLoadState.Dispose();
                toolStripButtonSaveState.Dispose();
            }

            AliveAPI.GameTick += AliveAPI_GameTick;
            AliveAPI.OnDebugDraw += AliveAPI_OnDebugDraw;

            Application.EnterThreadModal += delegate { modalMode = true; };
            Application.LeaveThreadModal += delegate { modalMode = false; };

            mKeyboardHook.KeyPressed += MKeyboardHook_KeyPressed;
            mKeyboardHook.RegisterHotKey(0, Keys.End);
            
        }

        private void MKeyboardHook_KeyPressed(object sender, KeyPressedEventArgs e)
        {
            if (e.Key == Keys.End)
                ToggleFrameFreeze();
        }

        private void AliveAPI_OnDebugDraw(object sender, EventArgs e)
        {
            //graphics.DrawLine(Pens.Red, 0, 0, 150, 150);

            //IntPtr blockPtr = new IntPtr(0x5B86C8);

            //Font f = new Font(FontFamily.GenericMonospace, 8.0f);

            //if (blockPtr!= IntPtr.Zero)
            //{
            //    for (int i = 0; i < 300;i++)
            //    {
            //        IntPtr blockOffset = blockPtr + (40 * i);

            //        int x = Marshal.ReadInt16(blockOffset + 0xC);
            //        int y = Marshal.ReadInt16(blockOffset + 0xE);

            //        //int u1 = (int)((((UInt16)Marshal.ReadInt16(blockOffset + 0x10)) / (float)UInt16.MaxValue) * 255);

            //        // int xOffset = Marshal.ReadInt16(blockOffset + 23);
            //        int xOffset = Marshal.ReadByte(blockOffset + 40);

            //        int width = Marshal.ReadInt16(blockOffset + 0x14);
            //        int height = Marshal.ReadInt16(blockOffset + 0x16);

            //        graphics.DrawRectangle(new Pen(Color.FromArgb(255,255,0,0)), new Rectangle(x, y, width-1, height-1));
            //        graphics.DrawString(xOffset.ToString(), f, Brushes.LawnGreen, new Point(x, y));
            //    }

            //    graphics.DrawString("Blocks Enabled", Font, Brushes.Red, new PointF(0, 0));
            //}
        }

        

        float Distance(int x1, int y1, int x2, int y2)
        {
            return (float)Math.Sqrt(Math.Pow(Math.Abs(x1 - x2), 2) + Math.Pow(Math.Abs(y1 - y2), 2));
        }

        AliveObject dragObject = null;
        public static bool freezeGame = false;
        public static bool stepFrame = false;
        VRamWindow vRamWindow = new VRamWindow();
        FunctionCallerWindow mFunctionCallerWindow = new FunctionCallerWindow();
        bool modalMode = false;
        Random rand = new Random();
        KeyboardHook mKeyboardHook = new KeyboardHook();

        float mouseXPrev = 0;
        float mouseYPrev = 0;

        int t = 0;

        protected override void SetVisibleCore(bool value)
        {
            base.SetVisibleCore(AliveAPI.DoesCommandArgExist("-no_ui") ? false : value);
        }

        private void AliveAPI_GameTick(object sender, EventArgs e)
        {

            RenderLoop();

            if (modalMode)
                return;

            while(freezeGame)
            {
                if (stepFrame)
                {
                    stepFrame = false;
                    break;
                }
            }

            // Cheats
            if (checkBoxCheatCrazyMuds.Checked)
            {
                foreach(var o in AliveAPI.ObjectListBaseObjects.AsAliveObjects)
                {
                    if (o.ObjectID == 110)
                    {
                        o.SafeWriteByte(o.mAddress + 0x180, 6);
                    }
                }
            }

            t++;
            
            if (checkBoxCheatSlowTick.Checked)
            {
                if (t % 2 == 0)
                    AliveAPI.gnFrame -= 1;
            }

            if (checkBoxCheatInstaChant.Checked)
            {
                AliveAPI.gnFrame += 10;
            }

            if (IsHandleCreated)
            {
                this.Invoke(new MethodInvoker(delegate { panelCurrentScreen.Refresh(); }));
            }

            lock (spawnQueues)
            {
                int gameX = (int)AliveAPI.CameraOffsetX + (int)(AliveAPI.MouseX * 374);
                int gameY = (int)AliveAPI.CameraOffsetY + (int)(AliveAPI.MouseY * 254);

                if (AliveAPI.IsGameWindowActive && AliveAPI.MouseRightPressed)
                {
                    if (SelectedSpawnObject != null)
                        spawnQueues.Add(new SpawnQueue() { spawnEntry = SelectedSpawnObject, parameters = SelectedSpawnObject.ParamsData, PositionX = (short)gameX, PositionY = (short)gameY });
                }
                if (AliveAPI.IsGameWindowActive && AliveAPI.MouseLeftDown)
                {
                    if (AliveAPI.MouseLeftPressed)
                    {
                        foreach (AliveObject obj in AliveAPI.ObjectListBaseObjects.AsAliveObjects)
                        {
                            float dist = Distance(gameX, gameY, (int)obj.PositionX, (int)obj.PositionY);
                            if (dist < 30)
                            {
                                dragObject = obj;
                                break;
                            }
                        }
                    }
                    else
                    {
                        if (dragObject != null)
                        {
                            dragObject.PositionX = gameX;
                            dragObject.PositionY = gameY;
                        }
                    }
                }
                else
                {
                    if (dragObject != null)
                    {
                        float velX = (AliveAPI.MouseX - mouseXPrev) * 200;
                        float velY = (AliveAPI.MouseY - mouseYPrev) * 200;
                        dragObject.FloorCollider = null;
                        if (dragObject.ObjectID == GameConfiguration.Instance.HeroTypeID)
                            dragObject.AliveState = 85;

                        dragObject.VelocityX = velX;
                        dragObject.VelocityY = velY;
                    }

                    dragObject = null;
                }
                foreach (var s in spawnQueues)
                {
                    // Load commonly shared resources
                    //AliveAPI.LoadResource("ABEBLOW.BAN");
                    //AliveAPI.LoadResource("DOGBLOW.BAN");
                    //AliveAPI.LoadResource("SHADOW.BAN");

                    //foreach (string b in s.spawnEntry.Resources)
                    //{
                    //    AliveAPI.LoadResource(b);
                    //}

                    AliveObject obj = AliveAPI.CreateObject(s.spawnEntry.PathID, s.PositionX, s.PositionY, (short)s.spawnEntry.Width, (short)s.spawnEntry.Height, (s.parameters == null) ? new byte[150] : s.parameters);
                }

                spawnQueues.Clear();
            }

            mouseXPrev = AliveAPI.MouseX;
            mouseYPrev = AliveAPI.MouseY;
        }

        List<SpawnQueue> spawnQueues = new List<SpawnQueue>();

        struct SpawnQueue
        {
            public SpawnEntry spawnEntry;
            public short PositionX;
            public short PositionY;
            public byte[] parameters;
        }

        AliveObject SelectedObject { get { return (AliveObject)listBox1.SelectedItem; } }
        SpawnEntry SelectedSpawnObject { get {

                if (InvokeRequired)
                {
                    SpawnEntry se = null;
                    Invoke(new MethodInvoker(delegate {
                        se = (SpawnEntry)spawnListBox.SelectedItem;
                        
                    }));
                    return se;
                }
                else
                    return (SpawnEntry)spawnListBox.SelectedItem;
            }
        }

        SoundPlayer mSoundPlayerWindow = new SoundPlayer();

        static string GetClipboardText()
        {
            string res = "";
            Thread staThread = new Thread(x =>
            {
                res = Clipboard.GetText();
            });
            staThread.SetApartmentState(ApartmentState.STA);
            staThread.Start();
            staThread.Join();
            return res;
        }

        static void SetClipboardText(string text)
        {
            Thread staThread = new Thread(x =>
            {
                Clipboard.SetText(text);
            });
            staThread.SetApartmentState(ApartmentState.STA);
            staThread.Start();
            staThread.Join();
        }

        private void AliveAPI_OnMemoryAllocate(object sender, MemAllocEventArgs e)
        {
            textBoxMemoryLog.AppendText(string.Format("[{0}] allocated {1} bytes at {2}", e.Allocation.Caller.ToString("X8"), e.Allocation.Size, e.Allocation.Address.ToString("X8")) + "\r\n");
        }

        void Log(string text)
        {
            textBox1.AppendText(text + "\r\n");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                AliveAPI.PlaySound((int)numericUpDown1.Value, (int)numericUpDown2.Value, 0, 0x10000);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
        }

        byte[] oldSwitchStates = new byte[256];

        List<int> unknownVTablesList = new List<int>();

        private void timer1_Tick(object sender, EventArgs e)
        {
            AliveObject[] objects = AliveAPI.ObjectListBaseObjects.AsAliveObjects;

            object selection = listBox1.SelectedItem;
            listBox1.Items.Clear();
            listBox1.Items.AddRange(objects);
            if (selection != null && objects.Contains(selection))
                listBox1.SelectedItem = selection;

            foreach(var o in objects)
            {
                int vtbl = (int)o.VTable;
                if (vtbl != 0 && !AliveAPI.VTableDatabase.DoesVTableDefinitionExist(vtbl) && !unknownVTablesList.Contains(vtbl))
                {
                    Log($"Unknown VTABLE!!: 0x{vtbl.ToString("X")}");
                    unknownVTablesList.Add(vtbl);
                }
            }

            byte[] newSwitchStates = AliveAPI.GetSwitchStates();
            for (int i = 0; i < 256; i++)
            {
                if (newSwitchStates[i] != oldSwitchStates[i])
                    Log(string.Format("Switch state {0} change from {1} to {2}", i, oldSwitchStates[i], newSwitchStates[i]));
            }
            oldSwitchStates = newSwitchStates;

            long total = 0;
            AliveAPI.UpdateAllocationList();
            foreach (var alloc in AliveAPI.MemoryAllocations)
            {
                total += alloc.Size;
            }

            lineGraphMemory.SetValue(total / 1024.0f, 0);
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                ObjectEditor objEditor = new ObjectEditor((AliveObject)listBox1.SelectedItem);
                objEditor.Show();
            }
        }

        private void buttonUpdateMemory_Click(object sender, EventArgs e)
        {
            long total = 0;
            AliveAPI.UpdateAllocationList();
            listMemoryAllocations.Items.Clear();
            foreach (var alloc in AliveAPI.MemoryAllocations)
            {
                listMemoryAllocations.Items.Add(alloc);
                total += alloc.Size;
            }

            labelTotalMemory.Text = "Total: " + (total / 1024) + " kb";
        }

        private void copyVTableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedObject != null)
            {
                SetClipboardText(SelectedObject.VTable.ToString("X8"));
            }
        }

        private void pasteVTableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedObject != null)
            {
                try
                {
                    SelectedObject.VTable = new IntPtr(int.Parse(GetClipboardText(), System.Globalization.NumberStyles.HexNumber | System.Globalization.NumberStyles.AllowHexSpecifier));
                }
                catch
                {
                    MessageBox.Show("Invalid Address (Hex only)");
                }
            }
        }

        private void duplicateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedObject != null)
            {
                AliveObject clone = SelectedObject.Duplicate();
                AliveAPI.ObjectListBaseObjects.Add(clone);
                AliveAPI.ObjectListDrawables.Add(clone);
            }
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedObject != null)
            {
                AliveAPI.ObjectListBaseObjects.Remove(SelectedObject.Pointer);
                AliveAPI.ObjectListDrawables.Remove(SelectedObject.Pointer);
            }
        }

        private void copyAddressToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedObject != null)
            {
                SetClipboardText(SelectedObject.Pointer.ToString("X8"));
            }
        }

        private void DebugWindow_Load(object sender, EventArgs e)
        {
            
        }

        private void buttonSpawnObject_Click(object sender, EventArgs e)
        {
            if (SelectedSpawnObject != null)
            {
                byte[] param = SelectedSpawnObject.ParamsData;

                lock(spawnQueues)
                {
                    spawnQueues.Add(new SpawnQueue() { spawnEntry = SelectedSpawnObject, parameters = param, PositionX = (short)(AliveAPI.GetPlayerObject().PositionX + 50), PositionY = (short)(AliveAPI.GetPlayerObject().PositionY - 20) });
                }
            }
        }

        private void mindControlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedObject != null)
            {
                AliveAPI.SetPlayerObject(SelectedObject);
            }
        }

        private void listBoxPath_DoubleClick(object sender, EventArgs e)
        {
            if (listBoxPath.SelectedItem != null)
            {
                new ObjectEditor((UnmanagedObject)listBoxPath.SelectedItem).Show();
            }
        }

        private void btnPathRefresh_Click(object sender, EventArgs e)
        {
            listBoxPath.Items.Clear();

            foreach (var o in AliveAPI.PathData.PathEntries)
            {
                listBoxPath.Items.Add(o);
            }

            panelCurrentScreen.Refresh();
        }

        private void panelCurrentScreen_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.Gray);
            DebugHelpers.PathRenderer.DrawPathLines(e.Graphics, panelCurrentScreen.Width, panelCurrentScreen.Height, checkBoxRenderMode.Checked);
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            ToggleFrameFreeze();
        }

        void ToggleFrameFreeze()
        {
            freezeGame = !freezeGame;
            Marshal.WriteByte(new IntPtr(0x005CA4D1), (byte)((freezeGame) ? 1 : 0));
        }

        private void btnStep_Click(object sender, EventArgs e)
        {
            freezeGame = true;
            stepFrame = true;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            AliveAPI.PathData.AddLine((short)AliveAPI.GetPlayerObject().PositionX, (short)AliveAPI.GetPlayerObject().PositionY, (short)(AliveAPI.GetPlayerObject().PositionX + 100), (short)AliveAPI.GetPlayerObject().PositionY, 0);
        }

        RecordWindow recordWindow = new RecordWindow();

        private void toolStripButtonRecord_Click(object sender, EventArgs e)
        {
            recordWindow.Show();
        }

        QuikSave quikSave;

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            quikSave = AliveAPI.QuikSave();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (quikSave != null)
            {
                AliveAPI.QuikLoad(quikSave);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AliveAPI.Lazors();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            new ObjectEditor(AliveAPI.Levels).Show();

            foreach(var l in AliveAPI.Levels.Levels)
            {
                foreach(var p in l.PathList)
                {
                    //var offset = Marshal.ReadInt32(new IntPtr(p.PathConstructorTable.Value)) + 28 + (49 * 4);
                    //Console.WriteLine(Marshal.ReadInt32(new IntPtr(p.PathConstructorTable.Value)).ToString("X"));
                    Marshal.WriteInt32(new IntPtr(0x005A80A8), 0x00403E90);
                }
            }
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            vRamWindow.Show();
        }

        private void checkBoxMusic_CheckedChanged(object sender, EventArgs e)
        {
            AliveAPI.MusicEnabled = checkBoxMusicEnabled.Checked;
        }

        private void btnSetSong_Click(object sender, EventArgs e)
        {
            AliveAPI.SetMusic((int)numSongNum.Value, AliveAPI.GetPlayerObject());
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            AliveAPI.EnableVerboseLog();
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            mSoundPlayerWindow.Show();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            DebugHelpers.PathRenderer.Enabled = checkboxPathOverlay.Checked;
        }

        private void button2_Click_2(object sender, EventArgs e)
        {
            int address = 0;
            if (textBoxPaletteAddr.Text.Contains("0x"))
                address = int.Parse(textBoxPaletteAddr.Text.Replace("0x",""), System.Globalization.NumberStyles.HexNumber);
            else
                address = int.Parse(textBoxPaletteAddr.Text);

            byte[] paletteData = new byte[(int)numericPaletteCount.Value * 2];
            Marshal.Copy(new IntPtr(address), paletteData, 0, paletteData.Length);
            paletteEditor1.SetPalette((int)numericPaletteCount.Value, paletteData);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            mFunctionCallerWindow.Show();
        }

        private void btnFactoryForceSpawn_Click(object sender, EventArgs e)
        {
            IntPtr address = new IntPtr(int.Parse(textBoxFactoryAddress.Text, System.Globalization.NumberStyles.HexNumber));

            AliveObject obj = new AliveObject(Marshal.ReadIntPtr(new IntPtr(0x005C1B8C)));

            MemoryStream str = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(str);
            int x = (int)obj.PositionX + 100;
            int y = (int)obj.PositionY;
            writer.Write((short)0);
            writer.Write((short)0);
            writer.Write((short)0); // id
            writer.Write((short)0);
            writer.Write((short)(x - (32 / 2)));
            writer.Write(y);
            writer.Write((short)(x + (32 / 2)));
            writer.Write((short)(y + 32));

            byte[] finalData = str.ToArray();

            IntPtr paramsData = Marshal.AllocHGlobal(1000);
            Marshal.Copy(finalData, 0, paramsData, finalData.Length);
            //AliveAPI.mMemorySharp.Assembly.Execute(address, Binarysharp.MemoryManagement.Assembly.CallingConvention.CallingConventions.Cdecl, new dynamic[] { paramsData, Marshal.ReadIntPtr(new IntPtr(0xBB47C0)), 0, 2 });
            AliveAPI.mMemorySharp.Assembly.Execute(address, Binarysharp.MemoryManagement.Assembly.CallingConvention.CallingConventions.Cdecl, new dynamic[] { paramsData, Marshal.ReadIntPtr(new IntPtr(0xBB47C0)), 0, 0 });
            Marshal.FreeHGlobal(paramsData);
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            IntPtr newAbeData = Marshal.AllocHGlobal(0x1BC);
            AliveAPI.mMemorySharp.Assembly.Execute(new IntPtr(0x44AD10), Binarysharp.MemoryManagement.Assembly.CallingConvention.CallingConventions.Thiscall, newAbeData, 58808, 85, 57, 55);
            AliveObject newAbe = new AliveObject(newAbeData);

            newAbe.PositionX = AliveAPI.CurrentlyControlled.PositionX + 20 + rand.Next(0, 100);
            newAbe.PositionY = AliveAPI.CurrentlyControlled.PositionY;
        }

        void RenderLoop()
        {
            while (checkBoxRenderLoop.Checked)
            {
                AliveAPI.mMemorySharp.Assembly.Execute(new IntPtr(0x464A70), CallingConventions.Thiscall, new IntPtr(0x5C1BB0));
                AliveAPI.mMemorySharp.Assembly.Execute(new IntPtr(0x494580), CallingConventions.Cdecl);

                int displayBufferSize = 0x470;
                int bufferIndex = Marshal.ReadInt16(new IntPtr(0x5C1130 + 0xC));
                IntPtr otBuffer = new IntPtr(0x5C1130 + 0x10 + (bufferIndex * displayBufferSize) + 0x70);

                foreach (var go in AliveAPI.ObjectListDrawables.AsAliveObjects)
                {
                    if (go.VTable == IntPtr.Zero)
                        continue;

                    if ((go.ObjectState & 4) == 0 && (go.ObjectState & 8) > 0)
                    {
                        AliveAPI.mMemorySharp.Assembly.Execute(Marshal.ReadIntPtr(new IntPtr((int)go.VTable + 0x8)), CallingConventions.Thiscall, go.Pointer, otBuffer);
                        bufferIndex = Marshal.ReadInt16(new IntPtr(0x5C1130 + 0xC));
                    }
                }

                // ScreenManager::40EC90(pScreenManager_5BB5F4, 0, 0, 640, 240, pScreenManager_5BB5F4->field_3A);
                

                AliveAPI.mMemorySharp.Assembly.Execute(new IntPtr(0x40E6E0), CallingConventions.Thiscall, new IntPtr(0x5BB5F4), otBuffer); // pScreenManager_5BB5F4->Render

                AliveAPI.mMemorySharp.Assembly.Execute(new IntPtr(0x4F6280), CallingConventions.Cdecl, 0); // Draw Sync
                AliveAPI.mMemorySharp.Assembly.Execute(new IntPtr(0x49C470), CallingConventions.Cdecl, 0x7A120); // sub_49C470

                AliveAPI.mMemorySharp.Assembly.Execute(new IntPtr(0x41DDF0), CallingConventions.Thiscall, new IntPtr(0x5C1130)); // PSX_Display_Render_OT_41DDF0
                AliveAPI.mMemorySharp.Assembly.Execute(new IntPtr(0x45F040), CallingConventions.Thiscall, new IntPtr(0x5BD4E0)); // Input_update_45F040

                AliveAPI.mMemorySharp.Assembly.Execute(new IntPtr(0x40EC90), CallingConventions.Thiscall, new IntPtr(0x5BB5F4), 0, 0, 640, 240, Marshal.ReadInt16(new IntPtr(Marshal.ReadInt32(new IntPtr(0x5BB5F4)) + 0x3a)));

                //Thread.Sleep(10);
            }
        }

        private void defineVTableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedObject != null)
            {
                string userVTableName = TextPopup.GetInput("Enter new VTable name", "Set new VTable definition");
                
                if (userVTableName != null)
                {
                    AliveAPI.VTableDatabase.AddUserVTable((int)SelectedObject.VTable, userVTableName);
                }
            }
        }

        private void contextMenuObject_Opening(object sender, CancelEventArgs e)
        {
            if (SelectedObject != null)
            {
                int vtable = (int)SelectedObject.VTable;
                defineVTableToolStripMenuItem.Enabled = (!AliveAPI.VTableDatabase.DoesVTableDefinitionExist(vtable)) || (AliveAPI.VTableDatabase.DoesVTableDefinitionExist(vtable) && AliveAPI.VTableDatabase.IsVTableUserDefined(vtable));
            }
            else
            {
                defineVTableToolStripMenuItem.Enabled = false;
            }
        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            DebugHelpers.ObjectRenderer.RenderObjectOverlays = checkBox1.Checked;
        }
    }
}