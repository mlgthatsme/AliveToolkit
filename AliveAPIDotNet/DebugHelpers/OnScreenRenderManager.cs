using AliveAPIDotNet.AI;
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

            Helpers.Add(new XboxButtons());
            Helpers.Add(new PathRenderer());
            Helpers.Add(new DDCheatNew());
            //Helpers.Add(new AIHelper());

            AlivePlugin.AIPlayer = new AIPlayer();
            AlivePlugin.AIPlayer.Start();

            Helpers.Add(AlivePlugin.AIPlayer);
        }

        private static int prevLevel;
        private static int prevPath;
        private static int prevCam;

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

            bool backgroundDirty = (prevLevel != AliveAPI.CurrentLevel || prevPath != AliveAPI.CurrentPath || prevCam != AliveAPI.CurrentCam);

            var t = Helpers.ToArray();
            foreach(var h in Helpers)
            {
                h.OnRender(graphics);
            }
            graphics.SetClip(new Rectangle(0, 272, 640, 240));
            foreach (var h in Helpers)
            {
                h.OnRenderBackground(graphics, AliveAPI.CurrentLevel, AliveAPI.CurrentPath, AliveAPI.CurrentCam, backgroundDirty);
            }

            prevLevel = AliveAPI.CurrentLevel;
            prevPath = AliveAPI.CurrentPath;
            prevCam = AliveAPI.CurrentCam;
        }

        public static List<OnScreenRenderHelper> Helpers { get; set; } = new List<OnScreenRenderHelper>();
    }
}
