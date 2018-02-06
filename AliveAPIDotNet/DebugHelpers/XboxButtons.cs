using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliveAPIDotNet.DebugHelpers
{
    class XboxButtons : OnScreenRenderHelper
    {
        public override void OnRender(Graphics g)
        {
        }

        Rectangle src_btn_A = new Rectangle(0, 0, 32, 32);
        Rectangle src_btn_X = new Rectangle(32, 0, 32, 32);
        Rectangle src_btn_Y = new Rectangle(64, 0, 32, 32);
        Rectangle src_btn_B = new Rectangle(96, 0, 32, 32);

        void DrawButton(Graphics g, Bitmap b, Rectangle src, Point location)
        {
            g.DrawImage(b, new Rectangle(location.X - (src.Width / 2), location.Y - (src.Height / 2), src.Width, src.Height), src, GraphicsUnit.Pixel);
        }

        public override void OnRenderBackground(Graphics g, int level, int path, int cam, bool dirty)
        {
            Point btnBottomLeft = new Point(64, 481);

            if (dirty)
            {
                if (level == 0 && path == 1 && (cam == 1 || cam == 2 || cam == 3))
                {
                    DrawButton(g, Properties.Resources.ABE_XBOX_BUTTONS, src_btn_A, new Point(64, 481));
                }
            }
        }
    }
}
