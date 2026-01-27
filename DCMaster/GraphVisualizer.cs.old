using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DCMaster
{
    internal class GraphVisualizer
    {
        public class GraphVisualiz
        {
            public static void ShowGraph(Dictionary<string, List<(string, float)>> graph)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new GraphForm(graph));
            }
        }

        public class GraphForm : Form
        {
            private Dictionary<string, List<(string, float)>> _graph;
            private Dictionary<string, PointF> _positions = new Dictionary<string, PointF>();
            private int _nodeSize = 20;
            private Font _font = new Font("Arial", 8);

            public GraphForm(Dictionary<string, List<(string, float)>> graph)
            {
                this._graph = graph;
                this.DoubleBuffered = true;
                this.Width = 800;
                this.Height = 800;
                this.Text = "Knowledge Graph Visualizer";
                CalculatePositions();
            }

            private void CalculatePositions()
            {
                int radius = 300;
                int centerX = Width / 2;
                int centerY = Height / 2;
                var keys = _graph.Keys.ToList();
                for (int i = 0; i < keys.Count; i++)
                {
                    double angle = 2 * Math.PI * i / keys.Count;
                    float x = centerX + (float)(radius * Math.Cos(angle));
                    float y = centerY + (float)(radius * Math.Sin(angle));
                    _positions[keys[i]] = new PointF(x, y);
                }
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                base.OnPaint(e);
                Graphics g = e.Graphics;
                g.Clear(Color.White);

                Pen edgePen = new Pen(Color.Gray, 1);
                foreach (var kvp in _graph)
                {
                    var from = kvp.Key;
                    foreach (var (to, weight) in kvp.Value)
                    {
                        if (_positions.ContainsKey(from) && _positions.ContainsKey(to))
                        {
                            PointF p1 = _positions[from];
                            PointF p2 = _positions[to];
                            g.DrawLine(edgePen, p1, p2);
                            var mid = new PointF((p1.X + p2.X) / 2, (p1.Y + p2.Y) / 2);
                            g.DrawString(weight.ToString("0.0"), _font, Brushes.Black, mid);
                        }
                    }
                }

                foreach (var kvp in _positions)
                {
                    Brush brush = Brushes.Gray;
                    string node = kvp.Key;
                    var parts = node.Split(',');
                    if (parts.Length == 3 && int.TryParse(parts[2], out int val))
                    {
                        brush = val > 0 ? Brushes.Green : val < 0 ? Brushes.Red : Brushes.LightGray;
                    }

                    PointF p = kvp.Value;
                    g.FillEllipse(brush, p.X - _nodeSize / 2, p.Y - _nodeSize / 2, _nodeSize, _nodeSize);
                    g.DrawEllipse(Pens.Black, p.X - _nodeSize / 2, p.Y - _nodeSize / 2, _nodeSize, _nodeSize);
                    g.DrawString(kvp.Key, _font, Brushes.Black, p.X + _nodeSize / 2, p.Y);
                }
            }
        }
    }

}

