using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCMaster
{
    // Public, because it is referenced from UI code.
    public class KnowledgeGraphBuilder
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
            
                /// <summary>
                /// How many times this transition happened (used by BuildFromWorkerPath when aggregating).
                /// For imprint-based edges, this is typically 1.
                /// </summary>
                public int Count { get; set; } = 1;
            }

            public List<Node> Nodes { get; private set; } = new List<Node>();
            public List<Edge> Edges { get; private set; } = new List<Edge>();

            public void BuildFromWorker(worker w)
            {
                Nodes.Clear();
                Edges.Clear();

                // 1) Imprint -> Node-ok
                // Supports both formats:
                //   "x,y"            (energy unknown)
                //   "x,y,energy"     (energy present)
                var seenPositions = new HashSet<string>(StringComparer.Ordinal);
                foreach (var entry in w.Imprint)
                {
                    if (!TryParsePosEnergy(entry, out var pos, out var energy))
                        continue;

                    if (!seenPositions.Contains(pos))
                    {
                        Nodes.Add(new Node { Position = pos, Energy = energy });
                        seenPositions.Add(pos);
                    }
                }

                // 2) Élek létrehozása az imprint pontok között
                for (int i = 1; i < w.Imprint.Count; i++)
                {
                    if (!TryParsePosOnly(w.Imprint[i - 1], out var from))
                        continue;
                    if (!TryParsePosOnly(w.Imprint[i], out var to))
                        continue;

                    var segment = ExtractPathSegment(w.WorkerPath, from, to);
                    if (segment.Count > 1)
                    {
                        Edges.Add(new Edge
                        {
                            From = from,
                            To = to,
                            Steps = segment.Count - 1, // steps = number of moves
                            Path = segment
                        });
                    }
                }
            }

            /// <summary>
            /// Converts the built Nodes+Edges into the adjacency list format expected by GraphVisualizer:
            /// Dictionary&lt;pos, List&lt;(to, weight)&gt;&gt;
            /// </summary>
            
            /// <summary>
            /// Builds a knowledge graph directly from WorkerPath (time-ordered).
            ///
            /// Nodes: unique positions appearing in WorkerPath (normalized "x,y").
            /// Edges: transitions between consecutive positions in WorkerPath.
            ///
            /// If aggregateTransitions=true, repeated transitions (From->To) are merged into one edge and Edge.Count is incremented.
            /// </summary>
            public void BuildFromWorkerPath(worker w,
                                            bool aggregateTransitions = true,
                                            bool includeSelfLoops = false,
                                            int minTransitionCount = 1,
                                            int maxTransitionsPerNode = 0)
            {
                Nodes.Clear();
                Edges.Clear();

                if (w == null || w.WorkerPath == null || w.WorkerPath.Count == 0)
                    return;

                // Normalize WorkerPath entries to "x,y"
                var path = new List<string>(w.WorkerPath.Count);
                foreach (var entry in w.WorkerPath)
                {
                    if (TryParsePosOnly(entry, out var pos))
                        path.Add(pos);
                }
                if (path.Count == 0)
                    return;

                // Nodes = unique positions from path (energy unknown here -> 0; your visualizer uses EnergyByPos anyway)
                var seen = new HashSet<string>(StringComparer.Ordinal);
                foreach (var pos0 in path)
                {
                    if (seen.Add(pos0))
                        Nodes.Add(new Node { Position = pos0, Energy = 0 });
                }

                if (!aggregateTransitions)
                {
                    // One edge per step
                    for (int i = 1; i < path.Count; i++)
                    {
                        var from = path[i - 1];
                        var to = path[i];
                        if (!includeSelfLoops && from == to) continue;

                        Edges.Add(new Edge
                        {
                            From = from,
                            To = to,
                            Steps = 1,
                            Count = 1,
                            Path = new List<string> { from, to }
                        });
                    }
                    return;
                }

                // Aggregate transitions by (from,to) using a composite string key to avoid tuple-key parsing issues.
                // Key format is: "from->to" (safe because positions are "x,y" without '>' characters).
                var counts = new Dictionary<string, int>(StringComparer.Ordinal);
                for (int i = 1; i < path.Count; i++)
                {
                    var from = path[i - 1];
                    var to = path[i];
                    if (!includeSelfLoops && from == to) continue;

                    var key = from + "->" + to;
                    if (counts.TryGetValue(key, out var c))
                        counts[key] = c + 1;
                    else
                        counts[key] = 1;
                }

                // Optional: filter by minTransitionCount
                IEnumerable<KeyValuePair<string, int>> filtered = counts;
                if (minTransitionCount > 1)
                    filtered = filtered.Where(kv => kv.Value >= minTransitionCount);

                // Optional: limit transitions per node: keep the most frequent ones per "from"
                if (maxTransitionsPerNode > 0)
                {
                    filtered = filtered
                        .GroupBy(kv => kv.Key.Split(new[] { "->" }, StringSplitOptions.None)[0], StringComparer.Ordinal)
                        .SelectMany(g => g.OrderByDescending(kv => kv.Value).Take(maxTransitionsPerNode));
                }

                foreach (var kv in filtered)
                {
                    var parts = kv.Key.Split(new[] { "->" }, StringSplitOptions.None);
                    if (parts.Length != 2) continue;

                    var from = parts[0];
                    var to = parts[1];

                    Edges.Add(new Edge
                    {
                        From = from,
                        To = to,
                        Steps = 1,
                        Count = kv.Value,
                        Path = new List<string> { from, to }
                    });
                }
            }

            /// <summary>
            /// Convenience: adjacency list where edge weight is Edge.Count (frequency) instead of Steps.
            /// Useful when using graphs built from WorkerPath with aggregation.
            /// </summary>
            public Dictionary<string, List<(string, float)>> ToVisualizerGraphByCount()
            {
                var graph = new Dictionary<string, List<(string, float)>>(StringComparer.Ordinal);

                foreach (var n in Nodes)
                    if (!graph.ContainsKey(n.Position))
                        graph[n.Position] = new List<(string, float)>();

                foreach (var e in Edges)
                {
                    if (!graph.ContainsKey(e.From))
                        graph[e.From] = new List<(string, float)>();

                    graph[e.From].Add((e.To, (float)e.Count));

                    if (!graph.ContainsKey(e.To))
                        graph[e.To] = new List<(string, float)>();
                }

                return graph;
            }

