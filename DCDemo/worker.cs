using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Data;
//using Npgsql;

namespace DCDemo
{
    class worker
    {
        labirynth lab;
        static Random rnd = new Random(); //random number for neighbor choose
        //Npgsql.NpgsqlConnectionStringBuilder connsb = new NpgsqlConnectionStringBuilder();
        int initial_worker_energy=500;
        int gold_energy=5000;
        int movement_costs=-1;
        int trap_energy=-400;
        int wumpus_energy=-1000;
        //int replication_energy_level=5000;

        //public worker(Int32 WID, labirynth currentLab, NpgsqlConnectionStringBuilder cnsb)
        //{
        //    lab = currentLab;
        //    //dbTools dbtgetParameters = new dbTools(cnsb);
        //    DataTable dt = dbtgetParameters.getParametersTable(cnsb);
        //    initial_worker_energy = Convert.ToInt16(dt.Rows[0][1]);
        //    gold_energy = Convert.ToInt16(dt.Rows[1][1]);
        //    movement_costs = Convert.ToInt16(dt.Rows[2][1]);
        //    trap_energy = Convert.ToInt16(dt.Rows[3][1]);
        //    wumpus_energy = Convert.ToInt16(dt.Rows[4][1]);
        //    replication_energy_level = Convert.ToInt16(dt.Rows[5][1]);
        //    _ID = WID;
        //    _energy = initial_worker_energy;

        //}

        public worker(labirynth currentLab)
        {
            lab = currentLab;
            _energy = initial_worker_energy;
        }

        public worker(Int32 id)
        {
            _ID = id;
        }

        public worker(Int32 id, labirynth currentLab)
        {
            _ID = id;
            lab = currentLab;
        }


        private Int32 _ID;
        public Int32 ID
        {
            get { return _ID;}
            set { _ID = value; }
        }

        //private string _knowledge_empty_fields;
        //public string Knowledge_empty_fields
        //{
        //    get { return _knowledge_empty_fields; }
        //    set { _knowledge_empty_fields = value; }
        //}

        private string _knowledge_wumpus_fields="";
        public string Knowledge_wumpus_fields
        {
            get { return _knowledge_wumpus_fields; }
            set { _knowledge_wumpus_fields = value; }
        }

        private string _knowledge_trap_fields="";
        public string Knowledge_trap_fields
        {
            get { return _knowledge_trap_fields; }
            set { _knowledge_trap_fields = value; }
        }

        private string _knowledge_gold_fields="";
        public string Knowledge_gold_fields
        {
            get { return _knowledge_gold_fields; }
            set { _knowledge_gold_fields = value; }
        } 


        List<string> _workerPath=new List<string>(); // worker path is series of number pairs, which contains x,y coordinates of the fields of labirynth
        public List<string> WorkerPath
        {
            get { return _workerPath;}
        }


        string _currentPosition;
        public string CurrentPosition
        {
            get { return _currentPosition; }
        }


        private string _startLocation;
        public string StartLocation
        {
            get { return _startLocation; }
            set
            {
                _startLocation = value;
                _currentPosition = _startLocation;
                int x = Convert.ToInt16(_currentPosition.Split(',')[0]);
                int y = Convert.ToInt16(_currentPosition.Split(',')[1]);
                _workerPath.Add(_currentPosition);
                _fieldValue = lab.Fields[x, y];
                if (_fieldValue == 0) { _live = false; return; }  //wumpus has found
                if (_fieldValue == 1) { _live = false; return; }  // Trap has found
                if (_fieldValue == 6) { _gotcha = true; return; } // gold has found
            }
        }

        private int _fieldValue;
        public int FieldValue
        {
            get { return _fieldValue;}
        }


        Boolean _live = true;
        public Boolean Live
        {
            get { return _live;}
            set { _live = value; }
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

        Boolean _gotcha = false;
        public Boolean Gotcha
        {
            get { return _gotcha; }
        }

        Int32 _parent;
        public Int32 Parent
        {
            get { return _parent; }
            set { _parent = value; }
        }


        int _replication_energy_level;
        public int Replication_Energy_level
        {
            get { return _replication_energy_level; }
            set
            {
                _replication_energy_level = value;
            }
        }

   

        public void moveNext()  //moves the next position in labirynth. Find neighbors of the current field, and select a field randomly from the neighbors
        {
            List<string> currentNeighbours = new List<string>(); 
            currentNeighbours =  getNeighbors(_currentPosition);
            string newPosition = currentNeighbours[rnd.Next(0, currentNeighbours.Count)];
            if (_knowledge_wumpus_fields.Contains(newPosition)) {newPosition = currentNeighbours[rnd.Next(0, currentNeighbours.Count)];}
            if (_knowledge_trap_fields.Contains(newPosition)) {newPosition = currentNeighbours[rnd.Next(0, currentNeighbours.Count)];}
            int x = Convert.ToInt16(newPosition.Split(',')[0]);
            int y = Convert.ToInt16(newPosition.Split(',')[1]);
            _workerPath.Add(newPosition);
            _fieldValue = lab.Fields[x, y];
            _currentPosition = newPosition;

            if (_fieldValue == 4)  // this is an empty field
            {
                _energy += movement_costs;
                if (_energy < 1) { _live = false; }
                return;
            } 
            if (_fieldValue == 0) // there is a wumpus here
            {
                _energy += movement_costs;
                _energy += wumpus_energy;
                if (_energy < 1) { _live = false; }
                return; 
            }  
            if (_fieldValue == 1)  // there is a Trap here
            {
                _energy += movement_costs;
                _energy +=trap_energy;
                if (_energy < 1) { _live = false; }
                return; 
            }
            if (_fieldValue == 6)  // there is gold here
            {
                _energy += gold_energy;
                _energy += movement_costs;
                _gotcha = true;
                return; 
            } 
        }


        public List<string> getNeighbors(string currentPos)  //computes adjacent fields
        {
            List<string> neighbors = new List<string>();
            int x = Convert.ToInt16(currentPos.Split(',')[0]);
            int y = Convert.ToInt16(currentPos.Split(',')[1]);
            if (y > 0) { neighbors.Add(x + "," + (y - 1)); } 
            if (y < lab.Size-1) { neighbors.Add(x + "," + (y + 1));}
            if (x > 0) { neighbors.Add((x - 1) + "," + y); }
            if (x < lab.Size-1) {neighbors.Add((x + 1) + "," + y); }
            return neighbors;
        }


        public void goBack2Start()  //  returns to the starting position. If a worker lives, the safe fields are known for it. It is its own knowledge for safe movement
        {
            foreach (string item in _workerPath)
            {
                _currentPosition = item;
            }

        }

    }
}
