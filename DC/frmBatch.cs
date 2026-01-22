using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;
using System.Threading;
using System.IO;


namespace DC
{
    public partial class frmBatch : Form
    {
        string WumpDBName;
        NpgsqlConnectionStringBuilder connsb = new NpgsqlConnectionStringBuilder();
        NpgsqlConnectionStringBuilder cnsbAdmin = new NpgsqlConnectionStringBuilder();
        string dbConnectionFilePath = Application.StartupPath + @"\config.cfg";  // config.cfg file contains database connection parameters for wumpus databases
        string dbAdminConnectionFilePath = Application.StartupPath + @"\configAdmin.cfg";  //configAdmin.cfg file contains database connection parameters for root

        int initial_worker_energy;
        int Gold_energy;
        int movement_costs;
        int Trap_energy;
        int Wumpus_energy;
        int Replication_energy_level;


        public frmBatch()
        {
            loadConnectionParameters(dbConnectionFilePath);
            loadDBAdminConnectionParameters(dbAdminConnectionFilePath);
            InitializeComponent();
            DataTable dt = getWumpusDBNames();
            foreach(DataRow row in dt.Rows)
            {
                cmbDatabaseNames.Items.Add(row[0]);
            }
        }

        void loadConnectionParameters(string fpath) //reads connection parameter from a parameter file (DBConnect.cfg)
        {
            string[] dbParams = System.IO.File.ReadAllLines(fpath);
            connsb.Username = dbParams[0].Split(' ')[1];
            connsb.Password = dbParams[1].Split(' ')[1];
            connsb.Host = dbParams[2].Split(' ')[1];
        }


        void loadDBAdminConnectionParameters(string fpath) //reads connection parameter from a parameter file (DBConnect.cfg)
        {
            string[] dbParams = System.IO.File.ReadAllLines(fpath);
            cnsbAdmin.Username = dbParams[0].Split(' ')[1];
            cnsbAdmin.Password = dbParams[1].Split(' ')[1];
            cnsbAdmin.Database = dbParams[2].Split(' ')[1];
            cnsbAdmin.Host = dbParams[3].Split(' ')[1];
        }


        DataTable getCatalog() // getsdatabase those name from database catalog, where the database names start with 'wump%'
        {
            DataTable dt = new DataTable();
            using (NpgsqlConnection cnn = new NpgsqlConnection(cnsbAdmin.ConnectionString))
            {
                cnn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    cmd.Connection = cnn;
                    cmd.CommandText = "SELECT datname FROM pg_database where datname like 'wump%'";
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                }
            }
            return dt;
        }



        private void cmbDatabaseNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            WumpDBName = cmbDatabaseNames.SelectedItem.ToString();
            connsb.Database = cmbDatabaseNames.SelectedItem.ToString();

