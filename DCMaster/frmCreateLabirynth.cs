using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DCMaster
{
    public partial class frmCreateLabirynth : Form
    {
        public labyrinth lab;
        public int LabSize;
        int numOfEnergySources;
        int numOfEnergySinks;
        int movement_cost;
        static Random rnd = new Random();
        IList<String> parameters = new List<String>();

        public frmCreateLabirynth()
        {
            InitializeComponent();
            string parameterFileName = Application.StartupPath + "\\parameters.txt";
            parameters = System.IO.File.ReadAllLines(parameterFileName);
            tbLabSize.Focus();
            tbLabSize.Select();
            movement_cost = int.Parse(parameters[3].Split(';')[1]);
            numOfEnergySinks = Properties.Settings.Default.numofenergysinks;
            tbNumberOfEnergySinks.Text = numOfEnergySinks.ToString();
            numOfEnergySources = Properties.Settings.Default.numofenergysources;
            tbNumberOfEnergySources.Text = numOfEnergySources.ToString();
            LabSize =Properties.Settings.Default.labsize; 
            tbLabSize.Text = LabSize.ToString();
        }


        private void bttnClose_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.numofenergysinks = numOfEnergySinks;
            Properties.Settings.Default.numofenergysources = numOfEnergySources;
            Properties.Settings.Default.labsize = LabSize;
            Properties.Settings.Default.Hostility=lab.Hostility.ToString();
            Properties.Settings.Default.Save();
            this.Close();
        }

        private void bttnCreateLab_Click(object sender, EventArgs e)
        {
            if (tbNumberOfEnergySinks.Text == "" | tbNumberOfEnergySources.Text == "" | tbLabSize.Text == "")
            {
                MessageBox.Show("Missing labirynth data", "Missing data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            lab = new labyrinth();
            LabSize = Convert.ToInt16(tbLabSize.Text);
            numOfEnergySources = Convert.ToInt16(tbNumberOfEnergySources.Text.Trim());
            numOfEnergySinks = Convert.ToInt16(tbNumberOfEnergySinks.Text.Trim());
            //if (LabSize * LabSize < (numOfEnergySinks + numOfEnergySources)) { MessageBox.Show("Too many sources and sinks"); return; }
            lab.createLabirynth(LabSize, numOfEnergySources, numOfEnergySinks, parameters); //create new labirynth
            label6.Text = "0";
            lblMin.Text = "-14";
            lblMax.Text = "14";
            lab.NumOfEnergySinks = numOfEnergySinks;
            lab.NumOfEnergySources = numOfEnergySources;
            Hostility host = new Hostility(lab, movement_cost);
            float hstl = host.ComputeStaticHostility();
            lab.Hostility = hstl;
            grp_static_hostility.Text = "Static hostility: " + hstl.ToString("##.##");
            int trvalue = (int)(hstl*10);
            if (trvalue > 140)
            {
                trackBar1.Value = 140;
            }
            else if (trvalue < -140)
            {
                trackBar1.Value = -140;
            }
            else
            {
                trackBar1.Value = trvalue;
                tbLabSize.Text = lab.Size.ToString();
            }
            grp_static_hostility.Visible = true;
            tbLab.Text = showLab();
            setupSums();
            this.AcceptButton = bttnClose;
        }

        void setupSums()
        {
            float sSource = 0;
            float sSink = 0;
            float sempty = 0;
            for (int i = 0; i < lab.Size; i++)
            {
                for (int j = 0; j < lab.Size; j++)
                {
                    if (lab.Fields[i, j] < movement_cost) sSink += lab.Fields[i, j];
                    if (lab.Fields[i, j] > 0) sSource += lab.Fields[i, j];
                    if (lab.Fields[i, j] == movement_cost) sempty += lab.Fields[i, j];
                }
            }
            sempty = sempty - movement_cost * (sSource + sSink);
            float numoffields = lab.Size * lab.Size;
            float sumemptyfield = sempty / numoffields;
            lblEmptyEnergySum.Text = "Empty fields energy sum: " + sempty;
            float summSource = sSource;
            lblSourceEnergySum.Text = "Energy source fields sum: " + sSource;
            float sumSink = sSink;
            lblSinkEnergySum.Text = "Energy sink fields sum: " + sSink;
            lblSumEnergy.Text ="Energy sum of entire lab: " + (sempty + sSource + sSink);
        }

        string showLab()
        {
            string labir = "Num of energy sources: " + lab.NumOfEnergySources + Environment.NewLine + "Num of energy sinks: " + lab.NumOfEnergySinks + Environment.NewLine +  "Lab size: " + lab.Size + " x " + lab.Size + Environment.NewLine + "---------------------------------------" + Environment.NewLine;

            int r = lab.Size;
            for (int i = 0; i < r; i++)
            {
                for (int j = 0; j < r; j++)
                {
                    if (lab.Fields[i, j] != -1)
                    {
                        labir += "Fields[" + i + "," + j + "] = " + lab.Fields[i, j] + Environment.NewLine;
                    }
                }

            }
            return labir;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            trackBar1.Value = (int)lab.Hostility; //(int)(ComputeHostility());
        }

        private void bttnLoadLab_Click(object sender, EventArgs e)
        {
            lab = new labyrinth();
            OpenFileDialog of = new OpenFileDialog();
            of.Filter = "labyrinths|*.lab";
            if (of.ShowDialog() == DialogResult.OK) 
            {
                string[] lines= File.ReadAllLines(of.FileName);
                //IEnumerable<string> lines = File.ReadLines(of.FileName);
                string s=lines.First().Trim();
                numOfEnergySinks = int.Parse(s.Split(',')[0].Split(':')[1]);
                numOfEnergySources = int.Parse(s.Split(',')[1].Split(':')[1]);
                LabSize = int.Parse(s.Split(',')[2].Split(':')[1]);
                tbLabSize.Text=LabSize.ToString();
                tbNumberOfEnergySinks.Text=numOfEnergySinks.ToString(); 
                tbNumberOfEnergySources.Text=numOfEnergySources.ToString();
                string[] lins = new string[lines.Length-1];
                int k = lines.Length-1;
                for (int i = 0; i < k; i++) { lins[i] = lines[i+1]; }
                lab.loadCreatedLabyrinth(lins, LabSize, numOfEnergySources, numOfEnergySinks, parameters);
                tbLab.Text = showLab();
                label6.Text = "0";
                lblMin.Text = "-100";
                lblMax.Text = "100";
                Hostility host = new Hostility(lab,movement_cost);
                lab.Hostility = host.ComputeStaticHostility();
                float hstl = lab.Hostility;
                grp_static_hostility.Text = "Static hostility: " + hstl.ToString();
                int trvalue = (int)(hstl);

                if (trvalue > 100)
                {
                    trackBar1.Value = 100;
                }
                else if (trvalue < -100)
                {
                    trackBar1.Value = -100;
                }
                else
                {
                    trackBar1.Value = trvalue;
                    tbLabSize.Text = lab.Size.ToString();
                }
                grp_static_hostility.Visible = true;
                setupSums();
            }
        }
    }
}
