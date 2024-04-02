using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Drawing;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.Remoting;
using System.Reflection.Emit;
using System.Net.Mime;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Data.SQLite;
using System.Xml.Linq;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Diagnostics.Eventing.Reader;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace DCMaster
{
    public partial class frmMain : Form
    {
        CancellationTokenSource cancellationTokenSource;
        CancellationToken cancellationToken;
        Int32 initial_worker_energy;
        int movement_costs;
        int source_energy_max;
        int sink_energy_max;
        int Replication_energy_level;
        int replication_rate;
        int Delay;
        Boolean randomDeath;
        Hostility hostile;
        Dictionary<Int32, worker> wk = new Dictionary<Int32, worker>();  // this is a workers' list as a dictionary, where key is the worker_id and worker is the worker object
        Int32[] wkSequence; // the workers' IDs are in it for the random start
        labyrinth lab;
        IList<String> parameters = new List<String>();
        Boolean learn;
        Boolean merge;
        Boolean randomStart=false;
        Random rndSt = new Random();
        SQLiteConnectionStringBuilder cnsb = new SQLiteConnectionStringBuilder();
        string iterationName;
        string appfolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        string repFileName;
        string labyrinthname;
        string dbName;
        string extLearn="_nolearn";
        string extStarp="";
        string extMerge="";
        string extLab="";
        string extDelay = "";

        public frmMain()
        {
            InitializeComponent();
            this.Text = "DC Master, v_" + Application.ProductVersion.ToString();
            loadParameters();
            tbStartPosition.TabStop = false;
            tbStop.TabStop = false;
        }

        void loadParameters()
        {
            string parameterFileName = Application.StartupPath + "\\parameters.txt";
            parameters = System.IO.File.ReadAllLines(parameterFileName);
            initial_worker_energy = Convert.ToInt16(parameters[0].Split(';')[1]);
            source_energy_max = Convert.ToInt16(parameters[1].Split(';')[1]);
            sink_energy_max = Convert.ToInt16(parameters[2].Split(';')[1]);
            movement_costs = Convert.ToInt16(parameters[3].Split(';')[1]);
            Replication_energy_level = Convert.ToInt16(parameters[4].Split(';')[1]);
            replication_rate = Convert.ToInt16(parameters[5].Split(';')[1]);
            Delay = Convert.ToInt16(parameters[6].Split(';')[1]);
            randomDeath = Properties.Settings.Default.RandomDeath;
            tbStartPosition.Text = Properties.Settings.Default.startposition;
            tbStop.Text = Properties.Settings.Default.countlimit;
            chkUseML.Checked = Properties.Settings.Default.learn;
            chkRandomDeath.Checked = Properties.Settings.Default.RandomDeath;
            chkCoincidence.Checked = Properties.Settings.Default.merge;
            chkStartPositionFromParent.Checked = Properties.Settings.Default.startfrombirthplace;
            randomStart = Properties.Settings.Default.RandomStart;
            chkRandomStartPosition.Checked = randomStart;
        }

        void refreshParameters()
        {
            string parameterFileName = Application.StartupPath + "\\parameters.txt";
            parameters = System.IO.File.ReadAllLines(parameterFileName);
            initial_worker_energy = Convert.ToInt16(parameters[0].Split(';')[1]);
            source_energy_max = Convert.ToInt16(parameters[1].Split(';')[1]);
            sink_energy_max = Convert.ToInt16(parameters[2].Split(';')[1]);
            movement_costs = Convert.ToInt16(parameters[3].Split(';')[1]);
            Replication_energy_level = Convert.ToInt16(parameters[4].Split(';')[1]);
            replication_rate = Convert.ToInt16(parameters[5].Split(';')[1]);
            Delay = Convert.ToInt16(parameters[6].Split(';')[1]);
            learn = chkUseML.Checked;
            merge = chkCoincidence.Checked;
            randomStart = chkRandomStartPosition.Checked;
            randomDeath = chkRandomDeath.Checked;
        }


        private void bttnShowParameters_Click(object sender, EventArgs e)
        {
            frmSetupParameters frmParams = new frmSetupParameters();
            frmParams.StartPosition = FormStartPosition.CenterScreen;
            frmParams.TopMost = true;
            frmParams.Show();
            this.Focus();
        }


        private void bttnCreateWorkers_Click(object sender, EventArgs e)  // create workerre klikkel
        {
            frmCreateWorkers createWorkers = new frmCreateWorkers(tbStartPosition.Text.Trim(), lab, learn); // workereket kreál az indulási pozícióval, a labirínthal és a learning opcióval
            lblStepCount.Text = "Step count: ";
            lblIterationCount.Text = "0";
            lblLivingWorkerNumber.Text = "0";
            createWorkers.StartPosition = FormStartPosition.CenterScreen;
            createWorkers.ShowDialog();
            bttnStartIteration.Enabled = true;
            wk = createWorkers.wk;
            lblInitnumofwkr.Text = "Initial worker count: " + wk.Count.ToString();
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            string ps = tbStartPosition.Text.Trim().Split(',')[0].Trim() + "," + tbStartPosition.Text.Trim().Split(',')[1].Trim();
            float host = hostile.ComputeDinamicHostility(ps);
            tbStartPosition.Text = ps;
            int trvalue = (int)(host);
            trackBar2.Invoke(new Action(() => trackBar2.Value = trvalue));
        }


        private void bttnCreateLab_Click(object sender, EventArgs e)
        {
            frmCreateLabirynth createLab = new frmCreateLabirynth();
            createLab.StartPosition = FormStartPosition.CenterScreen;
            createLab.ShowDialog();
            lblLabSize.Text = "Labyrinth size: " + createLab.LabSize; 
            lab = createLab.lab;
            bttnEditCreateWorkers.Enabled=true;
            bttnEditCreateLabs.Enabled = true; //false
            bttnShowHideLabirynth.Enabled = true;
            tsbttnShowAnalyser.Enabled=true;
            hostile = new Hostility(lab, movement_costs);
            if (lab == null) return;
            if (!randomStart) {grpDinamicHostility.Visible = true; refreshDinamicHostility(); }
            else { grpDinamicHostility.Visible = false; } 
            int trvalue = (int)(lab.Hostility*10);
            if (trvalue > trackBar1.Maximum ) /*lab.Hostility = trackBar1.Maximum;*/ trvalue = (int)(lab.Hostility*10);
            trackBar1.Value = trvalue;
            grp_static_hostility.Text = "Static hostility of labyrinth: " + lab.Hostility.ToString("##.##");
            //trackBar2.Invoke(new Action(() => trackBar2.Value = trvalue));
        }

        void refreshDinamicHostility()
        {
            string ps;
            try
            {
                ps = tbStartPosition.Text.Trim().Split(',')[0].Trim() + "," + tbStartPosition.Text.Trim().Split(',')[1].Trim();
            }
            catch (Exception)
            {

                MessageBox.Show("Error in input data", "Error in input data",MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            float host = hostile.ComputeDinamicHostility(ps);
            grpDinamicHostility.Text = "Dinamic hostility: " + (host).ToString("##.####") + " started from the field (" + ps + ")";
            int trvalue = (int)(host);
            int trmin=0;
            int trmax=0;
            int rate = Math.Abs((int)host / 10);
            if (host < 0)
            {
                if (rate < 1)
                {
                    trmin = -10;  
                    trmax = 10;  
                }
                if ((rate < 10) && (rate >= 1))
                {
                    trmin = -100;  
                    trmax = 100;
                }
                if ((rate < 100) && (rate >= 10))
                {
                    trmin = -1000;  
                    trmax = 1000;
                }
            }
            else
            {
                if (rate < 1)
                {
                    trmin = -10;
                    trmax = 10;
                }
                if ((rate < 10) && (rate >= 1))
                {
                    trmin = -100;
                    trmax = 100;
                }
                if ((rate < 100) && (rate >= 10))
                {
                    trmin = -1000;
                    trmax = 1000;
                }
            }
            lbltrmin.Text = trmin.ToString();
            lbltrmax.Text = trmax.ToString();
            trackBar2.TickFrequency =(trmax-trmin)/ 10;
            trackBar2.Minimum = trmin;
            trackBar2.Maximum = trmax;
            tbStartPosition.Text = ps;
            if (trvalue == -2147483648) return; //  -2,147,483,648 to 2,147,483,647
            if (trvalue == 2147483647) return;
            try
            {
                trackBar2.Invoke(new Action(() => trackBar2.Value = trvalue));
            }
            catch (Exception)
            {
                MessageBox.Show("Nem jó érték");
            }
        }

        string CreateRandomStartPositions()
        {
            Random rnd = new Random();
            int lsize = lab.Size;
            int x = rnd.Next(lsize-1);
            int y = rnd.Next(lsize-1);
            return x + "," + y;
        }

        // start the simulation ************************************************************************************************
        void Start()
        {
        //set up initial parameter of GUI and files (appfolder, reports and iterations) START ============================

        #region appfolder, reports and iterations    
            Int32 numOfWorkers = wk.Count;
            Int32 initial_num_of_workers = wk.Count;
            Int32 initial_energy = 0;
            refreshParameters();
            Delay = Convert.ToInt16(parameters[6].Split(';')[1]);
            extDelay = "_delay" + Delay;
            if (chkUseML.Checked) { learn = true; }
            else { learn = false; }
            if (chkCoincidence.Checked) { merge = true; }
            else { merge = false; }
            if (chkbSave2file.Checked) { tbFileName2Save.Enabled = true; }
            lblInitnumofwkr.Invoke(new Action(() => lblInitnumofwkr.Text = "Initial worker count: " + initial_num_of_workers.ToString()));
            lblLabSize.Invoke(new Action(() => lblLabSize.Text = "Labyrinth size: " + lab.Size.ToString() + "  x " + lab.Size.ToString()));
            lblLivingWorkerNumber.Invoke(new Action(() => lblLivingWorkerNumber.Text = numOfWorkers.ToString()));
            Int32 stepCount = 0;
            wkSequence = setwkIDsequence(wk);
            initial_energy = sumOfEnergy(wk);
            Directory.CreateDirectory(Path.Combine(appfolder, "DC"));

            if (learn) { extLearn = "_learn"; } else { extLearn = "_nolearn"; }
            if (merge) { extMerge = "_merge"; } else { extMerge = ""; }
            if (!randomStart) extStarp= "_startp" + tbStartPosition.Text.Replace(',','_');
            else extStarp = "_startpRandom";
            extLab = "_lab" + lab.Size.ToString();
            iterationName = appfolder + @"\DC\iterations\default" + extLab + extLearn + extMerge + extStarp + extDelay + ".iter";
            repFileName = appfolder + @"\DC\reports\default" + extLab + extLearn + extMerge + extStarp + extDelay + ".report";
            //bttnSaveCurrentLab.Invoke(new Action(() => bttnSaveCurrentLab.Enabled=true));
            DirectoryInfo di1=new DirectoryInfo(appfolder + @"\DC\iterations\"); 
            if (!di1.Exists) { di1.Create(); }
            DirectoryInfo di2 = new DirectoryInfo(appfolder + @"\DC\reports\"); 
            if (!di2.Exists) { di2.Create(); }
            DirectoryInfo diLab = new DirectoryInfo(appfolder + @"\DC\labyrinths\");
            if (!diLab.Exists) {  diLab.Create(); }
            labyrinthname = appfolder + @"\DC\labyrinths\" + tbFileName2Save.Text.Trim() + extLab + extLearn + extMerge + extStarp + extDelay + ".lab";
            if (chkbSave2file.Checked)
            {
                iterationName = appfolder + @"\DC\iterations\" + tbFileName2Save.Text.Trim() + extLab + extLearn + extMerge + extDelay + extStarp + ".iter";
                repFileName = appfolder + @"\DC\reports\" + tbFileName2Save.Text.Trim() + extLab + extLearn + extMerge + extStarp + extDelay + ".report";
                DirectoryInfo di3 = new DirectoryInfo(appfolder + @"\DC\Databases\");
                if (!di3.Exists)  { di3.Create(); }
                dbName= appfolder + @"\DC\databases\" + tbFileName2Save.Text.Trim() + extLab + extLearn + extMerge + extStarp + extDelay + ".s3db";

            }
                  
            using (FileStream fs = new FileStream(iterationName, FileMode.Create, FileAccess.Write, FileShare.Read))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.WriteLine("stepCount\tnumOfWorkers\tEnergy\t\tEntropy");
                    sw.WriteLine(stepCount + "\t\t" + initial_num_of_workers + "\t\t" + initial_energy + "\t\t0" );
                    sw.Close();
                }
            }
            for (Int32 l = 0; l < wkSequence.Length; l++)  //set up workers' start location
            {
                if (wk[wkSequence[l]].StartLocation == null)
                {
                    if (!randomStart) wk[wkSequence[l]].StartLocation = tbStartPosition.Text;
                    else { wk[wkSequence[l]].StartLocation = CreateRandomStartPositions(); }
                }
                else wk[wkSequence[l]].StartLocation = wk[wkSequence[l]].CurrentPosition;
                wk[wkSequence[l]].Learn = learn;
            }
        #endregion  //set up initial parameter of GUI and files (appfolder, reports and iterations)
             
            Int32 maxindex = wkSequence.Max();
            while (numOfWorkers > 0)  //addig pörög a ciklus, amíg van élő worker ----------------------------------------------------
            {
                if (randomDeath)  // véletlen halál: az első worker vagy meghal vagy nem
                {
                    wk[wkSequence[0]].Live = getRandomDeathValue();
                }
                if (tbStop.Text.Length != 0)
                {
                    if (stepCount == int.Parse(tbStop.Text))
                    {
                        saveLab2File();
                        writeReportFile(numOfWorkers, tbStop.Text);
                        this.Invoke(new Action(() => this.bttnStopIteration.Enabled = false));
                        this.Invoke(new Action(() => this.bttnStartIteration.Enabled = true));
                        if (cancellationTokenSource != null)
                        {
                            cancellationTokenSource.Cancel();
                            cancellationTokenSource = null;
                        }
                        if (chkbSave2file.Checked)
                        {
                            this.Invoke(new Action(() => this.Enabled = false));
                            frmTerminateProgram frmterm = new frmTerminateProgram();
                            frmterm.Location = new Point(this.Location.X + this.Width / 2 - frmterm.Width/2, this.Location.Y + this.Height / 2);
                            frmterm.Show();
                            this.Invoke(new Action(() => this.Text="Results are saving to database..."));                           
                            saveFinalData2DB();
                            this.Invoke(new Action(() => this.Enabled = true));
                            this.Invoke(new Action(() => this.Text = "DC Master, v_" + Application.ProductVersion.ToString()));
                            frmterm.Close();
                        }
                        MessageBox.Show("Process stopped at step count '" + tbStop.Text + "'", "Process stopped" ,MessageBoxButtons.OK, MessageBoxIcon.Information);
                     return;                       
                    }
                }
                initial_num_of_workers = numOfWorkers;
                wkSequence = setwkIDsequence(wk);
                //start workers  xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
                for (int ii=0; ii< wkSequence.Length; ii++)
                {
                    Int32 i = wkSequence[ii];
                    wk[i].moveNext();

                    if (wk[i].Live)
                    {
                        if (wk[i].Energy > Replication_energy_level)
                        {
                            Int32 parentsID = wk[i].ID;
                            for (int repi = 0; repi < replication_rate; repi++)
                            {
                                maxindex++;
                                wk.Add(maxindex, new worker(lab, maxindex, learn, parameters));
                                wkSequence = setwkIDsequence(wk);
                                if (chkStartPositionFromParent.Checked)
                                {
                                    wk[maxindex].StartLocation = wk[parentsID].CurrentPosition;
                                }
                                else
                                {
                                    wk[maxindex].StartLocation= tbStartPosition.Text;
                                }
                                wk[maxindex].Parent = wk[parentsID].Parent + "," + parentsID;
                                wk[maxindex].Live = true;
                                wk[maxindex].Energy = initial_worker_energy;
                                wk[maxindex].SEntropy = wk[i].SEntropy;
                                wk[maxindex].Imprint = wk[i].Imprint;
                                wk[i].Energy -= wk[maxindex].Energy; 
                            }
                            numOfWorkers = wk.Count;
                        }

                    }
                    else
                    {
                        wk.Remove(i);
                        numOfWorkers = wk.Count;
                        if (numOfWorkers != 0)
                        {
                            wkSequence = setwkIDsequence(wk);
                        }
                        else
                        {
                            lblIterationCount.Invoke(new Action(() => lblIterationCount.Text = (stepCount).ToString()));
                            lblLivingWorkerNumber.Invoke(new Action(() => lblLivingWorkerNumber.Text = numOfWorkers.ToString()));
                            writeReportFile(numOfWorkers, (stepCount).ToString());
                            Int32 sumEnergy = sumOfEnergy(wk);
                            float sumentropia = sumofSEntropy(wk);
                            saveIteration2File(stepCount+1, numOfWorkers, sumEnergy, sumentropia);
                            writeReportFile(numOfWorkers, lblIterationCount.Text);
                            saveLab2File();
                            this.Invoke(new Action(() => this.bttnStopIteration.Enabled = false));
                            this.Invoke(new Action(() => this.bttnStartIteration.Enabled = true));
                            if (chkbSave2file.Checked) 
                            {
                                this.Invoke(new Action(() => this.Enabled = false));
                                this.Invoke(new Action(() => this.Text = "Results are saving to database..."));
                                frmTerminateProgram frmterm = new frmTerminateProgram();
                                frmterm.Location = new Point(this.Location.X + this.Width / 2 - frmterm.Width / 2, this.Location.Y + this.Height / 2);
                                frmterm.Show();
                                saveFinalData2DB();
                                this.Invoke(new Action(() => this.Text = "DC Master, v_" + Application.ProductVersion.ToString()));
                                this.Invoke(new Action(() => this.Enabled = true));
                                frmterm.Close();
                            }
                            cancellationTokenSource.Cancel();
                            cancellationTokenSource = null;
                            MessageBox.Show("No worker is alive because the population went to die.", "Process terminated because population has died", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return; 
                        }
                    }
                }  // xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx (end of start workers) xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
                if (chkCoincidence.Checked)
                {
                    mergeImprint(wkSequence);
                }
                stepCount++;
                Int32 gatheredEnergy = sumOfEnergy(wk);
                float entropia= sumofSEntropy(wk);
                saveIteration2File(stepCount, numOfWorkers, gatheredEnergy, entropia);
                //saveLab2File();
                lblIterationCount.Invoke(new Action(() => lblIterationCount.Text = (stepCount).ToString()));
                lblLivingWorkerNumber.Invoke(new Action(() => lblLivingWorkerNumber.Text = numOfWorkers.ToString()));               
                try
                {
                    cancellationToken.ThrowIfCancellationRequested();
                }
                catch (Exception)
                {
                    if (cancellationToken.IsCancellationRequested == true) 
                    {
                        writeReportFile(numOfWorkers, lblIterationCount.Text);
                        this.Invoke(new Action(() => this.bttnStopIteration.Enabled = false));
                        this.Invoke(new Action(() => this.bttnStartIteration.Enabled = true));
                        saveLab2File();
                        if (chkbSave2file.Checked)
                        {
                            //this.Invoke(new Action(() => this.bttnStartIteration.Enabled = false));
                            this.Invoke(new Action(() => this.Enabled = false));
                            this.Invoke(new Action(() => this.Text = "Results are saving to database..."));
                            frmTerminateProgram frmterm = new frmTerminateProgram();
                            frmterm.Location = new Point(this.Location.X + this.Width / 2 - frmterm.Width / 2, this.Location.Y + this.Height/2);
                            frmterm.Show();
                            saveFinalData2DB();
                            this.Invoke(new Action(() => this.Enabled = true));
                            this.Invoke(new Action(() => this.Text = "DC Master, v_" + Application.ProductVersion.ToString()));
                            frmterm.Close();
                            MessageBox.Show("Process terminated by user interrupt", "Process terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        return; 
                    }
                }
            } /*-------------------------------------------- while (numOfWorkers > 0) ------------------------------------------------------*/
        }

        Boolean getRandomDeathValue()
        {
            double v = rndSt.NextDouble();
            Boolean rndVal = v > 0.05D;
            return rndVal;
        }


        void saveIteration2File(int stepNum, int numWk, Int32 energ, float SEntr)
        {
            using (FileStream fs = new FileStream(iterationName, FileMode.Append, FileAccess.Write, FileShare.Read))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.WriteLine(stepNum + "\t\t" + numWk + "\t\t" + energ + "\t\t" + SEntr);
                }
            }
        }

       
        void saveFinalData2DB()  // adatbázisba ment mindent a pillanatnyi állapotokról. Menti a workereket, a worker_path-okat, az iteráció lépéseinek adatait
        {
            cnsb.DataSource = dbName;
            dbsqliteTools dbs = new dbsqliteTools(cnsb);
            dbs.createNewDBFile();
            dbs.createWorkersTable();
            if (wk.Count != 0) dbs.saveWorkersTable(wkSequence, wk);
            dbs.createLabTable();           
            dbs.saveLabirynthTable(lab, movement_costs);
            dbs.createIterationTable();
            dbs.saveIteration(File.ReadAllLines(iterationName));
        }

        float sumofSEntropy(Dictionary<Int32, worker> wkrs)
        {
            float sum = 0;
            foreach (var item in wkrs.Values)
            {
                sum += item.SEntropy;
            }
            return sum;
        }


        Int32 sumOfEnergy(Dictionary<Int32, worker> wkrs)
        {
            Int32 sum = 0;
            foreach (var item in wkrs.Values)
            {
                sum += item.Energy;
            }
            return sum;
        }


        int[] setwkIDsequence(Dictionary<Int32, worker> dict)
        {
            int[] seq;
            if (dict.Count != 0)
            {
                Random rnd = new Random();
                List<Int32> keyList = new List<Int32>(dict.Keys);
                HashSet<int> wklist = new HashSet<int>();
                int wkcount = 0;
                do  // a workerek véletlenszerű indulási sorrendjét állítja össze a wkList nevű listába, majd az ID-ket konvertálja a seq nevű tömbbe
                {
                    Int32 randomKey = keyList[rnd.Next(keyList.Count)];
                    if (wklist.Add(randomKey)) wkcount += 1;
                } while (wkcount != dict.Count);
                seq = wklist.ToArray();
            }
            else seq = null;
            return seq;
           
        }

 
        void writeReportFile(int numofworkersAlive, string inum) //write run data into a text file, which contains run data (missions, labs, energy, etc.), file name: databasename.rep
        {
            string title = "Report on the iteration";
            StringBuilder sb = new StringBuilder(title);
            sb.Append(Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.AppendLine("Time:" + DateTime.Now.ToString() + Environment.NewLine);
            string lin = new string('-', title.Length);
            sb.AppendLine("World parameters" + Environment.NewLine + "----------------");
            sb.AppendLine("Labyrinth size: " + lab.Size + " x " + lab.Size);
            sb.AppendLine("Num of energy sources: " + lab.NumOfEnergySources );
            sb.AppendLine("Num of energy sinks: " + lab.NumOfEnergySinks );
            sb.AppendLine(parameters[0].Split(';')[0] + ": " + parameters[0].Split(';')[1] );
            sb.AppendLine(parameters[1].Split(';')[0] + ": " + parameters[1].Split(';')[1] );
            sb.AppendLine(parameters[2].Split(';')[0] + ": " + parameters[2].Split(';')[1] );
            sb.AppendLine(parameters[3].Split(';')[0] + ": " + parameters[3].Split(';')[1] );
            sb.AppendLine(parameters[4].Split(';')[0] + ": " + parameters[4].Split(';')[1] );
            sb.AppendLine(parameters[5].Split(';')[0] + ": " + parameters[5].Split(';')[1] );
            sb.Append("Hostility: " + lab.Hostility + Environment.NewLine);
            sb.Append(parameters[6].Split(';')[0] + ": " + parameters[6].Split(';')[1] + Environment.NewLine);


            sb.AppendLine(Environment.NewLine + "Iteration parameters" + Environment.NewLine + "--------------------");
            sb.AppendLine("Iteration number was " + inum + ", when process terminated");
            sb.AppendLine("Number of living workers: " + numofworkersAlive);
            sb.AppendLine(lblInitnumofwkr.Text);
            sb.AppendLine("Learning workers: " + chkUseML.Checked.ToString());
            sb.AppendLine("Merge knowledge when workers coincide: " + chkCoincidence.Checked.ToString());
            if (randomStart) { sb.AppendLine("Start position is random"); }

            if (chkStartPositionFromParent.Checked)
            {
                sb.AppendLine("Start positions of children are from parents at birth" );
                if (!randomStart) sb.AppendLine("Initial start position is: " + tbStartPosition.Text);
            }
            else
            {
                if (!randomStart) sb.AppendLine("Start position is always: "  + tbStartPosition.Text);
            }
            sb.AppendLine("Random death: " + randomDeath.ToString());
            System.IO.File.WriteAllText(repFileName, sb.ToString());
        }


        private void frmBatch_FormClosed(object sender, FormClosedEventArgs e)
        {
            Properties.Settings.Default.startposition = tbStartPosition.Text;
            Properties.Settings.Default.countlimit = tbStop.Text;
            Properties.Settings.Default.learn = chkUseML.Checked;
            Properties.Settings.Default.merge = chkCoincidence.Checked;
            Properties.Settings.Default.startfrombirthplace = chkStartPositionFromParent.Checked;
            Properties.Settings.Default.RandomStart = chkRandomStartPosition.Checked;
            Properties.Settings.Default.RandomDeath = chkRandomDeath.Checked;
            Properties.Settings.Default.Save();
            Application.Exit();
        }


        private void bttnStartIteration_Click(object sender, EventArgs e)
        {
            if (wk.Count == 0)
            {
                MessageBox.Show("There is no created workers. Please, create them", "Missing workers", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            bttnShowWorkerPath.Enabled = true;
            bttnShowHideWorkers.Enabled = true;
            bttnShowPopulation.Enabled = true;
            bttnShowGatheredEnergy.Enabled = true;
            bttnStartIteration.Enabled = false;
            bttnStopIteration.Enabled = true;
            tsbttnShowAnalyser.Enabled = true;
            bttnShowImprint.Enabled = true;
            bttnShowIterationData.Enabled = true;
            bttnShowEntropy.Enabled = true;
            bttnShowFittness.Enabled = true;    
            if (cancellationTokenSource == null)
            {
                cancellationTokenSource = new CancellationTokenSource();
                cancellationToken = cancellationTokenSource.Token;
                Task.Run(Start, cancellationToken);
            }
        }

        private void bttnStopIteration_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
            int numOfwks = Convert.ToInt16(lblLivingWorkerNumber.Text);
            if (cancellationTokenSource != null)
            {
                cancellationTokenSource.Cancel();
                cancellationTokenSource = null;
            }
            bttnStartIteration.Enabled = true;
        }


        void mergeImprint(Int32[] wkSeq) // merge different workers' knowledge when they coincide
        {
            //Int32[] wkSeq = setwkIDsequence(wk);

            for (Int32 i=0; i< wkSeq.Length; i++)
            {
                for (Int32 j=i+1; j< wkSeq.Length; j++)
                {
                    if (wk[wkSeq[i]].CurrentPosition == wk[wkSeq[j]].CurrentPosition)
                    {
                        if ((wk[wkSeq[i]].Imprint.Count != 0) || (wk[wkSeq[j]].Imprint.Count != 0))
                        {
                            try
                            {
                                HashSet<string> mergedImprint = new HashSet<string>(wk[wkSeq[j]].Imprint);
                                HashSet<string> mergedImprint1 = new HashSet<string>(wk[wkSeq[i]].Imprint);
                                mergedImprint.UnionWith(mergedImprint1);
                                wk[wkSeq[j]].Imprint = mergedImprint.ToList();
                                wk[wkSeq[i]].Imprint = mergedImprint.ToList();
                            }
                            catch (Exception e)
                            {
                                MessageBox.Show(e.Message);
                            }
                        }
                    }
                }               
            }
        }

        //string convertHashSet2String(HashSet<string> hSet)
        //{
        //    string stOut = "";
        //    stOut = string.Join("| ", hSet);
        //    return stOut;
        //}

        private void chkUseML_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkUseML.Checked)
            {
                learn = false;
                chkCoincidence.Checked = false;
            }
            else learn = true;
        }


        DataTable getCurrentWorkers()
        {
            DataTable dtWorkers = new DataTable();
            dtWorkers.Columns.Add("ID", typeof(Int32));
            dtWorkers.Columns.Add("Energy", typeof(Int32));
            dtWorkers.Columns.Add("Entropy", typeof(Int32));
            dtWorkers.Columns.Add("CurrentPosition");
            dtWorkers.Columns.Add("Parents");
            dtWorkers.Columns.Add("StartPosition");
            for (int i=0; i<wk.Count; i++)
            {
                DataRow dr=dtWorkers.NewRow();
                dr["ID"] = wk[wkSequence[i]].ID;
                dr["Energy"] = wk[wkSequence[i]].Energy;
                dr["Entropy"] = wk[wkSequence[i]].SEntropy;
                dr["Parents"] = wk[wkSequence[i]].Parent;
                dr["CurrentPosition"] = wk[wkSequence[i]].CurrentPosition;
                dr["StartPosition"] = wk[wkSequence[i]].StartLocation;
                dtWorkers.Rows.Add(dr);
            }
            return dtWorkers;
        }

        private void bttnShowAnalyser_Click(object sender, EventArgs e)
        {
            grpAnalyserBox.Visible = !grpAnalyserBox.Visible;
            if (grpAnalyserBox.Visible)
            {
                tsbttnShowAnalyser.Text = "Hide analyser";
                grpAnalyserTools.Visible = true;
                bttnShowIterationData.Visible = true;
                grpAnalyserBox.Visible = true;
                //dgvWks4Analyser.Visible = true;
                if (lab != null)
                {
                    grp_static_hostility.Visible = true;

                    float hst = lab.Hostility; //hostile.ComputeHostility();
                    trackBar1.Value = (int)(hst*10);
                    grp_static_hostility.Text = "Static hostility of the labyrinth: " + hst.ToString("##.##");
                }
                else { grp_static_hostility.Visible = false; grpDinamicHostility.Visible = false; }

                this.AutoSize = true;
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                tsbttnShowAnalyser.Text = "Show analyser";
                grpAnalyserTools.Visible = false;
                bttnShowIterationData.Visible = false;
                grpAnalyserBox.Visible = false;
                grp_static_hostility.Visible = false;
                dgvWks4Analyser.Visible = false;
                this.AutoSize = false;
                this.WindowState = FormWindowState.Normal;
                for (int index = Application.OpenForms.Count - 1; index >= 0; index--)
                {
                    if (Application.OpenForms[index].Name == "frmPicture2") {Application.OpenForms[index].Close();}
                }
                for (int index = Application.OpenForms.Count - 1; index >= 0; index--)
                {
                    if (Application.OpenForms[index].Name == "DisplayCharts") { Application.OpenForms[index].Close(); }
                }
                grpLabData.Visible = false;
                bttnShowHideLabirynth.PerformClick();   
                bttnShowHideLabirynth.Invoke(new Action(() => bttnShowHideLabirynth.Text = "Show labyrinth"));

                bttnShowHideWorkers.PerformClick();
                bttnShowHideWorkers.Invoke(new Action(() => bttnShowHideWorkers.Text = "Show workers"));
                //dgvWks4Analyser.Visible = !dgvWks4Analyser.Visible;
                bttnShowHideWorkers.Text = "Show workers";
                bswks.DataSource = null;
                bnwks.Visible = false;
            }
        }

        private void bttnShowHideWorkers_click(object sender, EventArgs e)
        {
            if (!dgvWks4Analyser.Visible)
            {
                bttnShowHideWorkers.Text = "Hide workers";
                dgvWks4Analyser.Visible = !dgvWks4Analyser.Visible;
                bswks.DataSource = getCurrentWorkers();
                bnwks.BindingSource = bswks;                
                dgvWks4Analyser.DataSource = bswks;
                bnwks.Visible = true;
            }
            else
            {
                dgvWks4Analyser.Visible = !dgvWks4Analyser.Visible;
                bttnShowHideWorkers.Text = "Show workers";
                bswks.DataSource = null;
                bnwks.Visible = false;
            }          
        }

        private void bttnShowHideLabirynth_Click(object sender, EventArgs e)
        {
            if (!grpLabData.Visible)
            {
                dgvLab.Columns.Clear();
                dgvLab.Columns.Add("Position", "Position");
                dgvLab.Columns.Add("Field_value", "Field_value");
                dgvLab.Columns.Add("Delay", "Delay");
                int r = lab.Size;
                int numofSources = 0;
                int numofSinks= 0;
                for (int i = 0; i < r; i++)
                {
                    for (int j=0; j < r; j++)
                    {
                        if (lab.Fields[i, j] != movement_costs)
                        {
                            //tbLabData.AppendText("Fields[" + i + "," + j + "] = " + lab.Fields[i,j] + "\tdelay: " + lab.Delay[i,j] + Environment.NewLine);
                            dgvLab.Rows.Add(i+ "," + j, lab.Fields[i, j], lab.Delay[i, j]);
                        }
                        if (lab.Fields[i, j] < movement_costs) numofSinks++;
                        if (lab.Fields[i,j] > movement_costs) numofSources++;
                    }
                }
                lblLabparams.Text = "Labyrinth size=" + lab.Size + " x " + lab.Size + ", Sink count=" + numofSinks + ", " + "Source count=" + numofSources;
                bttnShowHideLabirynth.Text = "Hide labyrinth";
                grpLabData.Visible = true;
            }
            else
            {
                grpLabData.Visible = false;
                bttnShowHideLabirynth.Text = "Show labyrinth";
                for (int index = Application.OpenForms.Count - 1; index >= 0; index--)
                {
                    if (Application.OpenForms[index].Name == "frmPicture2") { Application.OpenForms[index].Close(); }
                }
            }
        }


        private void btnShowLabImage_Click(object sender, EventArgs e)
        {
            ImageTools imcl = new ImageTools();
            Image img =  imcl.lab2Bmp(lab);
            frmPicture2 frmPic = new frmPicture2("Labyrinth", img, lab);
            frmPic.TopMost = true;  
            frmPic.StartPosition = FormStartPosition.Manual;
            frmPic.Location=new Point(Convert.ToInt32(1.9*this.ClientSize.Width/3), Convert.ToInt32(this.ClientSize.Height/2.6));
            frmPic.Show();
        }


        private void bttnShowPopulation_Click(object sender, EventArgs e)
        {
            DisplayCharts2 frmChart = new DisplayCharts2(iterationName,"population");
            frmChart.Text = "Database=" + Path.GetFileNameWithoutExtension(iterationName);
            frmChart.Show();
        }

        private void bttnShowPopulationEnergy_Click(object sender, EventArgs e)
        {
            DisplayCharts2 frmChart = new DisplayCharts2(iterationName, "energy");
            frmChart.Text = "Database=" + Path.GetFileNameWithoutExtension(iterationName);
            frmChart.Show();
        }

        private void bttnShowWorkerPath_Click(object sender, EventArgs e)
        {
            ImageTools imtl= new ImageTools();
            Image imgLab = imtl.lab2Bmp(lab);
            wkSequence = setwkIDsequence(wk);
            Image img = imtl.workerPath2Image(lab.Size, wk);
            frmPicture2 frmPic = new frmPicture2("Path", img, imgLab, lab);
            frmPic.Show();
        }

        private void bttnShowEntropy_Click(object sender, EventArgs e)
        {
            DisplayCharts2 frmChart = new DisplayCharts2(iterationName, "entropy");
            frmChart.Text = "Database=" + Path.GetFileNameWithoutExtension(iterationName);
            frmChart.Show();
        }

        private void bttnShowFittness_Click(object sender, EventArgs e)
        {
            DisplayCharts2 frmChart = new DisplayCharts2(iterationName, "fittness");
            frmChart.Text = "Database=" + Path.GetFileNameWithoutExtension(iterationName);
            frmChart.Show();
        }

        private void dgvWks4Analyser_MouseDown(object sender, MouseEventArgs e)
        {
            if (dgvWks4Analyser.SelectedRows.Count > 0)
            {
                if (e.Button == MouseButtons.Right)
                {
                    int selectedWorkerIndex = int.Parse(dgvWks4Analyser.SelectedRows[0].Cells["ID"].Value.ToString());
                    if (dgvWks4Analyser.SelectedRows.Count > 0)
                    {
                        ImageTools imtl = new ImageTools();
                        Image imgLab = imtl.lab2Bmp(lab);
                        ImageTools imt = new ImageTools();
                        int id = wk[selectedWorkerIndex].ID;
                        Image img = imt.workerPath2Image(lab.Size, wk[selectedWorkerIndex]);
                        frmPicture2 frmPic = new frmPicture2("Path of " + "ID=" + selectedWorkerIndex, img, imgLab, lab);
                        frmPic.Show();
                    }
                }
            }
        }

        private void tsbttnViewReports_Click(object sender, EventArgs e)
        {
            tscmbViewreports.Items.Clear();
            tscmbViewreports.Visible = !tscmbViewreports.Visible;

            string repfolder = appfolder + "\\DC\\reports\\";
            DirectoryInfo d = new DirectoryInfo(repfolder);
            FileInfo[] Files = d.GetFiles("*.*");
            string iterfolder = appfolder + "\\DC\\iterations\\";
            DirectoryInfo d2 = new DirectoryInfo(iterfolder);
            FileInfo[] Files2 = d2.GetFiles("*.*");

            for (int i = 0; i< Files.Length; i++)
            {
                if  (Path.GetExtension(Files[i].Name) == ".report")
                {
                    tscmbViewreports.Items.Add(Files[i].Name); 
                }
            }
            for (int i = 0; i < Files2.Length; i++)
            {
                if (Path.GetExtension(Files2[i].Name) == ".iter")
                {
                    tscmbViewreports.Items.Add(Files2[i].Name);
                }
            }
        }

        private void tscmbViewreports_SelectedIndexChanged(object sender, EventArgs e)
        {
            string fname = "";
            if (Path.GetExtension(tscmbViewreports.SelectedItem.ToString()) == ".iter") fname = appfolder + @"\DC\iterations\" + tscmbViewreports.SelectedItem.ToString();
            else fname= appfolder + @"\DC\reports\" + tscmbViewreports.SelectedItem.ToString();
            iterationName = fname;
            
            if (Path.GetExtension(fname) == ".iter")
            {
                bttnShowGatheredEnergy.Enabled = true;
                bttnShowPopulation.Enabled = true;
                bttnShowEntropy.Enabled = true;
                bttnShowIterationData.Visible = true;
                bttnShowIterationData.Enabled = true;
                bttnShowFittness.Enabled = true;
                bttnShowWorkerPath.Enabled = false;
                bttnShowHideWorkers.Enabled = false;
                bttnShowImprint.Enabled = false;
                tsbttnShowAnalyser.Enabled = true;
            }
            else 
            {
                bttnShowGatheredEnergy.Enabled = false;
                bttnShowPopulation.Enabled = false;;
            }
            frmViewTextFile viewreports = new frmViewTextFile(fname);
            viewreports.TopMost = true;
            viewreports.Show();
        }


        private void tsbttnViewIterationData_Click(object sender, EventArgs e)
        {
            frmViewTextFile viewreports = new frmViewTextFile(iterationName);
            viewreports.TopMost = true;
            viewreports.Show();
        }

        private void bttnShowImprint_Click(object sender, EventArgs e)
        {
            ImageTools imtl = new ImageTools();
            wkSequence = setwkIDsequence(wk);
            Image img = imtl.Imprint2Image(lab.Size, wk);
            frmPicture2 frmPic = new frmPicture2("Workers' knowledge", img, lab);
            frmPic.TopMost = true;
            frmPic.Show();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            float hst = hostile.ComputeStaticHostility();
            trackBar1.Value = (int)(hst);
        }


        private void tbStartPosition_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                refreshDinamicHostility();
            }
        }

        private void chkbSave2file_CheckedChanged(object sender, EventArgs e)
        {
            if (chkbSave2file.Checked) { tbFileName2Save.Enabled = true; }
            else { tbFileName2Save.Enabled = false; }
        }

        private void tsbttnHelp_Click(object sender, EventArgs e)
        {
            frmGuide guide = new frmGuide(Application.StartupPath + @"\dcusersguide.pdf");
            guide.Show();
        }

 
        void saveLab2File()
        {
            List<string> llab = new List<string>();
            string line1 = getEnergySourceAndSinks();
            llab.Add(line1 + ",labsize:" + lab.Size);
            for (int i = 0; i < lab.Size; i++)
            {
                for (int j = 0; j < lab.Size; j++)
                {
                    if (lab.Fields[i, j] != -1)
                    {
                        llab.Add(i + "," + j + "," + lab.Fields[i, j] + "," + lab.Delay[i,j]);
                    }
                }
            }
            string[] slab = llab.ToArray();

            System.IO.File.WriteAllLines(labyrinthname, slab);
        }

        string getEnergySourceAndSinks()
        {
            string res;
            int numofSinks = 0;
            int numofSources = 0;
            for (int i=0; i< lab.Size; i++)
            {
                for (int j=0; j< lab.Size; j++)
                {
                    if (lab.Fields[i, j] < movement_costs) numofSinks += 1;
                    if (lab.Fields[i, j] > 0) numofSources += 1;
                }
            }
            res = "Num of energy sinks: "+ numofSinks + "," + "num of energy sources:" + numofSources;
            return res.ToString();
        }

        private void chkCoincidence_CheckedChanged(object sender, EventArgs e)
        {
            if(chkCoincidence.Checked) { chkUseML.Checked = true; }
        }

        private void chkRandomStartPosition_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRandomStartPosition.Checked) 
            {
                randomStart = true;
                lblsp0.Enabled = false; 
                lblsp1.Enabled=false; 
                tbStartPosition.Enabled = false; 
                grpDinamicHostility.Visible = false;
            }
            else 
            { 
                randomStart=false;
                lblsp0.Enabled = true; 
                lblsp1.Enabled = true; 
                tbStartPosition.Enabled = true;
                grpDinamicHostility.Visible = true;
                refreshDinamicHostility();
            }
        }


    }
}