            bttnStartMission.Enabled = true;
            bttnShowEditParameters.Enabled = true;
            bttnEditCreateWorkers.Enabled = true;
            bttnEditCreateLabs.Enabled = true;
            bttnDeleteAllDBContent.Enabled = true;
            bttnShowLogbook.Enabled = true;
            bttnDeleteWorkers.Enabled = true;
            cmbDatabaseNames.DropDownStyle = ComboBoxStyle.DropDownList;
            toolStrip2.Visible = true;
            writeReportFile();//***********************************************************************
        }


        DataTable getLabirynthData() //select labirynth data sorted by lab_id
        {
            DataTable dt = new DataTable();
            using (NpgsqlConnection cnn = new NpgsqlConnection(connsb.ConnectionString))
            {
                cnn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    cmd.Connection = cnn;
                    cmd.CommandText = "SELECT * FROM lab ORDER BY lab_id";
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                }
            }
            return dt;
        }


        DataTable getLabirynthIDs() // selec lab_id -s from lab table
        {
            DataTable dt = new DataTable();
            using (NpgsqlConnection cnn = new NpgsqlConnection(connsb.ConnectionString))
            {
                cnn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    cmd.Connection = cnn;
                    cmd.CommandText = "SELECT lab_id FROM lab";
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                }
            }
            return dt;
        }


        DataTable getWumpusDBNames() //select wumpus database names
        {
            DataTable dt = new DataTable();
            using (NpgsqlConnection cnn = new NpgsqlConnection(cnsbAdmin.ConnectionString))
            {
                cnn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    cmd.Connection = cnn;
                    cmd.CommandText = "SELECT datname FROM pg_database where datname like 'wump%'";
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                }
            }
            return dt;
        }



        DataRow getOneLabirynthData(string labId) // select one labirynth data
        {
            DataTable dt = new DataTable();
            using (NpgsqlConnection cnn = new NpgsqlConnection(connsb.ConnectionString))
            {
                cnn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    cmd.Connection = cnn;
                    cmd.CommandText = "SELECT * FROM lab WHERE lab_id='" + labId + "'";
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                }
            }
            return dt.Rows[0];
        }


        private void bttnShowParameters_Click(object sender, EventArgs e)
        {
            frmSetupParameters frmParams = new frmSetupParameters(connsb);
            frmParams.StartPosition = FormStartPosition.Manual;
            frmParams.Location = new Point(this.Size.Width - frmParams.Width + (this.Size.Width-this.ClientSize.Width)*2, this.Location.Y );
            frmParams.TopMost = true;
            frmParams.Show();
            this.Focus();
        }


        private void frmBatch_FormClosed(object sender, FormClosedEventArgs e)
        {
            int frmCount = Application.OpenForms.Count;
            for (int i=1; i<frmCount;i++)
            {
                Application.OpenForms[1].Close();
            }
        }


        private void bttnCreateWorkers_Click(object sender, EventArgs e)
        {
            frmCreateWorkers createWorkers = new frmCreateWorkers(connsb);
            createWorkers.StartPosition = FormStartPosition.Manual;
            createWorkers.Location = new Point(this.Location.X + this.Size.Width, this.Location.Y);
            createWorkers.ShowDialog();
        }



        private void bttnCreateLab_Click(object sender, EventArgs e)
        {
            frmCreateLabirynth createLab = new frmCreateLabirynth(connsb);
            createLab.StartPosition = FormStartPosition.Manual;
            createLab.Location = new Point(this.Location.X + this.Size.Width, this.Location.Y);
            createLab.ShowDialog();
        }



        private void bttnRefreshDB_Click(object sender, EventArgs e)
        {
            cmbDatabaseNames_SelectedIndexChanged(cmbDatabaseNames, EventArgs.Empty);
        }



        labirynth createLabirynth(int grsize, int labirID, int numOfWump)   //create a new labirynth with grsize size and labirynth ID, and fills its fields with empty content (no wumpus, no Trap, no gold)
        {
            Dictionary<string, int> fieldCode = new Dictionary<string, int>()
            {
            {"Wumpus",0}, {"Trap",1}, {"Breeze",2}, {"Stench",3}, {"Empty field",4}, {"Glitter",5}, {"Gold",6}
            };
            labirynth lab = new labirynth();
            lab.Size = grsize;
            lab.Fields = new int[grsize, grsize];
            lab.LabId = labirID;
            lab.numOfWumpusTrapsGold = numOfWump;
            for (int i = 0; i < grsize; i++)
            {
                for (int j = 0; j < grsize; j++)
                {
                    lab.Fields[j, i] = fieldCode["Empty field"];
                }
            }
            return lab;
        }


        int getRandomNumber(Random rand, int val) // gets random numbers
        {
            return rand.Next(val);
        }

        string getStartLocation(Random rand, labirynth lab) // gets start position randomly
        {
            string startPos = "";

            if (getRandomAxis(rand) == "x")
            {
                startPos = getRandomNumber(rand, lab.Size) + ",0";
            }
            else // "y"
            {
                startPos = "0," + getRandomNumber(rand, lab.Size);
            }
            return startPos;
        }


        string getRandomAxis(Random rand) //choose a axis randomly
        {
            string flagXY;
            if (rand.NextDouble() >= 0.5)
            { flagXY = "x"; }
            else
            { flagXY = "y"; }
            return flagXY;
        }



        labirynth getLab(string labid) // get a certain labirynth's data from lab table
        {
            Dictionary<string, int> fieldCode = new Dictionary<string, int>()
            {
            {"Wumpus",0}, {"Trap",1}, {"Breeze",2}, {"Stench",3}, {"Empty field",4}, {"Glitter",5}, {"Gold",6}
            };
            DataRow row= getOneLabirynthData(labid.ToString());
            labirynth labir = new labirynth();
            labir.Size=Convert.ToInt16(row["lab_size"]);
            labir.LabId=Convert.ToInt16(row["lab_id"]);
            labir.numOfWumpusTrapsGold=Convert.ToInt16(row["number_of_wumpus"]);
            labir = createLabirynth(labir.Size, labir.LabId, labir.numOfWumpusTrapsGold);
            labir.WumpusPosition = row["wumpus_position"].ToString();
            labir.TrapPosition = row["trap_position"].ToString();
            labir.GoldPosition = row["gold_position"].ToString();
            string[] goldPositions = labir.GoldPosition.Split(';');
            string[] trapPositions = labir.TrapPosition.Split(';');
            string[] wumpusPositions = labir.WumpusPosition.Split(';');

            for (int i = 0; i < labir.numOfWumpusTrapsGold; i++)
            {
                int xg = Convert.ToInt16(goldPositions[i].Split(',')[0]);
                int yg = Convert.ToInt16(goldPositions[i].Split(',')[1]);
                labir.Fields[xg, yg] = fieldCode["Gold"];

                int xt = Convert.ToInt16(trapPositions[i].Split(',')[0]);
                int yt = Convert.ToInt16(trapPositions[i].Split(',')[1]);
                labir.Fields[xt, yt] = fieldCode["Trap"];

                int xw = Convert.ToInt16(wumpusPositions[i].Split(',')[0]);
                int yw = Convert.ToInt16(wumpusPositions[i].Split(',')[1]);
                labir.Fields[xw, yw] = fieldCode["Wumpus"];
            }
            return labir;
        }



        worker[] getWorkers(labirynth lab) //get workers from workers database
        {
            DataTable dt = new DataTable();
            using (NpgsqlConnection cnn = new NpgsqlConnection(connsb.ConnectionString))
            {
                cnn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    cmd.Connection = cnn;
                    string comText = "SELECT * FROM workers";
                    cmd.CommandText = comText;
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                }
            }
            Int32 numOfWork = dt.Rows.Count;
            worker[] wk = new worker[numOfWork];
            int i = 0;
            foreach (DataRow row in dt.Rows)
            {
                wk[i] = new worker(Convert.ToInt32(row["worker_id"].ToString()),lab,connsb);
                wk[i].Energy = Convert.ToInt32(row["energy"].ToString());
                wk[i].Parent = Convert.ToInt32(row["parent"].ToString());
                wk[i].Live = Convert.ToBoolean(row["live"].ToString());
                i++;
            }
            return wk;
        }


        void logBookWriter(Int32 worker_id, int lab_id, string start_position, int mission) //write data into logbook table
        {
            using (NpgsqlConnection cnn = new NpgsqlConnection(connsb))
            {
                cnn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    cmd.Connection = cnn;
                    string values = worker_id + "," + lab_id + ",'" + start_position + "'," + mission;
                    cmd.CommandText = "INSERT INTO logbook (worker_id,lab_id,start_position,mission_id" + ") VALUES(" + values + ")";
                    cmd.ExecuteNonQuery();
                }
            }
        }


        private void bttnCreateNewDatabase_Click(object sender, EventArgs e)
        {
            frmCreateDB createDb = new frmCreateDB(cnsbAdmin);
            createDb.ShowDialog();
            cmbDatabaseNames.Items.Clear();
            DataTable dt = getCatalog();
            foreach (DataRow row in dt.Rows)
            {
                cmbDatabaseNames.Items.Add(row[0].ToString());
            }
            cmbDatabaseNames.SelectedIndex = - 1;
            bttnEditCreateLabs.Enabled = false;
            bttnEditCreateWorkers.Enabled = false;
            bttnShowEditParameters.Enabled = false;
            bttnStartMission.Enabled = false;
            //cmbLabirynths.Items.Clear();
            //pb.Image = null;
        }

        private void bttnDeleteAllDBContent_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Dangerous command! Are you sure to DELETE all tables content?", "Dangerous command!", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                dbTools dbtDeleteTable = new dbTools(connsb);
                dbtDeleteTable.deleteTable("workers");
                dbtDeleteTable.deleteTable("lab");
                dbtDeleteTable.deleteTable("worker_path");
                dbtDeleteTable.deleteTable("logbook");
                dbtDeleteTable.deleteTable("mission");
                dbtDeleteTable.deleteTable("knowledge");
                dbtDeleteTable.resetAllSequences();
                //cmbLabirynths.Items.Clear();
                MessageBox.Show("All tables content has been successfuly deleted", "Tables has deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void bttnConfigFile_Click(object sender, EventArgs e)
        {
            frmConfig config = new frmConfig(Application.StartupPath + @"\config.cfg");
            config.ShowDialog();
        }

        private void bttnShowLogbook_Click(object sender, EventArgs e)
        {
            frmLogBook logBook = new frmLogBook(connsb);
            logBook.Show();
        }

        private void bttnStartAnal_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Application.StartupPath + @"\wump_anal.exe");
        }

        private void bttnDeleteWorkers_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Dangerous command! Are you sure to DELETE worker table content?", "Dangerous command!", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                dbTools dbtDeleteWorkers = new dbTools(connsb);
                dbtDeleteWorkers.deleteTable("workers");
                dbtDeleteWorkers.resetSequence("workers");
                dbtDeleteWorkers.deleteTable("worker_path");
                dbtDeleteWorkers.resetSequence("worker_path");
                dbtDeleteWorkers.deleteTable("logbook");
                dbtDeleteWorkers.resetSequence("logbook");
                dbtDeleteWorkers.deleteTable("knowledge");
                dbtDeleteWorkers.resetSequence("knowledge");
                dbtDeleteWorkers.deleteTable("mission");
                dbtDeleteWorkers.resetSequence("mission");
                MessageBox.Show("Workers tables content has been successfuly deleted", "Table has deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void rbManualStartPosition_CheckedChanged(object sender, EventArgs e)
        {
            label1.Visible = true;
            tbStartPosition.Visible = true;
            tbStartPosition.Focus();
        }

        private void rbRandomStartPosition_CheckedChanged(object sender, EventArgs e)
        {
            label1.Visible = false;
            tbStartPosition.Visible = false;
        }


        private void bttnStartMission_Click(object sender, EventArgs e) // start missions
        {
            dbTools dbtLoadNumOf = new dbTools(connsb);
            Random rand = new Random();

            DataTable dt = dbtLoadNumOf.loadTableData("select count(*) from lab");
            int numOfLabs = Convert.ToInt16(dt.Rows[0][0].ToString());
            labirynth[] lab = new labirynth[numOfLabs];
            Boolean stop = false;
            dbTools dbt = new dbTools(connsb);
            DataTable dtpars = dbt.getParametersTable(connsb);
            initial_worker_energy = Convert.ToInt16(dtpars.Rows[0][1]);
            Gold_energy = Convert.ToInt16(dtpars.Rows[1][1]);
            Trap_energy = Convert.ToInt16(dtpars.Rows[3][1]);
            Wumpus_energy = Convert.ToInt16(dtpars.Rows[4][1]);
            movement_costs = Convert.ToInt16(dtpars.Rows[2][1]);
            pBarLabs.Maximum = numOfLabs;
            dbTools dbT = new dbTools(connsb);
            this.Cursor = Cursors.WaitCursor;
            dbTools dbtSaveMission = new dbTools(connsb);
            DataTable dtWkrNum = dbtLoadNumOf.loadTableData("SELECT COUNT(*) FROM workers");
            Int32 initial_num_of_workers = Convert.ToInt32(dtWkrNum.Rows[0][0]);
            Int32 num_of_alive_workers = initial_num_of_workers;
            lblFinalNumOfWorkers.Text = "";
            lblLivingWorkerNumber.Text = "";
            lblLabSize.Text = "";
            lblLabNumber.Text = "";
            lblInitnumofwkr.Text = "";
            lblMissionNumber.Text = "";
            if (initial_num_of_workers == 0)
            {
                MessageBox.Show("Worker number is 0! Create workers, please", "No available workers", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Cursor = Cursors.Default;
                return; 
            }
            lblInitnumofwkr.Text = "Initial number of workers: " + initial_num_of_workers.ToString();
            Int32 gathered_energy = Convert.ToInt32(dbtLoadNumOf.loadTableData("SELECT sum(energy) FROM workers").Rows[0][0].ToString()); //num_of_alive_workers * initial_worker_energy;
            Single energy_rate = 1;
            Single pop_rate = 1;
            dbtSaveMission.saveMissionData(0, num_of_alive_workers, gathered_energy, pop_rate, energy_rate);

            for (int imiss = 0; imiss < 20; imiss++) // start missions
            {
                groupBox1.Visible = true;
                lblLabSize.Text = "Labirynth size: " + "";
                lblLabNumber.Text = "Labirynth id: " + "0";
                Single popStart = Convert.ToSingle(dbtLoadNumOf.loadTableData("SELECT COUNT(*) FROM workers").Rows[0][0].ToString());
                Single energyStart = Convert.ToInt32(dbtLoadNumOf.loadTableData("SELECT sum(energy) FROM workers").Rows[0][0].ToString());   //popStart * initial_worker_energy;
                lblMissionNumber.Text = "Mission id: " + (imiss+1).ToString();
                pBarLabs.Value = 0;
                Application.DoEvents();

                for (int j = 0; j < numOfLabs; j++) // start labs
                {
                    pBarLabs.PerformStep();
                    dt = dbtLoadNumOf.loadTableData("select count(*) from workers");
                    Int32 numOfWorkers = Convert.ToInt32(dt.Rows[0][0].ToString());   //wk.Length;
                    if (numOfWorkers < 1) { break; }
                    worker[] wk = new worker[numOfWorkers];
                    pbarWorkers.Maximum = numOfWorkers;
                    pbarWorkers.Value = 0;
                    DataTable labIDs = getLabirynthIDs();
                    string labID = labIDs.Rows[j][0].ToString();
                    lab[j] = getLab(labID);
                    lblLabSize.Text = "Labirynth size: " + lab[j].Size.ToString() + "  x " + lab[j].Size.ToString();
                    lblLabNumber.Text = "Labirynth id: " + lab[j].LabId.ToString();
                    Application.DoEvents();
                    wk = getWorkers(lab[j]);
                    int numberOfWumpus = lab[j].numOfWumpusTrapsGold;

                    for (Int32 i = 0; i < numOfWorkers; i++)  //start workers
                    {
                        Application.DoEvents();
                        pbarWorkers.PerformStep();
                        int fieldValue = wk[i].FieldValue;
                        if (rbRandomStartPosition.Checked) { wk[i].StartLocation = getStartLocation(rand, lab[j]); }
                        if (rbManualStartPosition.Checked) { wk[i].StartLocation = tbStartPosition.Text; }
                        int row = Convert.ToInt16(wk[i].StartLocation.Split(',')[1]);
                        int col = Convert.ToInt16(wk[i].StartLocation.Split(',')[0]);
                        dbT = new dbTools(connsb, wk[i], lab[j]);
                        logBookWriter(wk[i].ID, lab[j].LabId, wk[i].StartLocation, imiss);
                        while (wk[i].Live)
                        {
                            wk[i].moveNext();
                            dbT.update_worker_energy(wk[i].Energy, wk[i].ID);
                            if (wk[i].Gotcha)
                            {
                                wk[i].goBack2Start();
                                dbT.update_worker_energy(wk[i].Energy - initial_worker_energy*2, wk[i].ID);
                                dbTools dbtAddNewWorker = new dbTools(connsb);
                                dbtAddNewWorker.AddNewWorker(wk[i].ID, initial_worker_energy);
                                dbtAddNewWorker.AddNewWorker(wk[i].ID, initial_worker_energy);
                                break;
                            }
                        }
                        if (wk[i].Live == false)
                        {
                            dbT.setupWorkersLive(wk[i].ID);
                        }
                        saveWorkerPath2DB(Wumpus_energy, Trap_energy, Gold_energy, movement_costs, wk[i], lab[j]);
                        if (chkUseML.Checked)
                        {
                            dbTools dbtSaveKnowledge = new dbTools(connsb);
                            dbtSaveKnowledge.saveKnowledge(wk[i].ID, lab[j].LabId);
                            string knWumpField = getPeculiarFieldsFromKnowledge(wk[i].ID, Wumpus_energy);
                            if (knWumpField != "") { wk[i].Knowledge_wumpus_fields = knWumpField;}
                            string knTrapField = getPeculiarFieldsFromKnowledge(wk[i].ID, Trap_energy);
                            if (knTrapField != "") { wk[i].Knowledge_trap_fields = knTrapField; }
                            string knGoldField = getPeculiarFieldsFromKnowledge(wk[i].ID, Gold_energy);
                            if (knGoldField != "") { wk[i].Knowledge_gold_fields = knGoldField; }
                        }
                        dbTools deleteDeadWorkers = new dbTools(connsb);
                        deleteDeadWorkers.deleteDeadWorkers();
                    }
                    pbarWorkers.Value = 0;
                    lblLivingWorkerNumber.Text = "Living worker numbers: " + (dbtLoadNumOf.loadTableData("SELECT COUNT(*) FROM workers").Rows[0][0].ToString());
                }
                pBarLabs.Value = 0;
                dtWkrNum = dbtLoadNumOf.loadTableData("SELECT COUNT(*) FROM workers");
                num_of_alive_workers = Convert.ToInt32(dtWkrNum.Rows[0][0]);
                DataTable dtgE=dbt.loadTableData("select sum(workers.energy) from workers");
                if (num_of_alive_workers < 1) 
                { 
                    gathered_energy = 0; 
                    dbtSaveMission.saveMissionData(imiss, 0, 0, 0, 0);
                    MessageBox.Show("All the workers have been died", "Workers died", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                }
                else
                {
                    gathered_energy = Convert.ToInt32(dtgE.Rows[0][0].ToString());
                }
                pop_rate = Convert.ToSingle(num_of_alive_workers) / popStart;
                energy_rate = Convert.ToSingle(gathered_energy)/ energyStart;

                dbtSaveMission.saveMissionData(imiss, num_of_alive_workers, gathered_energy, pop_rate, energy_rate);
                Int32 ls = Convert.ToInt32(lblLabSize.Text.Split('x')[1].Trim());
                if (num_of_alive_workers > (ls * ls))
                {
                    stop = true;
                    break;
                }
                if (stop) { return; }
            }
            lblFinalNumOfWorkers.Text="Number of workers, when process termiated:" + num_of_alive_workers;
            writeReportFile();
            this.Cursor = Cursors.Default;
            MessageBox.Show("Missions have been terminated", "Mission terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        void writeReportFile() //write run data into a text file, which contains run data (missions, labs, energy, etc.), file name: databasename.rep
        {
            string myfolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\reports\\";
            string title = "Report on '" + WumpDBName + "' Database";
            StringBuilder sb = new StringBuilder(title);
            sb.Append(Environment.NewLine);
            for (int i = 0; i < title.Length; i++)
            {
                sb.Append("=");
            }
            sb.Append(Environment.NewLine);
            sb.AppendLine("Time:" + DateTime.Now.ToString() + Environment.NewLine);
            sb.AppendLine(lblMissionNumber.Text  + ", when process termiated");
            dbTools dbt = new dbTools(connsb);
            sb.AppendLine("Number of labirynths: " + dbt.getLabirynthData().Rows.Count);
            sb.AppendLine(lblInitnumofwkr.Text);
            sb.AppendLine(lblFinalNumOfWorkers.Text + Environment.NewLine);
            if (rbManualStartPosition.Checked)
            {
                sb.AppendLine("Starting position: manual (" + tbStartPosition.Text + ")");
            }
            else
            {
                sb.AppendLine("Starting position: random");
            }

            sb.AppendLine("World parameters" + Environment.NewLine + "----------------" );

            DataTable dtab = dbt.getParametersTable(connsb);
            foreach (DataRow row in dtab.Rows)
            {
                sb.AppendLine( row[0].ToString() +  ": " + row[1].ToString());
            }
            if (!Directory.Exists(myfolder)) { Directory.CreateDirectory(myfolder); }

            string infoFileName = myfolder + WumpDBName + ".rep";
            System.IO.File.WriteAllText(infoFileName, sb.ToString());
        }


        string getPeculiarFieldsFromKnowledge(Int32 wkid, int energy)  //select wumpus, trap and gold fields of a certain worker
        {
            string pecFields = "";
            dbTools dbtLoad = new dbTools(connsb);
            string sqlCommand = "SELECT worker_path from knowledge WHERE worker_id = " + wkid + " AND value = " + energy;
            DataTable dtab = dbtLoad.loadTableData(sqlCommand);
            foreach (DataRow row in dtab.Rows)
            {
                pecFields += row[0].ToString() + ";";
            }
            return pecFields;
        }



        void saveWorkerPath2DB(int wumpus_energy, int trap_energy, int gold_energy, int empty_field_energy, worker wkr, labirynth lab)    //saves worker_path to 'workers_path' table
        {
            using (NpgsqlConnection cnn = new NpgsqlConnection(connsb.ConnectionString))
            {
                cnn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    cmd.Connection = cnn;
                    string tableName = "worker_path";
                    string fields = "worker_path, worker_id, lab_id, value";
                    int fieldEnergy = empty_field_energy;
                    foreach (string item in wkr.WorkerPath)
                    {
                        //gold on the field
                        string[] l1 = lab.GoldPosition.Split(';');
                        foreach (string p1 in l1)
                        {
                            if (p1 == item) { fieldEnergy = gold_energy; }
                        }
                        //trap on the field
                        string[] l2 = lab.TrapPosition.Split(';');
                        foreach (string p2 in l2)
                        {
                            if (p2 == item) { fieldEnergy = trap_energy; }
                        }
                        string[] l3 = lab.WumpusPosition.Split(';');
                        foreach (string p3 in l3)
                        {
                            if (p3 == item) { fieldEnergy = wumpus_energy; }
                        }

                        string values = "'" + item + "'," + wkr.ID + "," + lab.LabId + "," + fieldEnergy;
                        string sqlCommand = "INSERT INTO " + tableName + " (" + fields + ") VALUES(" + values + ")";
                        cmd.CommandText = sqlCommand;
                        cmd.ExecuteNonQuery();
                        fieldEnergy = empty_field_energy;
                    }      
                }
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
           
        }

        private void tsbttnStop_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }



    }
}
