using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Be.Windows.Forms;
using System.Runtime.InteropServices;
using AliveAPIDotNet.Unmanaged;

namespace AliveAPIDotNet.Forms
{
    public partial class ObjectEditor : Form
    {
        public ObjectEditor(object obj)
        {
            InitializeComponent();
            aliveObj = obj;
        }

        object aliveObj;

        private void ObjectEditor_Load(object sender, EventArgs e)
        {
            propertyGrid1.SelectedObject = aliveObj;
            if (aliveObj is UnmanagedObject)
            {
                Text = string.Format("Editing object at {0}", ((UnmanagedObject)aliveObj).Pointer.ToString("X"));
                
                foreach (Control control in propertyGrid1.Controls)
                {
                    ToolStrip toolStrip = control as ToolStrip;

                    if (toolStrip != null)
                    {
                        var button = new ToolStripButton("Hex Edit");
                        toolStrip.Items.Add(button);
                        button.Click += delegate { new ObjectHexEdit((UnmanagedObject)aliveObj).Show(); };

                        var autoUpdateButton = new ToolStripButton("Auto Update");
                        autoUpdateButton.CheckOnClick = true;
                        toolStrip.Items.Add(autoUpdateButton);
                        autoUpdateButton.Click += delegate { timer1.Enabled = autoUpdateButton.Checked; };

                        break;
                    }
                }

            }        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            propertyGrid1.SelectedObject = aliveObj;
        }
    }
}
