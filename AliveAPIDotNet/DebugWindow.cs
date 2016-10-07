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

namespace AliveAPIDotNet
{
    public partial class DebugWindow : Form
    {
        public DebugWindow()
        {
            InitializeComponent();

            AliveAPI.OnMemoryAllocate += AliveAPI_OnMemoryAllocate;

            foreach(var v in GameConfiguration.Instance.SpawnEntries.OrderBy(x=>x.PathID))
            {
                listBox2.Items.Add(v);
            }

            if (GameConfiguration.Instance.GameType != GameTypes.Exoddus)
            {
                toolStripButtonRecord.Dispose();
                toolStripSeparator5.Dispose();
                toolStripButton4.Dispose();
                toolStripButton3.Dispose();
            }

            AliveAPI.GameTick += AliveAPI_GameTick;
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

        public static List<RaycastHit> mRaycastHits = new List<RaycastHit>();

        float Distance(int x1, int y1, int x2, int y2)
        {
            return (float)Math.Sqrt(Math.Pow(Math.Abs(x1 - x2), 2) + Math.Pow(Math.Abs(y1 - y2), 2));
        }

        AliveObject dragObject = null;
        public static bool freezeGame = false;
        public static bool stepFrame = false;

        float mouseXPrev = 0;
        float mouseYPrev = 0;

        private void AliveAPI_GameTick(object sender, EventArgs e)
        {
            while(freezeGame)
            {
                if (stepFrame)
                {
                    stepFrame = false;
                    break;
                }
            }

            this.Invoke(new MethodInvoker(delegate { panelCurrentScreen.Refresh(); }));
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
                        foreach (AliveObject obj in AliveAPI.ObjectList.AsAliveObjects)
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
                    AliveAPI.LoadResource("ABEBLOW.BAN");
                    AliveAPI.LoadResource("DOGBLOW.BAN");
                    AliveAPI.LoadResource("SHADOW.BAN");

                    foreach (string b in s.spawnEntry.Resources)
                    {
                        AliveAPI.LoadResource(b);
                    }

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
        SpawnEntry SelectedSpawnObject { get { return (SpawnEntry)listBox2.SelectedItem; } }

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

        private void timer1_Tick(object sender, EventArgs e)
        {
            AliveObject[] objects = AliveAPI.ObjectList.AsAliveObjects;

            object selection = listBox1.SelectedItem;
            listBox1.Items.Clear();
            listBox1.Items.AddRange(objects);
            if (selection != null && objects.Contains(selection))
                listBox1.SelectedItem = selection;

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
                ObjectEditor objEditor = new AliveAPIDotNet.ObjectEditor((AliveObject)listBox1.SelectedItem);
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

            labelTotal.Text = "Total: " + (total / 1024) + " kb";
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
                AliveAPI.ObjectList.Add(clone);
            }
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedObject != null)
            {
                SelectedObject.Destroy();
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

            pathEntries = AliveAPI.PathData.PathEntries;

            foreach (var o in pathEntries)
            {
                listBoxPath.Items.Add(o);
            }

            panelCurrentScreen.Refresh();
        }

        PathLineObject[] pathEntries = null;

        private void panelCurrentScreen_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.Gray);
            if (pathEntries != null)
            {
                e.Graphics.ScaleTransform(panelCurrentScreen.Width / 368.0f, panelCurrentScreen.Height / 240.0f);
                e.Graphics.TranslateTransform(-AliveAPI.CameraOffsetX, -AliveAPI.CameraOffsetY);
                
                foreach (var l in pathEntries)
                {
                    Point p1 = new Point(l.X1, l.Y1);
                    Point p2 = new Point(l.X2, l.Y2);

                    e.Graphics.DrawLine(Pens.Red, p1, p2);
                }

                foreach(var o in AliveAPI.ObjectList.AsAliveObjects)
                {
                    e.Graphics.FillEllipse(Brushes.Black, new RectangleF(o.PositionX - 4, o.PositionY - 8, 8, 8));
                }

                lock (mRaycastHits)
                {
                    foreach (var l in mRaycastHits)
                    {
                        Point p1 = new Point(l.X1, l.Y1);
                        Point p2 = new Point(l.X2, l.Y2);

                        if (l.Hit)
                        {
                            p2 = new Point(l.CX, l.CY);
                        }

                        e.Graphics.DrawLine((l.Hit) ? Pens.Green : Pens.Blue, p1, p2);

                        e.Graphics.FillEllipse(Brushes.Yellow, new RectangleF(l.CX - 2, l.CY - 2, 4, 4));
                        e.Graphics.FillEllipse(Brushes.Brown, new RectangleF(l.X1 - 2, l.Y1 - 2, 4, 4));

                        if (l.Hit)
                        {
                            PathLineObject line = new PathLineObject(new IntPtr(l.CollidedObject));
                            e.Graphics.DrawLine(Pens.Orange, line.X1, line.Y1, line.X2, line.Y2);
                        }

                        if (checkBoxRenderMode.Checked)
                            e.Graphics.DrawString(l.Mode.ToString(), new Font(FontFamily.GenericSansSerif, 5.0f), Brushes.White, new PointF((l.X1 + l.X2) / 2, (l.Y1 + l.Y2) / 2), new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
                        //if (l.Hit)
                        //{
                        //    e.Graphics.DrawString(l.CollidedObject.ToString(), Font, Brushes.White, new PointF((l.X1 + l.CX) / 2, (l.Y1 + l.CY) / 2), new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
                        //}
                    }
                }
            }
            else
            {
                e.Graphics.DrawString("Refresh", Font, Brushes.Black, PointF.Empty);
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            freezeGame = !freezeGame;

            if (freezeGame)
                Marshal.WriteByte(new IntPtr(0x005CA4D1), 1);
            else
                Marshal.WriteByte(new IntPtr(0x005CA4D1), 0);
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
    }
}