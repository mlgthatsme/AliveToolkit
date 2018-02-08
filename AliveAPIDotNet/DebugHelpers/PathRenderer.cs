using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliveAPIDotNet.DebugHelpers
{
    class PathRenderer : OnScreenRenderHelper
    {
        public PathRenderer()
        {
           
        }

        public override void OnRender(Graphics g)
        {
            Tick++;
            if (Tick % 5 == 0)
                PathLines = AliveAPI.PathData.PathEntries;

            DrawPathLines(g, 640, 240);
        }

        public static bool Enabled = false;

        static PathLineObject[] PathLines;
        int Tick = 0;

        public static void DrawPathLines(Graphics g, int width, int height, bool showDetails = false)
        {
            if (!Enabled)
                return;

            if (PathLines != null)
            {
                g.ScaleTransform(width / 368.0f, height / 240.0f);
                g.TranslateTransform(-AliveAPI.CameraOffsetX, -AliveAPI.CameraOffsetY);

                foreach (var l in PathLines)
                {
                    Point p1 = new Point(l.X1, l.Y1);
                    Point p2 = new Point(l.X2, l.Y2);

                    g.DrawLine(Pens.Red, p1, p2);
                }

                foreach (var o in AliveAPI.ObjectList.AsAliveObjects)
                {
                    g.FillEllipse(Brushes.YellowGreen, new RectangleF(o.PositionX - 4, o.PositionY - 8, 8, 8));
                }

                lock (AliveAPI.RaycastHits)
                {
                    foreach (var l in AliveAPI.RaycastHits)
                    {
                        Point p1 = new Point(l.X1, l.Y1);
                        Point p2 = new Point(l.X2, l.Y2);

                        if (l.Hit)
                        {
                            p2 = new Point(l.CX, l.CY);
                        }

                        g.DrawLine((l.Hit) ? Pens.Green : Pens.Blue, p1, p2);

                        g.FillEllipse(Brushes.Yellow, new RectangleF(l.CX - 2, l.CY - 2, 4, 4));
                        g.FillEllipse(Brushes.Brown, new RectangleF(l.X1 - 2, l.Y1 - 2, 4, 4));

                        if (l.Hit)
                        {
                            PathLineObject line = new PathLineObject(new IntPtr(l.CollidedObject));
                            g.DrawLine(Pens.Orange, line.X1, line.Y1, line.X2, line.Y2);
                        }

                        if (showDetails)
                            g.DrawString(l.Mode.ToString(), new Font(FontFamily.GenericSansSerif, 5.0f), Brushes.White, new PointF((l.X1 + l.X2) / 2, (l.Y1 + l.Y2) / 2), new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
                        //if (l.Hit)
                        //{
                        //    g.DrawString(l.CollidedObject.ToString(), Font, Brushes.White, new PointF((l.X1 + l.CX) / 2, (l.Y1 + l.CY) / 2), new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
                        //}
                    }
                }

                g.ResetTransform();
            }
        }
    }
}
