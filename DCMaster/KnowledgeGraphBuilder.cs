using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCMaster
{
    internal class KnowledgeGraphBuilder
    {
        public class KnowledgeGraphBuild
        {
            public class Node
            {
                public string Position { get; set; } // "x,y"
                public int Energy { get; set; }
            }

            public class Edge
            {
                public string From { get; set; }
                public string To { get; set; }
                public int Steps { get; set; }
                public List<string> Path { get; set; } = new List<string>();
            }

            public List<Node> Nodes { get; private set; } = new List<Node>();
            public List<Edge> Edges { get; private set; } = new List<Edge>();

            public void BuildFromWorker(worker w)
            {
                Nodes.Clear();
                Edges.Clear();

                // 1. Imprint -> Node-ok
                HashSet<string> seenPositions = new HashSet<string>();
                foreach (var entry in w.Imprint)
                {
                    var parts = entry.Split(',');
                    string pos = parts[0] + "," + parts[1];
                    int energy = int.Parse(parts[2]);

                    if (!seenPositions.Contains(pos))
                    {
                        Nodes.Add(new Node { Position = pos, Energy = energy });
                        seenPositions.Add(pos);
                    }
                }

                // 2. Élek létrehozása az imprint pontok között
                for (int i = 1; i < w.Imprint.Count; i++)
                {
                    string from = GetPos(w.Imprint[i - 1]);
                    string to = GetPos(w.Imprint[i]);

                    var segment = ExtractPathSegment(w.WorkerPath, from, to);
                    if (segment.Count > 1)
                    {
                        Edges.Add(new Edge
                        {
                            From = from,
                            To = to,
                            Steps = segment.Count,
                            Path = segment
                        });
                    }
                }
            }

            private string GetPos(string entry)
            {
                var parts = entry.Split(',');
                return parts[0] + "," + parts[1];
            }

            private List<string> ExtractPathSegment(List<string> fullPath, string start, string end)
            {
                List<string> result = new List<string>();
                bool recording = false;

                foreach (var item in fullPath)
                {
                    var pos = item.Split(',')[0] + "," + item.Split(',')[1];
                    if (pos == start) recording = true;
                    if (recording) result.Add(item);
                    if (pos == end && recording) break;
                }

                return result;
            }
        }
    }

}
