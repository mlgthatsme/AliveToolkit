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

        int currentLevel = 0;
        int currentPath = 0;
        int currentCam = 0;

        bool IsOnCam(int level, int path, int[] cams)
        {
            if (level != currentLevel)
                return false;
            if (path != currentPath)
                return false;
            if (!cams.Contains(currentCam))
                return false;

            return true;
        }

        void DrawButton(Graphics g, Bitmap b, Rectangle src, Point location)
        {
            g.DrawImage(b, new Rectangle(location.X - (src.Width / 2), location.Y - (src.Height / 2), src.Width, src.Height), src, GraphicsUnit.Pixel);
        }

        public override void OnRenderBackground(Graphics g, int level, int path, int cam, bool dirty)
        {
            currentLevel = level;
            currentPath = path;
            currentCam = cam;

            Point btnBottomLeft = new Point(64, 481);
            Point btnBottomRight = new Point(577, 480);
            Point btnTopLeft = new Point(60, 303);

            if (dirty && AliveAPI.Input.JoystickEnabled)
            {
                if (IsOnCam(0, 1, new int[] { 1, 2, 3 }))
                {
                    DrawButton(g, Properties.Resources.ABE_XBOX_BUTTONS, src_btn_A, btnBottomLeft);
                }
                if (IsOnCam(0, 1, new int[] { 3, 2, 4, 30, 31, 6, 23 }))
                {
                    DrawButton(g, Properties.Resources.ABE_XBOX_BUTTONS, src_btn_Y, btnBottomRight);
                }
                if (IsOnCam(0, 1, new int[] { 31, 6, 4, 30, 23 })) // Top Left A
                {
                    DrawButton(g, Properties.Resources.ABE_XBOX_BUTTONS, src_btn_A, btnTopLeft);
                }
                if (IsOnCam(0, 1, new int[] { 12 }))
                {
                    DrawButton(g, Properties.Resources.ABE_XBOX_BUTTONS, src_btn_A, new Point(282, 440));
                    DrawButton(g, Properties.Resources.ABE_XBOX_BUTTONS, src_btn_Y, new Point(288, 486));
                }
            }
        }
    }
}
