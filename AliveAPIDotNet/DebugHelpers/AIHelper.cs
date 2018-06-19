using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliveAPIDotNet.DebugHelpers
{
    class AIHelper : OnScreenRenderHelper
    {
        public override void OnRender(Graphics g)
        {
            if (AliveAPI.DDCheatAlwaysRender || AliveAPI.DDCheatIsFlying)
            {
                // Extra ai info
                StringBuilder aiDebug = new StringBuilder();
                int[] supportedObjectsForAI = new int[] { 69, 110, 125 };
                foreach (var o in AliveAPI.ObjectListBaseObjects.AsAliveObjects)
                {
                    if (supportedObjectsForAI.Contains(o.ObjectID))
                        aiDebug.AppendLine($"[{AliveObject.GetFriendlyName(o.ObjectID)}] - {o.AliveState}");
                }
                var aiDebugHeight = BitmapFontRenderer.GetStringHeight(BitmapFont.EightByEight, aiDebug.ToString());
                g.RenderBitmapFont(BitmapFont.EightByEight, aiDebug.ToString(), new Point(8, 240 - 8 - aiDebugHeight), Color.FromArgb(127, Color.Orange));
            }
        }
    }
}
