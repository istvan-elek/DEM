using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Data;
using System.Runtime.Remoting.Channels;
using System.Collections;
using System.Windows.Forms;
using System.Threading;
using System.Windows.Forms.DataVisualization.Charting;

namespace DCMaster
{
    public class worker
    {
        labyrinth lab;
        static Random rnd = new Random(); //random number for neighbor choose
        float lbsize;
        int initEnergy;
        int movementCost;
        int delayValue;
        

        
        // Knowledge graph built from Imprint (nodes) and WorkerPath (edges)
        // Node key: "x,y" (stable); node energy stored separately.
        public KnowledgeGraph Knowledge { get; } = new KnowledgeGraph();

        public worker(labyrinth currentLab, Int32 id, Boolean learning, IList<string> param) 
        {
            lab = currentLab;
            lbsize = (float)lab.Size;
            _ID = id;
            _learn = learning;
            initEnergy = Convert.ToInt16(param[0].Split(';')[1]); ;
            movementCost = Convert.ToInt16(param[3].Split(';')[1]);
            delayValue= Convert.ToInt16(param[6].Split(';')[1]);
        }


#region properties

        private Int32 _ID;
        public Int32 ID
        {
            get { return _ID;}
            set { _ID = value; }
        }


        string _currentPosition;
        public string CurrentPosition
        {
            get { return _currentPosition; }
            set { _currentPosition = value; }
        }

        Int32 _energy;
        public Int32 Energy
        {
            get { return _energy; }
            set
            {
                _energy = value;
                if (_energy < 1) { _live = false; }
            }
        }

        Boolean _learn;
        public Boolean Learn 
        { 
            get { return _learn; } 
            set { _learn = value; }
        }



        string _parent;
        public string Parent
        {
            get { return _parent; }
            set { _parent = value; }
        }


        List<string> _workerPath = new List<string>(); // worker path is series of number triads, which contains x,y coordinates and energy of the fields in a labirynth
        public List<string> WorkerPath
        {get { return _workerPath; } }


        List<string> _edge = new List<string>();
        public List<string> Edge
        { get { return _edge; } }


        private string _startLocation;
        public string StartLocation
        {
            get { return _startLocation; }
            set
            {
                _startLocation = value;
                _currentPosition = _startLocation;
                if (_startLocation != "")
                {
                    int x = Convert.ToInt16(_currentPosition.Split(',')[0]);
                    int y = Convert.ToInt16(_currentPosition.Split(',')[1]);
                    _workerPath.Add(_currentPosition + "," + lab.Fields[x,y]);
                    Knowledge.UpdateFromImprint(_imprint);
                    Knowledge.UpdateFromWorkerPath(_workerPath);
                }
            }
        }

        Boolean _live = true;
        public Boolean Live
        {
            get { return _live;}
            set { _live = value; }
        }

        //HashSet<string> _imprint = new HashSet<string>();
        //public HashSet<string> Imprint
        //{
        //    get { return _imprint; }
        //    set { _imprint = value; }
        //}


        List<string> _imprint = new List<string>();
        public List<string> Imprint
        {
            get { return _imprint; }
            set { _imprint = value; }
        }

        float _sEntropy =0;
        public float SEntropy
        {
            get { return _sEntropy; }
            set { _sEntropy = value; }
        }



#endregion


#region Methods

        public void moveNext()  //moves the next position in labirynth. Find neighbors of the current field, and select a field randomly from the neighbors
        {
            HashSet<string> visited = new HashSet<string>(_imprint);            
            List<string> currentNeighbours = new List<string>();
            currentNeighbours = getNeighbors(_currentPosition);
            int xold= Convert.ToInt16(_currentPosition.Split(',')[0]);
            int yold = Convert.ToInt16(_currentPosition.Split(',')[1]);
            string newPosition = currentNeighbours[rnd.Next(0, currentNeighbours.Count)];
            if (_learn)  // ha tanul, akkor megnézi, hogy az új mező szerepel-e a tudásában (imprint)
            {
                foreach (string item in _imprint)
                {
                    string[] s = item.Split(',');   //imprint content
                    string s2 = s[0] + "," + s[1]; // position x=s[0], y=s[1],  s[2] is energy
                    Boolean danger = true;
                    while (danger)
                    {
                        if ((s2 == newPosition) && (int.Parse(s[2]) < movementCost))// megnézi, hogy a mező benne van-e az imprintben és energia értéke negatív-e (energianyelő)
                        {
                            newPosition = currentNeighbours[rnd.Next(0, currentNeighbours.Count)];
                        }
                        else
                        {
                            danger = false;
                        }
                    }
                }
            }
            int x = Convert.ToInt16(newPosition.Split(',')[0]);
            int y = Convert.ToInt16(newPosition.Split(',')[1]);
            _workerPath.Add(newPosition + "," + lab.Fields[x, y]);
            int fieldval = lab.Fields[x, y];
            if (_learn) // ha tanul, akkor az új mezőt az energiatartalmával együtt beírja az imprintbe (tudásbázisba)
            {
                if (lab.Delay[x, y] > 0)  // disable a mező
                {
                    lab.Delay[x, y] -= 1;
                }
                else  // enable a mező
                {
                    if (fieldval < movementCost) { visited.Add(newPosition + "," + fieldval);  }
                    if (fieldval > -movementCost) { visited.Add(newPosition + "," + fieldval); lab.Delay[x, y] = delayValue; }
                    _energy += fieldval;
                    _sEntropy -= Math.Abs(fieldval - lab.Fields[xold, yold]);
                    _imprint = visited.ToList();
                }
            }
            else  // ha nem tanul
            {
                if (lab.Delay[x, y] == 0)
                {
                    _energy += fieldval; 
                    _sEntropy -= Math.Abs(fieldval - lab.Fields[xold, yold]);
                    if (fieldval > -movementCost) lab.Delay[x, y] = delayValue;                   
                }
                else
                {
                    lab.Delay[x, y] -= 1;
                }
            }
            _currentPosition = newPosition;

            // Update knowledge graph incrementally
            Knowledge.UpdateFromImprint(_imprint);
            Knowledge.UpdateFromWorkerPath(_workerPath);

            if (_energy < 1) { _live = false; }
        }


