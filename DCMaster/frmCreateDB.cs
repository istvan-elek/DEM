using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace DCMaster
{
    public partial class frmCreateDB : Form
    {
        public string dbName;
        int initial_worker_energy; //=100;
        int gold_energy;  // = 1000;
        int trap_energy;  // = -400;
        int wumpus_energy;  // = -1000;
        int movement_costs;  // = -1;
        int replication_energy_level;  // = 5000;
        int replication_rate;  // = 2;




        void loadInitParameters()
        {
            string fileName = Application.StartupPath + @"\init.par";
            string[] ParameterString = System.IO.File.ReadAllLines(fileName);
            foreach(string line in ParameterString)
            {
                switch (line.Split(';')[0])
                {
                    case "initial_worker_energy":
                        initial_worker_energy = Convert.ToInt16(line.Split(';')[1]);
                        break;
                    case "gold_energy":
                        gold_energy = Convert.ToInt16(line.Split(';')[1]);
                        break;
                    case "trap_energy":
                        trap_energy = Convert.ToInt16(line.Split(';')[1]);
                        break;
                    case "wumpus_energy":
                        wumpus_energy = Convert.ToInt16(line.Split(';')[1]);
                        break;
                    case "movement_costs":
                        movement_costs = Convert.ToInt16(line.Split(';')[1]);
                        break;
                    case "replication_energy_level":
                        replication_energy_level = Convert.ToInt16(line.Split(';')[1]);
                        break;
                    case "replication_rate":
                        replication_rate = Convert.ToInt16(line.Split(';')[1]);
                        break;
                }
            }
        }

   

        private void bttnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
