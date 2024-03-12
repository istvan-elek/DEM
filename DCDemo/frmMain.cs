using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using Npgsql;

namespace DCDemo
{
    public partial class frmMain : Form
    {
        public Dictionary<int, string> fieldValue = new Dictionary<int, string>()
        {
            {0, "Wumpus"}, {1, "Trap"}, {2, "Breeze"}, {3, "Stench" }, {4,"Empty field" }, {5, "Glitter"}, {6,"Gold"}
        };

        int LabSize=10; // labirynth size: 10x10 default
        int numberOfWorkers=1; //number of workers (default is 1)
        int numberOfWorkersMAX = 5; //maximum number of workers, 400
        int numberOfWumpus = 1;
        int numberOfWumpusTrapsGoldMAX = 5;   //maximum number of wumpus, Traps and gold, 400
        int labSizeMAX = 50;  //lab size maximum 655 x 655, size must be less then 656, because datagrid can not display so many columns
        labirynth lab = new labirynth();
        Boolean newLabirynth = true; // it is a flag, it is true, if you are going to create new labirynth


        public frmMain()
        {
            InitializeComponent();
            toolStrip1.ImageScalingSize = new Size(40, 40);
            dgv.Visible = true;
            tsbttnLegend.Visible = true;
            stLblLastField.Visible = true;  // it contains the last field, where process stopped, visible, in case of no database connection
            stLblGotcha.Visible = true; // it is true, if gold has been catched, visible, if no database connection
            stLbLLive.Visible = true;  // it is true, if process terminated, but worker is taying alive, visible, if no database connection  
            stLblWorkerPathCount.Visible = true;  // number of steps, visible, if no database connection
        }


        DataTable loadLabirynth2DataTable(int gridSize)  // loads a created labirynth to a datagridview. GridView can not display more then 655 columns, thus lab size must be less then 655*655, if it is set up autogenerate
        {
            DataTable dt = new DataTable();
            // create columns
            for (int i = 0; i < lab.Size; i++)
            {
                dt.Columns.Add();
            }
            for (int j = 0; j < lab.Size; j++)
            {
                DataRow row = dt.NewRow();
                for (int i = 0; i < lab.Size; i++)
                {
                    row[i] = "";
                }
                dt.Rows.Add(row);
            }
            return dt;
        }



        void Emphasizer()   //emphasize empty fields plus wumpus, Trap, gold and their adjacent fields with different colors
        {
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                for (int j = 0; j < dgv.Columns.Count; j++)
                {
                    dgv.Rows[i].Cells[j].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    switch (fieldValue[lab.Fields[j, i]])
                    {
                        case "Wumpus":
                            dgv.Rows[i].Cells[j].Style.BackColor = Color.DarkGray;
                            break;
                        case "Trap":
                            dgv.Rows[i].Cells[j].Style.BackColor = Color.Red;
                            break;
                        case "Breeze":
                            dgv.Rows[i].Cells[j].Style.BackColor = Color.LightSalmon;
                            break;
                        case "Stench":
                            dgv.Rows[i].Cells[j].Style.BackColor = Color.LightGray;
                            break;
                        case "Empty field":
                            dgv.Rows[i].Cells[j].Style.BackColor = Color.LightGreen;
                            break;
                        case "Glitter":
                            dgv.Rows[i].Cells[j].Style.BackColor = Color.Goldenrod;
                            break;
                        case "Gold":
                            dgv.Rows[i].Cells[j].Style.BackColor = Color.DarkGoldenrod;
                            break;
                    }
                }
            }
        }