        public List<string> getNeighbors(string currentPos)  //computes adjacent fields
        {
            List<string> neighbors = new List<string>();
            int x = Convert.ToInt16(currentPos.Split(',')[0]);
            int y = Convert.ToInt16(currentPos.Split(',')[1]);
            Math.DivRem(x - 1 + lab.Size, lab.Size, out int xrem);
            neighbors.Add(xrem + "," + y);
            Math.DivRem(x + 1 + lab.Size, lab.Size, out xrem);
            neighbors.Add(xrem + "," + y);
            Math.DivRem(y - 1 + lab.Size, (lab.Size), out int yrem);
            neighbors.Add(x + "," + yrem);
            Math.DivRem(y + 1 + lab.Size, (lab.Size), out yrem);
            neighbors.Add(x + "," + yrem);
            return neighbors;
        }

    /// <summary>
    /// Knowledge graph built from:
    ///  - Nodes: Imprint triads ("x,y,energy") => key "x,y", energy attribute
    ///  - Edges: WorkerPath triads ("x,y,energy") in time order => directed transitions
    /// Uses incremental processing of WorkerPath to avoid O(n^2) rebuilds.
    /// </summary>
    public class KnowledgeGraph
    {
        // adjacency: from -> (to -> weight)
        public Dictionary<string, Dictionary<string, float>> Adj { get; } = new Dictionary<string, Dictionary<string, float>>();

        // node attributes: last known energy per position ("x,y")
        public Dictionary<string, int> EnergyByPos { get; } = new Dictionary<string, int>();

        // last processed index in workerPath list
        public int LastProcessedPathIndex { get; private set; } = 0;

        private static bool TryParseTriad(string triad, out string pos, out int energy)
        {
            pos = "";
            energy = 0;
            if (string.IsNullOrWhiteSpace(triad)) return false;

            var parts = triad.Split(',');
            if (parts.Length < 3) return false;

            // Keep as string key "x,y" to be stable even if energy changes
            pos = parts[0].Trim() + "," + parts[1].Trim();
            return int.TryParse(parts[2].Trim(), out energy);
        }

        /// <summary>
        /// Refresh node energy attributes from imprint (unique visited fields).
        /// Safe to call often (imprint is typically much smaller than path).
        /// </summary>
        public void UpdateFromImprint(List<string> imprint)
        {
            if (imprint == null) return;

            // imprint is a set-like list (order not meaningful)
            foreach (var s in imprint)
            {
                if (TryParseTriad(s, out var pos, out var e))
                {
                    EnergyByPos[pos] = e;
                }
            }
        }

        /// <summary>
        /// Incrementally update edges from workerPath and also learn energies from the path.
        /// </summary>
        public void UpdateFromWorkerPath(List<string> workerPath)
        {
            if (workerPath == null) return;

            if (workerPath.Count == 0)
            {
                LastProcessedPathIndex = 0;
                return;
            }

            // Learn energy from the last element even if there's no edge yet
            if (workerPath.Count == 1)
            {
                if (TryParseTriad(workerPath[0], out var singlePos, out var singleE))
                    EnergyByPos[singlePos] = singleE;
                LastProcessedPathIndex = 1;
                return;
            }

            // Ensure LastProcessedPathIndex is sane
            if (LastProcessedPathIndex < 0 || LastProcessedPathIndex > workerPath.Count)
                LastProcessedPathIndex = 0;

            for (int i = Math.Max(1, LastProcessedPathIndex); i < workerPath.Count; i++)
            {
                if (!TryParseTriad(workerPath[i - 1], out var a, out var ea)) continue;
                if (!TryParseTriad(workerPath[i], out var b, out var eb)) continue;

                // record energies (last known)
                EnergyByPos[a] = ea;
                EnergyByPos[b] = eb;

                if (a == b) continue;

                if (!Adj.TryGetValue(a, out var outMap))
                {
                    outMap = new Dictionary<string, float>();
                    Adj[a] = outMap;
                }

                // weight = transition frequency (can be replaced later with reinforcement logic)
                outMap[b] = outMap.TryGetValue(b, out var w) ? w + 1f : 1f;
            }

            LastProcessedPathIndex = workerPath.Count;
        }

        /// <summary>
        /// Convert to the structure expected by GraphVisualizer: Dictionary(from -> List(to, weight)).
        /// </summary>
        public Dictionary<string, List<(string to, float weight)>> ToVisualizerGraph()
        {
            var result = new Dictionary<string, List<(string to, float weight)>>();

            foreach (var kv in Adj)
            {
                var list = kv.Value.Select(e => (e.Key, e.Value)).ToList();
                result[kv.Key] = list;
            }

            return result;
        }
    }

#endregion
    }
}
