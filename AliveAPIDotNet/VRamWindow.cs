using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AliveAPIDotNet
{
    public partial class VRamWindow : Form
    {
        public VRamWindow()
        {
            InitializeComponent();
            FormClosing += VRamWindow_FormClosing;
        }

        private void VRamWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        private void VRamWindow_Load(object sender, EventArgs e)
        {
            AliveAPI.OnDebugDraw += AliveAPI_GameTick;
        }

        [DllImport("gdi32.dll")]
        public static extern bool BitBlt(IntPtr hdc, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, int dwRop);

        private void AliveAPI_GameTick(object sender, EventArgs e)
        {
            Invoke(new MethodInvoker(delegate
            {
                if (pictureBox1.Image == null)
                {
                    pictureBox1.Image = new Bitmap(1024, 512);
                }

                if (AliveAPI.ScreenHdc != IntPtr.Zero)
                {
                    Graphics src = Graphics.FromHdc(AliveAPI.ScreenHdc);
                    var g = Graphics.FromImage(pictureBox1.Image);
                    BitBlt(g.GetHdc(), 0, 0, pictureBox1.Image.Width, pictureBox1.Image.Height, AliveAPI.ScreenHdc, 0, 0, 0x00CC0020);
                    g.ReleaseHdc();
                }

                pictureBox1.Refresh();
            }));
        }
    }
}
