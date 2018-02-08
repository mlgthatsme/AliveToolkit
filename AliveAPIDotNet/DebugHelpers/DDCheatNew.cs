using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliveAPIDotNet.DebugHelpers
{
    class DDCheatNew : OnScreenRenderHelper
    {
        UnmanagedString ddcheatStringBuffer = new UnmanagedString(new IntPtr(0x00C2764A));

        public override void OnRender(Graphics g)
        {
            // Newer and cleaner ddcheat text
            
            if (AliveAPI.DDCheatAlwaysRender || AliveAPI.DDCheatIsFlying)
                g.RenderBitmapFont(BitmapFont.EightByEight, ddcheatStringBuffer, new Point(8, 8), Color.FromArgb(200, 0, 255, 255));
        }
    }
}
