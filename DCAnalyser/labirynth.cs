using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCAnalyser
{
    public class labirynth
    {
        int numOfSources;
        int numOfSinks;
        int initial_worker_energy;
        int source_energy_max;
        int sink_energy_max;
        int movement_costs;

        public void createLabirynth(int grsize, int numberOfSources, int numberOfSinks, IList <String> par)   //create a new labirynth with grsize size, and fills its fields with 0 (empty content) 
        {
            initial_worker_energy = Convert.ToInt16(par[0].Split(';')[1]);
            source_energy_max = Convert.ToInt16(par[1].Split(';')[1]);
            sink_energy_max = Convert.ToInt16(par[2].Split(';')[1]);
            movement_costs = Convert.ToInt16(par[3].Split(';')[1]);
            numOfSources = numberOfSources;
            numOfSinks = numberOfSinks;
            _size = grsize;
            _fields = new int[_size, _size];

            for (int i = 0; i < _size; i++)
            {
                for (int j = 0; j < _size; j++)
                {
                    _fields[j, i] = movement_costs;
                }
            }
            createObjects();
        }


        void createObjects() //  creates special objects in the labirynth, such as energy_sources and energy_sinks
        {
            Random rnd = new Random();
            Dictionary<int,string> positions = new Dictionary<int,string>(); // nem lehet két egyforma pozíció, ezért dictionary

            int inum = 0;
            while (inum < numOfSources) // energiaforrások generálása
            {
                int xx = rnd.Next(_size);
                int yy = rnd.Next(_size);
                positions.Add(inum, xx.ToString() + "," + yy.ToString());
                inum=positions.Count;
            }
            for (int i = 0; i<inum; i++)
            {                
                string place = positions[i].ToString();
                int val = rnd.Next(initial_worker_energy, source_energy_max);
                Fields[int.Parse(place.Split(',')[0]), int.Parse(place.Split(',')[1])] = val;
            }
            while (inum < numOfSources + numOfSinks)   // Energianyelők generálása
            {
                int xx = rnd.Next(_size);
                int yy = rnd.Next(_size);
                positions.Add(inum, xx.ToString() + "," + yy.ToString());
                inum = positions.Count;
            }
            for (int i = 0; i < numOfSinks; i++)
            {
                string place = positions[i+numOfSources].ToString();
                int val = rnd.Next(sink_energy_max, -initial_worker_energy);
                Fields[int.Parse(place.Split(',')[0]), int.Parse(place.Split(',')[1])] = val; 
            }
            _Position= positions;
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

        Dictionary<int, string> _Position;
        public Dictionary<int, string> Position
        {
            get { return _Position; }
            set { Position = value; }
        }

        Dictionary<string, Boolean> _disable; // a labirintus energiaforrás mezőinek rendelkezésre állását mutatja. Miután egy mező energiája el lett fogyasztva, néhány lépésig a mező disable=true lesz, vagyis nem elérhető. A nem elérhető mezők lesznek tárolva ebben a dictionaryben. Miután újra elérhetővé válik, ez a listaelem törölve lesz.
        public Dictionary<string, Boolean> Disable
        {
            get { return _disable; }
            set { _disable = value; }
        }


    }
}
