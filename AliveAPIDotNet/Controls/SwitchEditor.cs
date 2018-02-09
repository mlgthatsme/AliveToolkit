using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AliveAPIDotNet.Controls
{
    public partial class SwitchEditor : UserControl
    {
        public SwitchEditor()
        {
            InitializeComponent();

            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
        }

        private void SwitchEditor_Paint(object sender, PaintEventArgs e)
        {
            byte[] switchData = (DesignMode) ? new byte[256] :  AliveAPI.GetSwitchStates();

            int i = 0;
            int xSize = doubleBufferPanel1.Width / 16;
            int ySize = doubleBufferPanel1.Height / 16;
            for (int y = 0; y < 16; y++)
            {
                for (int x = 0; x < 16; x++)
                {
                    Rectangle r = new Rectangle(x * xSize, y * ySize, xSize - ((x == 15) ? 1 : 0), ySize - ((y == 15) ? 1 : 0));
                    e.Graphics.FillRectangle((switchData[i] > 0) ? Brushes.LawnGreen : new SolidBrush(BackColor), r);
                    e.Graphics.DrawRectangle(Pens.Black, r);
                    e.Graphics.DrawString(i.ToString(), Font, Brushes.Black, r, new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
                    i++;
                }
            }
        }

        private void SwitchEditor_MouseClick(object sender, MouseEventArgs e)
        {
            byte[] switchData = (DesignMode) ? new byte[256] : AliveAPI.GetSwitchStates();

            int xSize = doubleBufferPanel1.Width / 16;
            int ySize = doubleBufferPanel1.Height / 16;

            int x = e.X / xSize;
            int y = e.Y / ySize;

            int i = (y * 16) + x;

            AliveAPI.SetSwitchState(i, (byte)((switchData[i] > 0) ? 0 : 1));

            Refresh();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 256; i++)
            {
                AliveAPI.SetSwitchState(i, 1);
            }

            Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 256; i++)
            {
                AliveAPI.SetSwitchState(i, 0);
            }

            Refresh();
        }
    }
}
