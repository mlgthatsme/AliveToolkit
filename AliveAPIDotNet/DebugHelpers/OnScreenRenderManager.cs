using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AliveAPIDotNet.DebugHelpers
{
    public static class OnScreenRenderManager
    {
        public static void Init()
        {
            Console.WriteLine("Initializing Renderer Helper");
            AliveAPI.OnDebugDraw += AliveAPI_OnDebugDraw;

            Helpers.Add(new PathRenderer());
            Helpers.Add(new DDCheatNew());
            Helpers.Add(new AIHelper());
        }

        private static void AliveAPI_OnDebugDraw(object sender, EventArgs e)
        {
            Graphics graphics = Graphics.FromHdc(AliveAPI.ScreenHdc);
            graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            // Here we try to render in the currently clipped zone of the screen. Things like screen transitions.
            bool screenWipeXGoingBack = Marshal.ReadInt16(new IntPtr(0x00C3D080)) > 0;
            bool screenWipeYGoingBack = Marshal.ReadInt16(new IntPtr(0x00C3D082)) > 0;
            int screenWipeX = Marshal.ReadInt16(new IntPtr(0x00C3D084));
            int screenWipeY = Marshal.ReadInt16(new IntPtr(0x00C3D086));

            Rectangle r;
            if (screenWipeXGoingBack || screenWipeYGoingBack)
                r = new Rectangle((screenWipeX > 0) ? (640 - screenWipeX) : 0, (screenWipeY > 0) ? (240 - screenWipeY) : 0, (screenWipeX > 0) ? screenWipeX : 640, (screenWipeY > 0) ? screenWipeY : 240);
            else
                r = new Rectangle(0, 0, screenWipeX, screenWipeY);
            graphics.SetClip(r);

            var t = Helpers.ToArray();
            foreach(var h in Helpers)
            {
                h.OnRender(graphics);
            }
        }

        public static List<OnScreenRenderHelper> Helpers { get; set; } = new List<OnScreenRenderHelper>();
    }
}
