using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DCMaster
{
    // Public, because it is used from UI code.
    public class GraphVisualizer
    {
        public class GraphVisualiz
        {
            public static void ShowGraph(string wk,Dictionary<string, List<(string, float)>> graph, Dictionary<string, int> energyByPos = null)
            {
                string wkNum = wk;
                //Application.EnableVisualStyles();
                //Application.SetCompatibleTextRenderingDefault(false);
                //Application.Run(new GraphForm(graph, energyByPos));
                var f = new GraphForm(wkNum, graph, energyByPos);
                f.Show();        // nem blokkol
                                 // f.ShowDialog(); // ha inkább modális ablakot akarsz
            }
        }

        public class GraphForm : Form
        {
            private Dictionary<string, List<(string, float)>> _graph;
            private Dictionary<string, int> _energyByPos;
            private Dictionary<string, PointF> _positions = new Dictionary<string, PointF>();
            private int _nodeSize = 20;
            private Font _font = new Font("Arial", 6);
            private Font _fontBig = new Font("Arial", 8);
            private string _selectedWorkerLabel;
            private int wkNum = 0;

            public GraphForm(string selected_worker, Dictionary<string, List<(string, float)>> graph, Dictionary<string, int> energyByPos = null)
            {
                _selectedWorkerLabel = selected_worker ?? string.Empty;
                // Be tolerant: selected_worker might not be numeric (e.g., if user passes a title)
                int.TryParse(_selectedWorkerLabel, out wkNum);
                this._graph = graph;
                this._energyByPos = energyByPos;
                this.DoubleBuffered = true;
                this.Width = 800;
                this.Height = 800;
                this.Text = (wkNum != 0)
                    ? "Knowledge Graph Visualizer: Selected worker # " + wkNum
                    : "Knowledge Graph Visualizer: " + _selectedWorkerLabel;
                CalculatePositions();
            }

            private void CalculatePositions()
            {
                _positions.Clear();
                int radius = 300;
                int centerX = Width / 2;
                int centerY = Height / 2;
                if (_graph == null || _graph.Count == 0) return;
                var keySet = new HashSet<string>(_graph.Keys);
                foreach (var kv in _graph)
                {
                    foreach (var edge in kv.Value)
                        keySet.Add(edge.Item1);
                }
                var keys = keySet.ToList();
                if (keys.Count == 0) return;
                for (int i = 0; i < keys.Count; i++)
                {
                    double angle = 2 * Math.PI * i / keys.Count;
                    float x = centerX + (float)(radius * Math.Cos(angle));
                    float y = centerY + (float)(radius * Math.Sin(angle));
                    _positions[keys[i]] = new PointF(x, y);
                }
            }

            protected override void OnResize(EventArgs e)
            {
                base.OnResize(e);
                // Re-layout nodes when window size changes
                CalculatePositions();
                Invalidate();
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                base.OnPaint(e);
                Graphics g = e.Graphics;
                g.Clear(Color.White);

                if (_graph == null || _graph.Count == 0)
                {
                    g.DrawString("(Empty graph)", _font, Brushes.Black, new PointF(10, 10));
                    return;
                }

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
                            g.DrawString(weight.ToString("0.0"), _fontBig, Brushes.Goldenrod, mid);
                        }
                    }
                }

                foreach (var kvp in _positions)
                {
                    Brush brush = Brushes.Gray;
                    string node = kvp.Key;

                    if (_energyByPos != null && _energyByPos.TryGetValue(node, out int val))
                    {
                        //brush = val > 0 ? Brushes.Green : val < 0 ? Brushes.Red : Brushes.LightGray;
                        brush = val > 0 ? Brushes.Red : val < 0 ? Brushes.Blue : Brushes.LightGray;
                    }

                    PointF p = kvp.Value;
                    g.FillEllipse(brush, p.X - _nodeSize / 2, p.Y - _nodeSize / 2, _nodeSize, _nodeSize);
                    g.DrawEllipse(Pens.Black, p.X - _nodeSize / 2, p.Y - _nodeSize / 2, _nodeSize, _nodeSize);
                    var label = kvp.Key;
                    if (_energyByPos != null && _energyByPos.TryGetValue(kvp.Key, out int eVal))
                        label += "," + eVal;
                    g.DrawString(label, _font, Brushes.DarkGreen, p.X + _nodeSize / 2, p.Y);
                }
            }
        }
    }

}