        private void bttnPlay_Click(object sender, EventArgs e)   // event handler for Play_click button, which starts workers
        {
            int n;
            bool isNumeric = int.TryParse(tstbNumOfWump.Text, out n);
            if (isNumeric)
            {
                numberOfWumpus = n;
                if (n < 1 || n > numberOfWumpusTrapsGoldMAX)
                {
                    MessageBox.Show("Number of workers must be between 1 and " + numberOfWumpusTrapsGoldMAX, "Improper value", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            isNumeric = int.TryParse(tsTbNumOfWorkers.Text, out n);
            if (isNumeric)
            {
                numberOfWorkers = n;
                if (n < 1 || n > numberOfWorkersMAX)
                {
                    MessageBox.Show("Number of workers must be between 1 and " + numberOfWorkersMAX, "Improper value", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            isNumeric = int.TryParse(tsTbLabSize.Text, out n);
            if (isNumeric)
            {
                LabSize = n;
                if (n < 1 || n > labSizeMAX)
                {
                    MessageBox.Show("Size of labirynth must be between 1 and " + labSizeMAX, "Improper size", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            statusStrip1.Visible = true;
            dgv.DataSource = loadLabirynth2DataTable(LabSize);
            dgv.ClearSelection();
            if (Convert.ToInt16(tsTbStartLoc.Text.Split(',')[0]) > lab.Size-1 || Convert.ToInt16(tsTbStartLoc.Text.Split(',')[1]) > lab.Size-1)
            {
                MessageBox.Show("Start position is out of labirynth size", "Improper start position", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Emphasizer();
            startWorkers(numberOfWorkers);
            statusStrip1.Visible = true;
        }
 


        private void startWorkers(int numOfWorkers)  // start running workers with no database connection
        {
            worker[] wk = new worker[numOfWorkers];
            for (int i = 0; i < numOfWorkers; i++)
            {
                wk[i] = new worker(lab);
                string startLocation = tsTbStartLoc.Text;
                wk[i].StartLocation = startLocation;
                int row = Convert.ToInt16(startLocation.Split(',')[1]);
                int col = Convert.ToInt16(startLocation.Split(',')[0]);
                dgv.Rows[row].Cells[col].Style.BackColor = Color.LightBlue;
                while (wk[i].Live)
                {
                    wk[i].moveNext();
                    if (wk[i].Gotcha) 
                    { 
                        wk[i].goBack2Start();
                        break;
                    }
                    if (!wk[i].Live)
                    {
                        break;
                    }
                }
               showVisitedFields(wk[i]);
            }
            if (numOfWorkers > 1)
            {
                MessageBox.Show("More than one workers were started, so each fields display more than one worker's path, thus the image can become confused", "More than one workers were started", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        void showVisitedFields(worker wk) // show visited fields with numbers. This is only for one worker started without db connection
        {
            int k = 0; ;
            foreach (string item in wk.WorkerPath)
            {
                int x = Convert.ToInt16(item.Split(',')[0]);
                int y = Convert.ToInt16(item.Split(',')[1]);
                if (dgv.Rows[y].Cells[x].Value.ToString() == "") 
                { 
                    dgv.Rows[y].Cells[x].Value =  k.ToString();
                }
                else 
                { 
                    dgv.Rows[y].Cells[x].Value = dgv.Rows[y].Cells[x].Value.ToString() + "-" + k.ToString(); 
                }
                k++;
                stLblLastField.Text = "Final field: " + wk.WorkerPath[wk.WorkerPath.Count - 1];
                int row = Convert.ToInt16(wk.WorkerPath[wk.WorkerPath.Count - 1].Split(',')[1]);
                int col = Convert.ToInt16(wk.WorkerPath[wk.WorkerPath.Count - 1].Split(',')[0]);
                dgv.Rows[row].Cells[col].Selected = true;
                stLbLLive.Text = "     Live: " + wk.Live.ToString();
                stLblLastEnergyLevel.Text = "  Energy level: " + wk.Energy.ToString();

                if (wk.Live)
                {
                    stLbLLive.BackColor = Color.Green;
                }
                else
                {
                    stLbLLive.BackColor = Color.Black;
                    stLbLLive.ForeColor = Color.White;  
                }
                stLblGotcha.Text = "   Gotcha: " + wk.Gotcha.ToString();
                if (wk.Gotcha)
                {
                    stLblGotcha.BackColor = Color.Green;
                    stLblGotcha.ForeColor = Color.White;
                }
                else
                {
                    stLblGotcha.ForeColor = Color.Black;
                    stLblGotcha.BackColor = Control.DefaultBackColor;
                }
                stLblWorkerPathCount.Text = "   Worker path count: " + wk.WorkerPath.Count.ToString();
            }
        }


        private void bttnNewLabirynth_Click(object sender, EventArgs e)   // event handler for click New labirytn button, create new labirynth
        {
            this.Cursor = Cursors.WaitCursor;
            int n;
            Boolean isNumeric = int.TryParse(tsTbLabSize.Text, out n);
            if (isNumeric)
            {
                numberOfWorkers = n;
                    if (n < 1 || n > labSizeMAX)
                    {
                        this.Cursor = Cursors.Default;
                        MessageBox.Show("Size of labirynth must be between 1 and " + labSizeMAX, "Improper number of values", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
            }
            isNumeric = int.TryParse(tstbNumOfWump.Text, out n);
            if (isNumeric)
            {
                numberOfWumpus = n;
                    if (n < 1 || n > numberOfWumpusTrapsGoldMAX)
                    {
                        this.Cursor = Cursors.Default;
                        MessageBox.Show("Number of wumpus/Traps/gold must be between 1 and " + numberOfWumpusTrapsGoldMAX, "Improper value", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
            }
            LabSize = Convert.ToInt16(tsTbLabSize.Text);
            lab.createLabirynth(LabSize, 0, numberOfWumpus); //create new labirynth
            dgv.DataSource = null;
            dgv.DataSource = loadLabirynth2DataTable(LabSize);
            for (int i = 0; i < dgv.Columns.Count; i++)
            {
                dgv.Columns[i].Width = dgv.Rows[i].Height;
            }
            dgv.ClearSelection();
            Emphasizer();
            newLabirynth = true;
            this.Cursor = Cursors.Default;
            bttnPlay.Enabled = true;
        }


        private void tsbttnLegend_Click(object sender, EventArgs e)
        {
            legend Legend = new legend();
            int titleBarHeight = SystemInformation.ToolWindowCaptionHeight;
            Legend.Location = new Point(this.Location.X + this.Width - Legend.Width, this.Location.Y + this.Height - this.ClientRectangle.Height + toolStrip1.Height); 
            Legend.Show();
            this.Focus();
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }


    }
}
