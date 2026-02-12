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
        float lbsize; //labyrinth size
        int initEnergy; // kezdeti energia, amit a paraméterekből olvasunk be
        int movementCost;// egy lépés költsége, amit a paraméterekből olvasunk be
        int delayValue;// egy mező újra engedélyezéséhez szükséges lépések száma, amit a paraméterekből olvasunk be. A delayValue értékétől függően a mező energia értéke elérhetetlen lesz. Legközelebb akkor lesz újra elérhető, amikor a delayValue értékétől függő lépések száma letelik. Ez a mechanizmus teszi lehetővé, hogy a mezők átmenetileg üressé váljanak, és így befolyásolják a worker döntéseit és útvonalát a labirintusban.



        // Knowledge graph built from Imprint (nodes) and WorkerPath (edges)
        // Node key: "x,y" (stable); node energy stored separately.
        public KnowledgeGraph Knowledge { get; } = new KnowledgeGraph();

        public worker(labyrinth currentLab, Int32 id, Boolean learning, IList<string> param) // constructor, initializes the worker with the given labyrinth, id, learning mode, and parameters (initial energy, movement cost, delay value)
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
        {
            get { return _workerPath; }
            set { _workerPath = value; }
        }


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

        public void moveNext() //moves the next position in labirynth. Find neighbors of the current field, and select a field randomly from the neighbors
        {
            HashSet<string> visited = new HashSet<string>(_imprint);            
            List<string> currentNeighbours = new List<string>();
            currentNeighbours = getNeighbors(_currentPosition);
            int xold= Convert.ToInt16(_currentPosition.Split(',')[0]);
            int yold = Convert.ToInt16(_currentPosition.Split(',')[1]);
            string newPosition = ChooseNextByGraphAndEnergy(currentNeighbours);
            if (_learn)  // ha tanul, akkor kerüli az energianyelő (negatív) mezőket az imprint alapján
            {
                int tries = 0;
                while (tries < 8 && IsDangerByImprint(newPosition))
                {
                    newPosition = ChooseNextByGraphAndEnergy(currentNeighbours);
                    tries++;
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
                if (lab.Delay[x, y] == 0)// enable a mező
                {
                    _energy += fieldval; 
                    _sEntropy -= Math.Abs(fieldval - lab.Fields[xold, yold]);
                    if (fieldval > -movementCost) lab.Delay[x, y] = delayValue;                   
                }
                else  // disable a mező
                {
                    lab.Delay[x, y] -= 1;
                }
            }
            _currentPosition = newPosition;

            // Update knowledge incrementally
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
    /// 

        private struct ScoredPos
        {
            public string Pos;
            public float Score;

            public ScoredPos(string pos, float score)
            {
                Pos = pos;
                Score = score;
            }
        }

        private string ChooseNextByGraphAndEnergy(List<string> neighbors)
        {
            // Hangolható paraméterek
            float alphaGraph = 0.7f;        // 0..1  (gráf hatása)
            float betaEnergy = 0.3f;        // energia hatása
            float temperature = 1.2f;       // >0   (kisebb -> determinisztikusabb)
            float backtrackPenalty = 1.0f;  // visszalépés bünti

            string cur = _currentPosition;

            // Előző pozíció (loop ellen)
            string prev = null;
            if (_workerPath != null && _workerPath.Count >= 2)
            {
                // workerPath elem: "x,y,energy"
                var p = _workerPath[_workerPath.Count - 2].Split(',');
                if (p.Length >= 2) prev = p[0].Trim() + "," + p[1].Trim();
            }

            var items = new List<ScoredPos>(neighbors.Count);

            foreach (var n in neighbors)
            {
                float g = 0f;

                // Gráf-él súly (ha ismert)
                if (Knowledge != null &&
                    Knowledge.Adj != null &&
                    Knowledge.Adj.TryGetValue(cur, out var outMap) &&
                    outMap != null &&
                    outMap.TryGetValue(n, out var w))
                {
                    g = w;
                }

                // Energia (ha ismert)
                int e = 0;
                if (Knowledge != null &&
                    Knowledge.EnergyByPos != null &&
                    Knowledge.EnergyByPos.TryGetValue(n, out var ee))
                {
                    e = ee;
                }

                float score = alphaGraph * g + betaEnergy * e;

                // visszalépés büntetése
                if (prev != null && n == prev) score -= backtrackPenalty;

                // kis zaj
                score += (float)(rnd.NextDouble() * 0.05);

                items.Add(new ScoredPos(n, score));
            }

            return SoftmaxPick(items, temperature);
        }

        private string SoftmaxPick(List<ScoredPos> items, float temperature)
        {
            if (items == null || items.Count == 0) return _currentPosition;
            if (temperature <= 0.0001f) temperature = 0.0001f;

            float max = items.Max(t => t.Score);

            double sum = 0;
            var weights = new double[items.Count];

            for (int i = 0; i < items.Count; i++)
            {
                double v = Math.Exp((items[i].Score - max) / temperature);
                weights[i] = v;
                sum += v;
            }

            double r = rnd.NextDouble() * sum;
            double acc = 0;

            for (int i = 0; i < items.Count; i++)
            {
                acc += weights[i];
                if (r <= acc) return items[i].Pos;
            }

            return items[items.Count - 1].Pos;
        }

        private bool IsDangerByImprint(string pos)
        {
            // Danger = az imprintben szerepel és energia értéke negatív (energianyelő)
            // imprint elem: "x,y,energy"
            if (_imprint == null) return false;

            foreach (string item in _imprint)
            {
                var s = item.Split(',');
                if (s.Length < 3) continue;

                string p = s[0].Trim() + "," + s[1].Trim();
                if (p == pos)
                {
                    if (int.TryParse(s[2], out int e))
                    {
                        if (e < movementCost) return true;
                    }
                }
            }
            return false;
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
