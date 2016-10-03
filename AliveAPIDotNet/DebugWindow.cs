using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace AliveAPIDotNet
{
    public partial class DebugWindow : Form
    {
        public DebugWindow()
        {
            InitializeComponent();

            AliveAPI.OnMemoryAllocate += AliveAPI_OnMemoryAllocate;

            foreach(var v in mSpawnEntries.OrderBy(x=>x.PathID))
            {
                listBox2.Items.Add(v);
            }

            AliveAPI.GameTick += AliveAPI_GameTick;
        }

        private void AliveAPI_GameTick(object sender, EventArgs e)
        {
            lock (spawnQueues)
            {
                foreach(var s in spawnQueues)
                {
                    // Load commonly shared resources
                    AliveAPI.LoadResource("ABEBLOW.BAN");
                    AliveAPI.LoadResource("DOGBLOW.BAN");
                    AliveAPI.LoadResource("SHADOW.BAN");

                    foreach (string b in s.spawnEntry.Resources)
                    {
                        AliveAPI.LoadResource(b);
                    }

                    AliveObject obj = AliveAPI.CreateObject(s.spawnEntry.PathID, (short)(AliveAPI.GetPlayerObject().PositionX + 37), (short)AliveAPI.GetPlayerObject().PositionY, 25, 25, s.parameters);
                }

                spawnQueues.Clear();
            }
        }

        List<SpawnQueue> spawnQueues = new List<SpawnQueue>();

        struct SpawnQueue
        {
            public SpawnEntry spawnEntry;
            public byte[] parameters;
        }

        AliveObject SelectedObject { get { return (AliveObject)listBox1.SelectedItem; } }
        SpawnEntry SelectedSpawnObject { get { return (SpawnEntry)listBox2.SelectedItem; } }

        static byte[] CreateMudParams()
        {
            byte[] p = new byte[128];
            for(int i = 0; i < p.Length;i++)
            {
                p[i] = 0;
            }


            return p;
        }

        static byte[] CreateCustomParams(params int[] offsets)
        {
            byte[] b = new byte[100];
            foreach(int i in offsets)
            {
                b[i] = 1;
            }
            return b;
        }

        SpawnEntry[] mSpawnEntries = {
            new SpawnEntry(10, "Rock Sack",null, "RTHROW.BND"),
            new SpawnEntry(15, "Slig",null, "SLGLEVER.BAN","SLGLIFT.BAN","SLGSLEEP.BAN","SLGEDGE.BAN","SLGSMASH.BAN","SLGBEAT.BAN","SLGKNFD.BAN","SLIGZ.BND","SLIG.BND", "SLGBLOW.BAN"),
            new SpawnEntry(16, "Slog",null, "SLOG.BND", "DOGKNFD.BAN"),
            new SpawnEntry(17, "Switch",null, "SWITCH1.BAN", "ABEPULL.BAN"),
            new SpawnEntry(19, "Anti Chant Orb", null, "MAIMORB.BAN","SPLINE.BAN", "SPARKS.BAN", "METAL.BAN", "EXPLO2.BAN"),
            new SpawnEntry(24, "Mine", null, "MINE.BND","EXPLODE.BND"),
            new SpawnEntry(25, "UXB", null, "UXB.BND","EXPLODE.BND"),
            new SpawnEntry(26, "Paramite", null, "PARAMITE.BND"),
            new SpawnEntry(27, "Stone", null, "ABESTONE.BAN"),
            new SpawnEntry(28, "Bird Portal", null, "PORTAL.BND", "SHRYPORT.BND"),
            new SpawnEntry(38, "Electric Wall", CreateCustomParams(4), "ELECWALL.BAN"),
            new SpawnEntry(40, "Meat Sack",null, "MTHROW.BND"),
            new SpawnEntry(41, "Scrab",null, "SCRAB.BND"),
            new SpawnEntry(49, "Mudokon", CreateMudParams(), "ABEBSIC1.BAN","ABEKNFD.BAN","ABEKNBK.BAN","ABEEDGE.BAN","MUDIDLE.BAN","MUDPAL.BND", "MUDSCRUB.BAN","MUDCHSL.BAN","MUDWORK.BND"),
            new SpawnEntry(82, "Flying Slig", null, "FLYSLIG.BND", "SLGBLOW.BAN", "GRENADE.BAN", "SMEXP.BAN","METAL.BAN", "BIGFLASH.BAN", "VAPOR.BAN"),
            new SpawnEntry(83, "Fleech",null, "FLEECH.BAN", "FLEEBLOW.BAN"),
            new SpawnEntry(84, "Slurg",null, "SLURG.BAN"),
            new SpawnEntry(93, "Mine Car",null, "BAYROLL.BAN", "ABECAR.BAN", "METAL.BAN", "EXPLO2.BAN"),
            new SpawnEntry(94, "Bonebag",null, "BONEBAG.BAN", "BTHROW.BND"),
            new SpawnEntry(98, "Ghost Trap",null, "GHOSTTRP.BAN"),
            new SpawnEntry(101, "Fart Machine",null, "ABESTONE.BAN", "LCDFONT.FNT", "BREWBTN.BAN", "EVILFART.BAN", "EXPLO2.BAN"),
            new SpawnEntry(103, "Naked Slig",null, "CRAWLSLG.BND", "GRENADE.BAN", "SLGBLOW.BAN", "SMEXP.BAN", "METAL.BAN", "BIGFLASH.BAN", "VAPOR.BAN"),
            new SpawnEntry(106, "Greeter",null, "MFLARE.BAN", "MOTION.BAN", "GREETER.BAN", "SPLINE.BAN", "EXPLO2.BAN"),
        };

        class SpawnEntry
        {
            public SpawnEntry(int pathId, string name,byte[] paramsData ,params string[] resources)
            {
                PathID = pathId;
                Name = name;
                Resources = resources;
                ParamsData = paramsData;
            }

            public override string ToString()
            {
                return Name;
            }

            public byte[] ParamsData;
            public int PathID;
            public string Name;
            public string[] Resources;
        }

        static string GetClipboardText()
        {
            string res = "";
            Thread staThread = new Thread(x =>
            {
                try
                {
                    res = Clipboard.GetText();
                }
                catch (Exception ex)
                {
                }
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
                try
                {
                    Clipboard.SetText(text);
                }
                catch (Exception ex)
                {
                }
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
            AliveObject[] objects = AliveAPI.ObjectList.Objects;

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
                AliveAPI.ObjectListActive.Add(clone);
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
                

                byte[] defaultParams = new byte[100];
                defaultParams[12] = 32;
                defaultParams[14] = 32;
                byte[] param = (SelectedSpawnObject.ParamsData == null) ? defaultParams : SelectedSpawnObject.ParamsData;

                lock(spawnQueues)
                {
                    spawnQueues.Add(new SpawnQueue() { spawnEntry = SelectedSpawnObject, parameters = defaultParams });
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
    }
}