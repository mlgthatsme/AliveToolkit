using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AliveAPIDotNet.Unmanaged;

namespace AliveAPIDotNet.DebugHelpers
{
    class ObjectRenderer : OnScreenRenderHelper
    {
        public static bool RenderObjectOverlays = false;

        public override void OnRender(Graphics g)
        {
            Color color = Color.FromArgb(200, 0, 255, 255);
            if (RenderObjectOverlays)
            {
                foreach (var o in AliveAPI.ObjectListBaseObjects.AsAliveObjects)
                {
                    float x = (o.PositionX - AliveAPI.CameraOffsetX) * (640 / 368.0f);
                    float y = o.PositionY - AliveAPI.CameraOffsetY;

                    float clampedX = Math.Min(Math.Max(16, x), 600 - 16);
                    float clampedY = Math.Min(Math.Max(16, y), 240 - 16);

                    g.RenderBitmapFont(BitmapFont.EightByEight, $"{o.ObjectID.ToString("00")}|{AliveObject.GetVTableName(o.VTable)}", new Point((int)clampedX, (int)clampedY), color);
                    g.DrawLine(new Pen(color, 1f), new Point((int)x, (int)y), new Point((int)clampedX, (int)clampedY));
                }
            }
            
        }
    }
}
