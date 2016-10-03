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

namespace AliveAPIDotNet
{
    public partial class ObjectEditor : Form
    {
        public ObjectEditor(AliveObject obj)
        {
            InitializeComponent();
            aliveObj = obj;
        }

        AliveObject aliveObj;

        private void ObjectEditor_Load(object sender, EventArgs e)
        {
            Text = string.Format("Editing {1} at {0}", aliveObj.Pointer.ToString("X"), AliveObject.GetFriendlyName(aliveObj.ObjectID));

            propertyGrid1.SelectedObject = aliveObj;
            foreach (Control control in propertyGrid1.Controls)
            {
                ToolStrip toolStrip = control as ToolStrip;

                if (toolStrip != null)
                {
                    var button = new ToolStripButton("Hex Edit");
                    toolStrip.Items.Add(button);
                    button.Click += delegate { new AliveAPIDotNet.ObjectHexEdit(aliveObj).Show(); };

                    break;
                }
            }
        }
    }
}
