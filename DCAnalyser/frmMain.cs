using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
//using Npgsql;
using System.Timers;
using System.Drawing.Drawing2D;
using System.Runtime.CompilerServices;
using System.Data.SQLite;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.LinkLabel;

namespace DCAnalyser
{
    public partial class frmMain : Form
    {
        SQLiteConnectionStringBuilder cnsb = new SQLiteConnectionStringBuilder();
        SQLiteConnectionStringBuilder cnsbAdmin = new SQLiteConnectionStringBuilder();
        string appfolder;// = Properties.Settings.Default.lastFolder;
        //System.Windows.Forms.FormWindowState state; 
        string dbFolder;
        string reportFolder;
        //List<Int32> currentWkrNumber;
        List<Int32> timeSeries = new List<Int32>(0);
        int labSize;
        string[] reportNames;
        string lastFolder;

        public frmMain()
        {
            InitializeComponent();
            this.Text = "DC Analyser, v_" + Application.ProductVersion.ToString();
            if (appfolder == null)
            {
                appfolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\DC\\";
            }
            dbFolder = appfolder + "databases\\";
            lastFolder = dbFolder;
            reportFolder = appfolder + "reports\\";
            if (Directory.Exists(reportFolder))
            {
                reportNames = Directory.GetFiles(reportFolder, "*.report");
                foreach (string item in reportNames)
                {
                    tscmbBrowseReport.Items.Add(Path.GetFileNameWithoutExtension(item));
                }
                this.WindowState = Properties.Settings.Default.winstate;
            }
            else { MessageBox.Show("Missing default folder (DC). Choose one"); }
        }

        private void databaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();
            lastFolder = Properties.Settings.Default.lastFolder;
            of.InitialDirectory = lastFolder;   //appfolder; //dbFolder;
            of.Filter = "Sqlite database file|*.s3db|All files|*.*";
            if (of.ShowDialog()==DialogResult.OK)
            {
                dbConnection(of.FileName);
                SQLanalyzeToolStripMenuItem.Enabled = true;
                generationsToolStripMenuItem.Enabled = true;
                selectionsToolStripMenuItem.Enabled=true;
                diagramsToolStripMenuItem.Enabled=true;
                Properties.Settings.Default["lastFolder"] = Path.GetDirectoryName(of.FileName); //appfolder;
                Properties.Settings.Default.Save();
            }
            this.Text = "DC Analyser --> " + Path.GetFileNameWithoutExtension(of.FileName);
        }



        void dbConnection(string fileName)
        {
            cnsb.DataSource = fileName;
            bsWorkers.DataSource = loadTableData("select * from workers order by id");
            dgvWorkers.DataSource = bsWorkers;
            bnWorkers.BindingSource= bsWorkers;
            //bnWorkers.CountItem.Text = "pmpom";
            tslblWorkers.Text = "Workers";
            bsIteration.DataSource = loadTableData("select * from iteration");
            dgvIteration.DataSource=bsIteration;
            bnIteration.BindingSource= bsIteration;
            tslblIteration.Text = "Iteration";           
            DataTable dtl = loadTableData("select * from labirynth"); 
            string sz= dtl.Rows[dtl.Rows.Count - 1][2].ToString();
            labSize = int.Parse(sz);
            dtl.Rows[dtl.Rows.Count-1].Delete();
            bsLabs.DataSource =dtl;
            dgvLabs.DataSource = bsLabs;
            bnLabs.BindingSource= bsLabs;
            tslblLabirynth.Text = "Labirynth";
        }

