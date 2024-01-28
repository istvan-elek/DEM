using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCDemo
{
    class labirynth
    {
        public Dictionary<string, int> fieldCode = new Dictionary<string, int>()

        {
            {"Wumpus",0}, {"Trap",1}, {"Breeze",2}, {"Stench",3}, {"Empty field",4}, {"Glitter",5}, {"Gold",6}
        };

        public void createLabirynth(int grsize, int labirID, int numOfWump)   //create a new labirynth with grsize size and labirynth ID, and fills its fields with empty content (no wumpus, no Trap, no gold)
        {
            _size = grsize;
            _fields = new int[_size, _size];
            _labId = labirID;
            _wumpusPosition = "";
            _TrapPosition = "";
            _goldPosition = "";
            _numOfWumpusTrapsGold = numOfWump;

            for (int i = 0; i < _size; i++)
            {
                for (int j = 0; j < _size; j++)
                {
                    _fields[j, i] = fieldCode["Empty field"];
                }
            }
            createObjects(numOfWump);
        }


        void createObjects(int NumberOfWumpus) //  creates special objects in the labirynth, such as wumpus, Trap and gold (in this case only one per type)
        {
            Random rnd = new Random();

            for (int inumw = 0; inumw < NumberOfWumpus; inumw++)
            {
                int xx = rnd.Next(_size);
                int yy = rnd.Next(_size);
                _fields[xx, yy] = fieldCode["Gold"];
                _goldPosition += xx + "," + yy + ";";
                if (xx < _size - 1) { _fields[xx + 1, yy] = fieldCode["Glitter"]; }
                if (xx > 0) { _fields[xx - 1, yy] = fieldCode["Glitter"]; }
                if (yy < _size - 1) { _fields[xx, yy + 1] = fieldCode["Glitter"]; }
                if (yy > 0) { _fields[xx, yy - 1] = fieldCode["Glitter"]; }

                int u = rnd.Next(_size);
                int v = rnd.Next(_size);
                _fields[u, v] = fieldCode["Trap"];
                _TrapPosition += u + "," + v + ";";
                if (u < _size - 1) { _fields[u + 1, v] = fieldCode["Breeze"]; }
                if (u > 0) { _fields[u - 1, v] = fieldCode["Breeze"]; }
                if (v < _size - 1) { _fields[u, v + 1] = fieldCode["Breeze"]; }
                if (v > 0) { _fields[u, v - 1] = fieldCode["Breeze"]; }

                int x = rnd.Next(_size);
                int y = rnd.Next(_size);
                _fields[x, y] = fieldCode["Wumpus"];
                _wumpusPosition += x + "," + y + ";";
                if (x < _size - 1) { _fields[x + 1, y] = fieldCode["Stench"]; }
                if (x > 0) { _fields[x - 1, y] = fieldCode["Stench"]; }
                if (y < _size - 1) { _fields[x, y + 1] = fieldCode["Stench"]; }
                if (y > 0) { _fields[x, y - 1] = fieldCode["Stench"]; }
            }
       }


        int[,] _fields;
        public int[,] Fields
        {
            get { return _fields; }
            set { _fields = value; }
        }

        static int _size;
        public int Size
        {
            get {return _size;}
            set { _size = value; }
        }

        string _wumpusPosition;
        public string WumpusPosition
        {
            get { return _wumpusPosition; }
            set { _wumpusPosition = value; }
        }

        string _TrapPosition;
        public string TrapPosition
        {
            get  { return _TrapPosition; }
            set { _TrapPosition = value; }
        }

        string _goldPosition;
        public string GoldPosition
        {
            get{ return _goldPosition; }
            set { _goldPosition = value; }
        }

        int _labId=0;
        public int LabId
        {
            get { return _labId;}
            set { _labId = value; }
        }

        int _numOfWumpusTrapsGold;
        public int numOfWumpusTrapsGold
        {
            get {  return _numOfWumpusTrapsGold; } 
            set { _numOfWumpusTrapsGold = value; }
        }

    }
}
