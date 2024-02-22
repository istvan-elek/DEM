using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace DCMaster
{
    public partial class frmCreateWorkers : Form
    {
        int numOfWorkers;
        int initial_worker_energy;
        //int Gold_energy;
        int movement_costs;
        //int Trap_energy;
        //int Wumpus_energy;
        int Replication_energy_level;
        string startPos;
        IList<string> parameters=new List<string>();
        public Dictionary<Int32, worker> wk = new Dictionary<Int32, worker>();
        labyrinth lab;
        Boolean learn;


        public frmCreateWorkers(string startP, labyrinth labi, Boolean learning)
        {
            string parameterFileName = Application.StartupPath + @"\parameters.txt";
            InitializeComponent();
            parameters = System.IO.File.ReadAllLines(parameterFileName);
            tbNumOfWorkers.Text = Properties.Settings.Default.initialnumofworkers.ToString();
            tbNumOfWorkers.Focus();
            tbNumOfWorkers.Select();
            startPos = startP;
            bttnCreateNewWorkers.Text = "Create " + numOfWorkers + " workers";
            this.lab = labi;
            learn= learning;
        }


        private void bttnQuit_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.initialnumofworkers = int.Parse(tbNumOfWorkers.Text);
            Properties.Settings.Default.Save();
            dgvWorkers.DataSource = null;
            this.Dispose();
            this.Close();
        }

        private void bttnCreateNewWorkers_Click(object sender, EventArgs e)
        {
            if (tbNumOfWorkers.Text == "")
            {
                MessageBox.Show("Missing number of workers", "Missing number of workers", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            numOfWorkers = Convert.ToInt16(tbNumOfWorkers.Text);
            Random rand = new Random();
            initial_worker_energy = Convert.ToInt16(parameters[0].Split(';')[1]);
            movement_costs = Convert.ToInt16(parameters[4].Split(';')[1]);
            Replication_energy_level = Convert.ToInt16(parameters[5].Split(';')[1]);
            for (int i = 0; i < numOfWorkers; i++)
            {
                wk[i] = new worker(lab, i, learn, parameters);
                wk[i].Energy = initial_worker_energy;
                wk[i].Parent = "";
                //wk[i].CurrentPosition = startPos;
            }
            //tbNumOfWorkers.Text = "";
            loadWorkers2Dgv();
            this.AcceptButton = bttnQuit;
        }

        void loadWorkers2Dgv()
        {
            dgvWorkers.Columns.Clear();
            dgvWorkers.Columns.Add("id", "id");
            dgvWorkers.Columns.Add("Energy", "Energy");
            //dgvWorkers.Columns.Add("Position", "Position");
            dgvWorkers.Columns.Add("Parent", "Parent");
            for (int i=0; i<numOfWorkers; i++) 
            {
                dgvWorkers.Rows.Add(wk[i].ID, wk[i].Energy, wk[i].CurrentPosition, wk[i].Parent);
            }
        }
      

        private void tbNumOfWorkers_TextChanged(object sender, EventArgs e)
        {
            bttnCreateNewWorkers.Text = "Create " + tbNumOfWorkers.Text + " workers";
        }


        private void dgvWorkers_DataSourceChanged(object sender, EventArgs e)
        {
            lblWorkercount.Text = "Worker count: " + (dgvWorkers.Rows.Count-1).ToString();
        }
    }
}
