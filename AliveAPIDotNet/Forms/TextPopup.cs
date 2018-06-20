using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AliveAPIDotNet.Forms
{
    public partial class TextPopup : Form
    {
        public TextPopup()
        {
            InitializeComponent();
        }

        public static string GetInput(string message, string title = "Enter Text")
        {
            TextPopup popup = new TextPopup();
            popup.label1.Text = message;
            if (popup.ShowDialog() == DialogResult.OK)
            {
                return popup.textBoxInput.Text;
            }

            return null;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