public Dictionary<string, List<(string, float)>> ToVisualizerGraph()
            {
                var graph = new Dictionary<string, List<(string, float)>>(StringComparer.Ordinal);

                // ensure all nodes exist as keys
                foreach (var n in Nodes)
                {
                    if (!graph.ContainsKey(n.Position))
                        graph[n.Position] = new List<(string, float)>();
                }

                foreach (var e in Edges)
                {
                    if (!graph.ContainsKey(e.From))
                        graph[e.From] = new List<(string, float)>();

                    graph[e.From].Add((e.To, (float)e.Steps));

                    // also ensure destination node exists
                    if (!graph.ContainsKey(e.To))
                        graph[e.To] = new List<(string, float)>();
                }

                return graph;
            }

            /// <summary>
            /// Energy lookup by position from Nodes (useful for coloring nodes in GraphVisualizer).
            /// </summary>
            public Dictionary<string, int> ToEnergyByPos()
            {
                var dict = new Dictionary<string, int>(StringComparer.Ordinal);
                foreach (var n in Nodes)
                    dict[n.Position] = n.Energy;
                return dict;
            }

            private List<string> ExtractPathSegment(List<string> fullPath, string start, string end)
            {
                var result = new List<string>();
                if (fullPath == null || fullPath.Count == 0) return result;

                bool recording = false;

                foreach (var item in fullPath)
                {
                    if (!TryParsePosOnly(item, out var pos))
                        continue;

                    if (!recording && pos == start)
                        recording = true;

                    if (recording)
                        result.Add(pos); // store normalized "x,y"

                    if (recording && pos == end)
                        break;
                }

                // if end not reached, return empty segment (so we don't create bogus edges)
                if (result.Count == 0 || result[result.Count - 1] != end)
                    return new List<string>();

                return result;
            }

            private static bool TryParsePosOnly(string entry, out string pos)
            {
                pos = null;
                if (string.IsNullOrWhiteSpace(entry)) return false;
                var parts = entry.Split(',');
                if (parts.Length < 2) return false;
                var x = parts[0].Trim();
                var y = parts[1].Trim();
                if (x.Length == 0 || y.Length == 0) return false;
                pos = x + "," + y;
                return true;
            }

            private static bool TryParsePosEnergy(string entry, out string pos, out int energy)
            {
                pos = null;
                energy = 0;

                if (!TryParsePosOnly(entry, out pos))
                    return false;

                var parts = entry.Split(',');
                if (parts.Length >= 3)
                {
                    if (!int.TryParse(parts[2].Trim(), out energy))
                        energy = 0;
                }
                else
                {
                    // Energy not present in imprint entry.
                    energy = 0;
                }
                return true;
            }
        }
    }

}