        DataTable loadTableData(string sqlCommand)
        {
            this.Cursor = Cursors.WaitCursor;
            DataTable dt = new DataTable();
            using (SQLiteConnection cnn = new SQLiteConnection(cnsb.ConnectionString))
            {
                cnn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(sqlCommand,cnn))
                {
                     try
                    {
                        SQLiteDataReader dr = cmd.ExecuteReader();
                        dt.Load(dr);
                        this.Cursor = Cursors.Default;
                        return dt;
                    }
                    catch (Exception err)
                    {
                        throw(err);
                    }
                }
            }
        }

  

        private void sQLWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSql SqlCommand = new frmSql(cnsb);
            SqlCommand.Show();
        }
      

        private void displayGatheredEnergyToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            DisplayCharts frmChart = new DisplayCharts(cnsb, appfolder, "energy", "1", 0);
            frmChart.Show();
        }

        private void displayResultOfAnySqlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DisplayCharts frmChart = new DisplayCharts(cnsb, appfolder, "any", "1", 0);
            frmChart.Show();
        }


        private void displayEntropyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DisplayCharts frmChart = new DisplayCharts(cnsb, appfolder, "entropy", "1", 0);
            frmChart.Show();
        }


        private void displayPopulationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DisplayCharts frmChart = new DisplayCharts(cnsb, appfolder, "poptotal", "1", 0);
            frmChart.Show();
        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default["winstate"] = this.WindowState;
            Properties.Settings.Default.Save();
            Application.Exit();
        }
 


        private void compareMissionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string[] files = Directory.GetFiles(dbFolder, "*.s3db");
            frmIterationCompare frmCompMissions = new frmIterationCompare(files, appfolder);
            frmCompMissions.Show();
        }


        private void displayFittnessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DisplayCharts frmChart = new DisplayCharts(cnsb, appfolder, "fittness", "1", 0);
            frmChart.Show();
        }

        private void displayDiscoveredFieldsOfSelectedWorkerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvWorkers.SelectedRows.Count > 0)
            {
                Int32 worker_id = Convert.ToInt32(dgvWorkers.SelectedRows[0].Cells["id"].Value);
                DataTable dtlab = loadTableData("select * from labirynth");
                dtlab.Rows[dtlab.Rows.Count - 1].Delete();
                string sqlCommand = "SELECT worker_path FROM workers WHERE id=" + worker_id;
                DataTable dtwork = loadTableData(sqlCommand);
                if (dtwork.Rows.Count > 0)
                {
                    this.Cursor = Cursors.WaitCursor;
                    Image img = path2Image(dtwork);
                    Image imgLab = lab2Bmp(dtlab);
                    frmPicture2 frmPic = new frmPicture2("Discovered paths visited by worker:" + worker_id, img, imgLab, dtlab, appfolder);
                    this.Cursor = Cursors.Default;
                    frmPic.Show();
                }
                else
                {
                    MessageBox.Show("Worker_path is empty");
                }
            }
            else
            {
                MessageBox.Show("Select a worker, please", "No selected worker", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void displayDiscoveredFieldOfAllWorkersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataTable dtlab = loadTableData("select * from labirynth");
            string sqlCommand = "SELECT worker_path FROM workers";
            DataTable dtwork = loadTableData(sqlCommand);
            Image img = allWorkerspath2Image(dtwork);
            Image imgLab = lab2Bmp(dtlab);
            frmPicture2 frmPic = new frmPicture2("Discovered path for all living workers", img, imgLab, dtlab, appfolder);
            frmPic.Show();
            return;
        }

        private void displayDiscoveredFieldsOfAGivenGenerationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSelectGeneration selectGeneration = new frmSelectGeneration();
            if (selectGeneration.ShowDialog() == DialogResult.OK)
            {
                DataTable dtlab = loadTableData("select * from labirynth"); 
                Image imglab = lab2Bmp(dtlab);
                labSize = imglab.Width;
                //string where = selectGeneration.kvantor + selectGeneration.generation;
                string gen = "";
                for (int i = 0; i< int.Parse(selectGeneration.generation); i++)
                {
                    gen += ",%";
                }
                string sqlCommand = "select worker_path from workers where parents like '" + gen +"'";
                DataTable dt = loadTableData(sqlCommand);
                if (selectGeneration.displayData) { bsLogBook.DataSource = dt; }
                if (dt.Rows.Count > 0)
                {
                    this.Cursor = Cursors.WaitCursor;
                    Image img = path2Image(dt);
                    frmPicture2 frmPic = new frmPicture2("Discovered paths from generation #" + selectGeneration.generation, img, imglab, dtlab, "");
                    this.Cursor = Cursors.Default;
                    frmPic.Show();
                    if (selectGeneration.displayData)
                    {
                        Form Dispform = new Form();
                        DataGridView dgv = new DataGridView();
                        sqlCommand = "select id, parents, worker_path from workers where parents like '" + gen + "'";
                        DataTable dt2 = loadTableData(sqlCommand);
                        Dispform.Width = this.Width/2;
                        dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
                        dgv.Dock = DockStyle.Fill;
                        Dispform.Controls.Add(dgv);
                        BindingSource bs = new BindingSource();
                        BindingNavigator bn = new BindingNavigator(bs);
                        bn.BindingSource = bs;
                        bn.AddNewItem.Visible = false;
                        bn.DeleteItem.Visible = false;
                        bs.DataSource = dt2;
                        dgv.DataSource = bs;
                        Dispform.Controls.Add(bn);
                        Dispform.Text = "Generations from #" + selectGeneration.generation;
                        Dispform.Show();
                    }
                }
                else
                {
                    MessageBox.Show("Worker_path is empty");
                }
            }
        }


        Image allWorkerspath2Image(DataTable dtPath)
        {
            Bitmap bmp = new Bitmap(labSize, labSize);
            int[,] imageMatrix = new int[labSize, labSize];
            foreach(DataRow row in dtPath.Rows)
            {
                string[] wpath = row[0].ToString().Split(';');
                string[] wp = wpath.Take(wpath.Length).ToArray();
                foreach (string line in wp)
                {
                    if (line != "")
                    {
                        int x = 0;
                        int y = 0;
                        int xx = Convert.ToInt16(line.ToString().Split(',')[0]);
                        Math.DivRem(xx + labSize, (labSize), out int x1);
                        if (x1 > 0) x = x1;
                        else x = labSize + x1;
                        //x = x1;
                        int yy = Convert.ToInt16(line.ToString().Split(',')[1]);
                        Math.DivRem(yy + labSize, (labSize), out int y1);
                        if (y1 > 0) y = y1;
                        else y = labSize + y1;
                        imageMatrix[x - 1, y - 1] += 1;
                    }
                }               
            }
            int rgb;
            for (int i = 0; i < labSize; i++)
            {
                for (int j = 0; j < labSize; j++)
                {
                    rgb = imageMatrix[i, j];
                    if (rgb > 255) rgb = 255;
                    bmp.SetPixel(i, j, Color.FromArgb(0, rgb, rgb));
                }
            }
            return bmp;
        }



        Image path2Image(DataTable dtPath)
        {
            string[] wpath = dtPath.Rows[0][0].ToString().Split(';');
            Bitmap bmp = new Bitmap(labSize, labSize);
            int[,] imageMatrix = new int[labSize, labSize];
            //int x=0;
            //int y = 0;
            string[] wp = wpath.Take(wpath.Length).ToArray(); 

            foreach (string row in wp)
            {
                if (row != "")
                {
                    int x = 0;
                    int y = 0;
                    int xx = Convert.ToInt16(row.ToString().Split(',')[0]);
                    Math.DivRem(xx + labSize, (labSize), out int x1);
                    if (x1 > 0) x = x1;
                    else x = labSize + x1;
                    //x = x1;
                    int yy = Convert.ToInt16(row.ToString().Split(',')[1]);
                    Math.DivRem(yy + labSize, (labSize), out int y1);
                    if (y1 > 0) y = y1;
                    else y = labSize + y1;
                    imageMatrix[x-1, y-1] += 1;
                }
            }
            int rgb;
            for (int i = 0; i < labSize; i++)
            {
                for (int j = 0; j < labSize; j++)
                {
                    rgb = imageMatrix[i, j];
                    if (rgb > 255) rgb = 255;
                    bmp.SetPixel(i, j, Color.FromArgb(0, rgb, rgb));
                }
            }

            return bmp;
        }

        Bitmap lab2Bmp(DataTable dt)
        {
            Bitmap lBmp = new System.Drawing.Bitmap(labSize, labSize, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            Color clrEmpty = Color.Black;
            Color source = Color.Red;
            Color sink = Color.RoyalBlue;

            for (int i = 0; i < labSize; i++)
            {
                for (int j = 0; j < labSize; j++)
                {
                    lBmp.SetPixel(j, i, clrEmpty);
                }
            }

            for (int j = 0; j < dt.Rows.Count - 1; j++)
            {
                string[] pos = dt.Rows[j][1].ToString().Split(',');
                if (int.Parse(dt.Rows[j][2].ToString()) < 0) lBmp.SetPixel(int.Parse(pos[0]), int.Parse(pos[1]), sink);
                if (int.Parse(dt.Rows[j][2].ToString()) > 0) lBmp.SetPixel(int.Parse(pos[0]), int.Parse(pos[1]), source);
            }
            return lBmp;
        }

        private void dgvWorkers_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            lblInfo.Text = "";
            if (e.Button == MouseButtons.Right)
            {
                string parents = dgvWorkers.Rows[e.RowIndex].Cells["parent"].Value.ToString();
                lblInfo.Text = "Parents: " + parents + "   |   ";
                stLblLastSQL.Width = this.ClientSize.Width - 40 - lblInfo.Width;
            }
        }


        private void lblInfo_Click(object sender, EventArgs e)
        {
            lblInfo.Text = "";
        }

        private void stLblLastSQL_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Clipboard.SetText(stLblLastSQL.Text);
            }
        }


        private void tscmbBrowseReport_SelectedIndexChanged(object sender, EventArgs e)
        {
            string name = tscmbBrowseReport.SelectedItem.ToString();
            tscmbBrowseReport.DropDownStyle= ComboBoxStyle.DropDownList;
            frmBrowseReports reports = new frmBrowseReports(reportFolder +  name + ".report");
            reports.Show();
        }

        private void dgvLabs_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                DataTable dt = loadTableData("select * from labirynth"); //dt.Rows[dt.Rows.Count - 1].Delete();
                Image img = lab2Bmp(dt);
                frmPicture2 picfrm = new frmPicture2("Labirynth",img,dt, appfolder);
                picfrm.Show();
            }
        }

        private void selectionsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmGuide guide = new frmGuide(Application.StartupPath + @"\dcusersguide.pdf");
            guide.Show();
        }

        private void tsbttnResizeColumns_Click(object sender, EventArgs e)
        {
            if (dgvWorkers.Tag.ToString() == "ColumnHeader")
            {
                dgvWorkers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
                dgvWorkers.Tag = "DisplayedCells";
            }
            else 
            {
                dgvWorkers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                dgvWorkers.Tag = "ColumnHeader";
            }

        }

        private void dgvWorkers_CellMouseDown_1(object sender, DataGridViewCellMouseEventArgs e)
        {
            if ((e.ColumnIndex < 0) || (e.RowIndex < 0)) return;
            if (e.Button==MouseButtons.Right)
            {
                string[] point;
                if ((dgvWorkers.Columns[e.ColumnIndex].Name == "imprint") || (dgvWorkers.Columns[e.ColumnIndex].Name == "worker_path"))
                {
                    string sl = dgvWorkers.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                    string[] s = sl.Split(';');
                    point = new string[s.Length];
                    DataTable dtcell = new DataTable();
                    dtcell.Columns.Add(); dtcell.Columns.Add(); dtcell.Columns.Add();
                    for (int i = 0; i < s.Length - 1; i++)
                    {
                        dtcell.Rows.Add();
                        point[i] = s[i].ToString();
                        string[] ln = point[i].Split(',');
                        for (int j = 0; j < 3; j++)
                        {
                            dtcell.Rows[i][j] = ln[j].ToString();
                        }
                    }
                    if (dgvWorkers.Columns[e.ColumnIndex].Name == "imprint")
                    { 
                        frmPicture2 frmPic = new frmPicture2(dgvWorkers.Columns[e.ColumnIndex].Name, cell2Bmp(dtcell, dgvWorkers.Columns[e.ColumnIndex].Name), loadTableData("select * from labirynth"), appfolder);
                        frmPic.Show();
                    }
                    else
                    {
                        Image img = cell2Bmp(dtcell, dgvWorkers.Columns[e.ColumnIndex].Name);
                        DataTable dtlab = loadTableData("select * from labirynth");
                        Image imgLab = lab2Bmp(dtlab);
                        string selectedWorker = dgvWorkers.Rows[e.RowIndex].Cells["id"].Value.ToString();
                        frmPicture2 frmPic = new frmPicture2("Workerpath for the clicked worker: " + selectedWorker, img, imgLab, dtlab, appfolder);
                        frmPic.Show();
                    }
                }
            }
        }

        Bitmap cell2Bmp(DataTable dt, string colname)
        {
            Bitmap lBmp = new System.Drawing.Bitmap(labSize, labSize, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            //if (colname=="worker_pah")
            //{ 
            //    Color clrEmpty = Color.Black;
            //    Color source = Color.Red;
            //    Color sink = Color.RoyalBlue;

            //    for (int i = 0; i < labSize; i++)
            //    {
            //        for (int j = 0; j < labSize; j++)
            //        {
            //            lBmp.SetPixel(j, i, clrEmpty);
            //        }
            //    }

            //    for (int j = 0; j < dt.Rows.Count; j++)
            //    {
            //        if (int.Parse(dt.Rows[j][2].ToString()) < 0) lBmp.SetPixel(Convert.ToInt32(dt.Rows[j][0]), Convert.ToInt32(dt.Rows[j][1]), sink);
            //        if (int.Parse(dt.Rows[j][2].ToString()) > 0) lBmp.SetPixel(Convert.ToInt32(dt.Rows[j][0]), Convert.ToInt32(dt.Rows[j][1]), source);
            //    }
            //}
            //else
            {
                Color green = Color.LightGreen;
                Color clrEmpty = Color.Black;
                for (int i = 0; i < labSize; i++)
                {
                    for (int j = 0; j < labSize; j++)
                    {
                        lBmp.SetPixel(j, i, clrEmpty);
                    }
                }
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                     lBmp.SetPixel(Convert.ToInt32(dt.Rows[j][0]), Convert.ToInt32(dt.Rows[j][1]), green);
                }
            }
            return lBmp;
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Properties.Settings.Default["winstate"] = this.WindowState;
            Properties.Settings.Default.Save();
            Application.Exit();
        }

        private void compareImprintsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string[] files = Directory.GetFiles(dbFolder, "*.s3db");
            frmCompareImprints frmImprints=new frmCompareImprints(labSize, appfolder);
            frmImprints.ShowDialog();
        }

        private void selectInitialWorkersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sqlcom = "select * from workers where parents=''";   //not instr(parents, ','
            frmSql frmSqlform = new frmSql(cnsb, sqlcom, labSize, "Generation #0");
            frmSqlform.Show();
        }




        private void selectWorkersWithTheSelectedParentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvWorkers.SelectedRows.Count > 0)
            {
                string selectedWorker = dgvWorkers.SelectedRows[0].Cells[0].Value.ToString();
                string sqlcom = "SELECT *  FROM workers  where instr(parents,'," + selectedWorker + "')";
                frmSql frmSqlform = new frmSql(cnsb, sqlcom, labSize, "Workers' generations from selected #" + selectedWorker);
                //frmSqlform.bttnShowGraphically.Visible = true;
                frmSqlform.Show();
            }
            else { MessageBox.Show("There is no selected worker"); }
        }


        private void drawAllWorkersParentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
                frmFamilyTree familyTree = new frmFamilyTree(loadTableData("select id, parents from workers"), "All workers' parents", false);
                familyTree.Show();
        }

        private void selectFromGivenGenerationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSelectGeneration selectGeneration = new frmSelectGeneration();
            if (selectGeneration.ShowDialog() == DialogResult.OK)
            {
                DataTable dtlab = loadTableData("select * from labirynth");
                Image imglab = lab2Bmp(dtlab);
                labSize = imglab.Width;
                ////string where = selectGeneration.kvantor + selectGeneration.generation;
                string gen = "%";
                for (int i = 0; i < int.Parse(selectGeneration.generation); i++)
                {
                    gen += ",%";
                }
                string sqlCommand = "select * from workers where parents like '" + gen + "'";
                DataTable dt = loadTableData(sqlCommand);
                frmSql dispSql = new frmSql(cnsb, sqlCommand, labSize,"Generations from #" + selectGeneration.generation);
                //dispSql.bttnShowGraphically.Visible=true;
                dispSql.Show();
            }
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sqlcom = "SELECT *  FROM workers";
            frmSql frmSqlform = new frmSql(cnsb, sqlcom, labSize, "Generations from #0");
            //frmSqlform.bttnShowGraphically.Visible = true;
            frmSqlform.Show();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            //Boolean b=this.Visible;
        }
    }
}
