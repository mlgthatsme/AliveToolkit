using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AliveAPIDotNet
{
    public partial class LineGraph : UserControl
    {
        public LineGraph()
        {
            InitializeComponent();
        }

        Dictionary<int, List<float>> GraphValues = new Dictionary<int, List<float>>();

        public string Title { get { return label1.Text; } set { label1.Text = value; } }

        public int MaxPoints { get; set; } = 256;

        private int tick = 0;

        public void SetValue(float v, int graphId)
        {
            if (v > maxValue)
                maxValue = v;
            if (v < minValue)
                minValue = v;

            if (!GraphValues.ContainsKey(graphId))
            {
                GraphValues.Add(graphId, new List<float>());
            }

            if (GraphValues[graphId].Count == 1)
            {
                minValue = v;
                maxValue = v;
            }

            GraphValues[graphId].Add(v);
            if (GraphValues[graphId].Count > MaxPoints)
                GraphValues[graphId].RemoveAt(0);

            doubleBufferPanel1.Refresh();
        }

        float minValue = 0.0f;
        float maxValue = 1.0f;

        float Lerp(float from, float to, float t)
        {
            return from + ((to - from) * t);
        }

        float fitMult;
        float offsetY;

        private void doubleBufferPanel1_Paint(object sender, PaintEventArgs e)
        {
            tick++;

            label2.Text = maxValue.ToString("0.00");
            label3.Text = minValue.ToString("0.00");

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;



            e.Graphics.DrawRectangle(new Pen(new SolidBrush(ForeColor), 1.0f), new Rectangle(0, 0, doubleBufferPanel1.Width - 1, doubleBufferPanel1.Height - 1));

            fitMult = doubleBufferPanel1.Height / (maxValue - minValue);// Lerp(fitMult, doubleBufferPanel1.Height / (maxValue - minValue), 0.1f);
            offsetY = (((minValue + maxValue) / 2.0f) * fitMult);

            e.Graphics.TranslateTransform(0, offsetY + (doubleBufferPanel1.Height / 2));

            int gridSplit = 4;
            float gridSplitGapWidth = doubleBufferPanel1.Width / (float)gridSplit;
            float gridSplitGapHeight = doubleBufferPanel1.Height / (float)gridSplit;
            Pen p = new Pen(new SolidBrush(Color.FromArgb(100, Color.Black)), 1.0f);
            for (int i = -5; i < gridSplit + 5; i++)
            {
                int gridYOffset = 0;
                int gridXOffset = ((int)((doubleBufferPanel1.Width / (float)MaxPoints) * tick)) % (int)(gridSplitGapWidth);
                e.Graphics.DrawLine(p, new PointF(-doubleBufferPanel1.Width - gridXOffset, (gridSplitGapHeight * i) - gridYOffset), new PointF((doubleBufferPanel1.Width * 2) - gridXOffset, (gridSplitGapHeight * i) - gridYOffset));
                e.Graphics.DrawLine(p, new PointF((gridSplitGapWidth * i) - gridXOffset, -doubleBufferPanel1.Height - gridYOffset), new PointF((gridSplitGapWidth * i) - gridXOffset, (doubleBufferPanel1.Height * 2) - gridYOffset));
            }

            e.Graphics.DrawLine(new Pen(new SolidBrush(Color.FromArgb(150, Color.Black)), 1.0f), new Point(0, 0), new Point(doubleBufferPanel1.Width, 0));

            foreach (var v in GraphValues)
            {
                List<PointF> points = new List<PointF>();
                for (int x = 0; x < v.Value.Count; x++)
                {
                    points.Add(new PointF(x * (doubleBufferPanel1.Width / (float)MaxPoints), v.Value[x] * -fitMult));
                }

                PointF[] linePoints = points.ToArray();

                if (points.Count > 1)
                {
                    points.Insert(0, new PointF(points[0].X, points[0].Y + 1000));
                    points.Add(new PointF(points[points.Count - 1].X, points[points.Count - 1].Y + 1000));

                    e.Graphics.FillPolygon(new SolidBrush(Color.FromArgb(80, ForeColor)), points.ToArray());

                    e.Graphics.DrawLines(new Pen(new SolidBrush(ForeColor), 1.0f), linePoints);
                }
            }
        }
    }

    public class DoubleBufferPanel : Panel
    {
        public DoubleBufferPanel()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint, true);
        }
    }
}
