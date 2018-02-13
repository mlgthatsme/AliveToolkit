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
    public partial class PaletteEditor : UserControl
    {
        public PaletteEditor()
        {
            InitializeComponent();
        }

        private void PaletteEditor_Load(object sender, EventArgs e)
        {

        }

        public List<Color> PaletteColors { protected set; get; } = new List<Color>();

        public void SetPalette(int colorCount, byte[] data)
        {
            PaletteColors.Clear();
            int bOffset = 0;
            for(int i = 0; i < colorCount;i++)
            {
                uint oldPixel = BitConverter.ToUInt16(data, bOffset);

                uint semiTrans = (((oldPixel) >> 15) & 0xffff);

                // RGB555
                ushort green16 = (ushort)((oldPixel >> 5) & 0x1F);
                ushort red16 = (ushort)((oldPixel >> 0) & 0x1F);
                ushort blue16 = (ushort)((oldPixel >> 10) & 0x1F);

                uint green32 = (uint)((green16 * 255) / 31);
                uint blue32 = (uint)((blue16 * 255) / 31);
                uint red32 = (uint)((red16 * 255) / 31);

                uint newPixel = (red32 << 24) | (blue32 << 8) | (green32 << 16);

                int alpha = 255;

                if (semiTrans > 0)
                    alpha = 127;
                else if (newPixel == 0)
                    alpha = 0;

                PaletteColors.Add(Color.FromArgb(alpha, (int)red32, (int)green32, (int)blue32));

                bOffset += 2;
            }

            doubleBufferPanel1.Refresh();
        }

        private void doubleBufferPanel1_Paint(object sender, PaintEventArgs e)
        {
            int gridSize = 12;

            for(int y = 0; y < (doubleBufferPanel1.Height / gridSize) + 1; y++)
            {
                for (int x = 0; x < (doubleBufferPanel1.Width / gridSize) + 1; x++)
                {
                    e.Graphics.FillRectangle(((x + y) % 2 == 0) ? Brushes.Gray : Brushes.White, new Rectangle(gridSize * x, gridSize * y, gridSize, gridSize));
                }
            }

            if (PaletteColors.Count > 0)
            {
                float pixelSize = doubleBufferPanel1.Width / (float)PaletteColors.Count;

                for (int i = 0; i < PaletteColors.Count; i++)
                {
                    e.Graphics.FillRectangle(new SolidBrush(PaletteColors[i]), new RectangleF(pixelSize * i, 0, pixelSize, doubleBufferPanel1.Height));
                }
            }
        }
    }
}
