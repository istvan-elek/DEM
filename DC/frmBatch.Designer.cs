namespace DC
{
    partial class frmBatch
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBatch));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.cmbDatabaseNames = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bttnDatabase = new System.Windows.Forms.ToolStripDropDownButton();
            this.bttnCreateNewDatabase = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.bttnConfigFile = new System.Windows.Forms.ToolStripMenuItem();
            this.bttnShowLogbook = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.bttnDeleteWorkers = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.bttnDeleteAllDBContent = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.bttnEditCreateWorkers = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.bttnEditCreateLabs = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.bttnShowEditParameters = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.bttnStartMission = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.bttnStartAnal = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.pBarLabs = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.pbarWorkers = new System.Windows.Forms.ToolStripProgressBar();
            this.tsbttnStop = new System.Windows.Forms.ToolStripButton();
            this.lblLabSize = new System.Windows.Forms.Label();
            this.rbManualStartPosition = new System.Windows.Forms.RadioButton();
            this.rbRandomStartPosition = new System.Windows.Forms.RadioButton();
            this.tbStartPosition = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblMissionNumber = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblLivingWorkerNumber = new System.Windows.Forms.Label();
            this.lblFinalNumOfWorkers = new System.Windows.Forms.Label();
            this.lblInitnumofwkr = new System.Windows.Forms.Label();
            this.lblLabNumber = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkUseML = new System.Windows.Forms.CheckBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.toolStrip1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmbDatabaseNames,
            this.toolStripSeparator1,
            this.bttnDatabase,
            this.toolStripSeparator8,
            this.bttnEditCreateWorkers,
            this.toolStripSeparator3,
            this.bttnEditCreateLabs,
            this.toolStripSeparator4,
            this.bttnShowEditParameters,
            this.toolStripSeparator2,
            this.bttnStartMission,
            this.toolStripSeparator5,
            this.bttnStartAnal,
            this.toolStripSeparator9});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(884, 51);
            this.toolStrip1.TabIndex = 13;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // cmbDatabaseNames
            // 
            this.cmbDatabaseNames.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.cmbDatabaseNames.Name = "cmbDatabaseNames";
            this.cmbDatabaseNames.Size = new System.Drawing.Size(300, 51);
            this.cmbDatabaseNames.Sorted = true;
            this.cmbDatabaseNames.Text = "Select a database";
            this.cmbDatabaseNames.SelectedIndexChanged += new System.EventHandler(this.cmbDatabaseNames_SelectedIndexChanged);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 51);
            // 
            // bttnDatabase
            // 
            this.bttnDatabase.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.bttnDatabase.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bttnCreateNewDatabase,
            this.toolStripMenuItem1,
            this.bttnConfigFile,
            this.bttnShowLogbook,
            this.toolStripMenuItem2,
            this.bttnDeleteWorkers,
            this.toolStripMenuItem3,
            this.bttnDeleteAllDBContent});
            this.bttnDatabase.Image = ((System.Drawing.Image)(resources.GetObject("bttnDatabase.Image")));
            this.bttnDatabase.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bttnDatabase.Name = "bttnDatabase";
            this.bttnDatabase.Size = new System.Drawing.Size(68, 48);
            this.bttnDatabase.Text = "Database";
            // 
            // bttnCreateNewDatabase
            // 
            this.bttnCreateNewDatabase.Name = "bttnCreateNewDatabase";
            this.bttnCreateNewDatabase.Size = new System.Drawing.Size(200, 22);
            this.bttnCreateNewDatabase.Text = "Create new database";
            this.bttnCreateNewDatabase.Click += new System.EventHandler(this.bttnCreateNewDatabase_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(197, 6);
            // 
            // bttnConfigFile
            // 
            this.bttnConfigFile.Name = "bttnConfigFile";
            this.bttnConfigFile.Size = new System.Drawing.Size(200, 22);
            this.bttnConfigFile.Text = "Config file";
            this.bttnConfigFile.Click += new System.EventHandler(this.bttnConfigFile_Click);
            // 
            // bttnShowLogbook
            // 
            this.bttnShowLogbook.Enabled = false;
            this.bttnShowLogbook.Name = "bttnShowLogbook";
            this.bttnShowLogbook.Size = new System.Drawing.Size(200, 22);
            this.bttnShowLogbook.Text = "Show logbook";
            this.bttnShowLogbook.Click += new System.EventHandler(this.bttnShowLogbook_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(197, 6);
            // 
            // bttnDeleteWorkers
            // 
            this.bttnDeleteWorkers.Enabled = false;
            this.bttnDeleteWorkers.Name = "bttnDeleteWorkers";
            this.bttnDeleteWorkers.Size = new System.Drawing.Size(200, 22);
            this.bttnDeleteWorkers.Text = "Delete workers";
            this.bttnDeleteWorkers.Click += new System.EventHandler(this.bttnDeleteWorkers_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(197, 6);
            // 
            // bttnDeleteAllDBContent
            // 
            this.bttnDeleteAllDBContent.Enabled = false;
            this.bttnDeleteAllDBContent.Name = "bttnDeleteAllDBContent";
            this.bttnDeleteAllDBContent.Size = new System.Drawing.Size(200, 22);
            this.bttnDeleteAllDBContent.Text = "Delete all tables content";
            this.bttnDeleteAllDBContent.Click += new System.EventHandler(this.bttnDeleteAllDBContent_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(6, 51);
            // 
            // bttnEditCreateWorkers
            // 
            this.bttnEditCreateWorkers.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.bttnEditCreateWorkers.Enabled = false;
            this.bttnEditCreateWorkers.Image = ((System.Drawing.Image)(resources.GetObject("bttnEditCreateWorkers.Image")));
            this.bttnEditCreateWorkers.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.bttnEditCreateWorkers.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bttnEditCreateWorkers.Name = "bttnEditCreateWorkers";
            this.bttnEditCreateWorkers.Size = new System.Drawing.Size(112, 48);
            this.bttnEditCreateWorkers.Text = "Edit/create workers";
            this.bttnEditCreateWorkers.Click += new System.EventHandler(this.bttnCreateWorkers_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 51);
            // 
            // bttnEditCreateLabs
            // 
            this.bttnEditCreateLabs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.bttnEditCreateLabs.Enabled = false;
            this.bttnEditCreateLabs.Image = ((System.Drawing.Image)(resources.GetObject("bttnEditCreateLabs.Image")));
            this.bttnEditCreateLabs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bttnEditCreateLabs.Name = "bttnEditCreateLabs";
            this.bttnEditCreateLabs.Size = new System.Drawing.Size(123, 48);
            this.bttnEditCreateLabs.Text = "Edit/create labirynths";
            this.bttnEditCreateLabs.Click += new System.EventHandler(this.bttnCreateLab_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 51);
            // 
            // bttnShowEditParameters
            // 
            this.bttnShowEditParameters.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.bttnShowEditParameters.Enabled = false;
            this.bttnShowEditParameters.Image = ((System.Drawing.Image)(resources.GetObject("bttnShowEditParameters.Image")));
            this.bttnShowEditParameters.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bttnShowEditParameters.Name = "bttnShowEditParameters";
            this.bttnShowEditParameters.Size = new System.Drawing.Size(127, 48);
            this.bttnShowEditParameters.Text = "Show/Edit parameters";
            this.bttnShowEditParameters.Click += new System.EventHandler(this.bttnShowParameters_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 51);
            // 
            // bttnStartMission
            // 
            this.bttnStartMission.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bttnStartMission.Enabled = false;
            this.bttnStartMission.Image = ((System.Drawing.Image)(resources.GetObject("bttnStartMission.Image")));
            this.bttnStartMission.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.bttnStartMission.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bttnStartMission.Name = "bttnStartMission";
            this.bttnStartMission.Size = new System.Drawing.Size(46, 48);
            this.bttnStartMission.Text = "Start a mission on the selected database";
            this.bttnStartMission.Click += new System.EventHandler(this.bttnStartMission_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 51);
            // 
            // bttnStartAnal
            // 
            this.bttnStartAnal.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bttnStartAnal.Image = ((System.Drawing.Image)(resources.GetObject("bttnStartAnal.Image")));
            this.bttnStartAnal.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.bttnStartAnal.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bttnStartAnal.Name = "bttnStartAnal";
            this.bttnStartAnal.Size = new System.Drawing.Size(48, 48);
            this.bttnStartAnal.Text = "Start analyser";
            this.bttnStartAnal.ToolTipText = "Start analyser";
            this.bttnStartAnal.Click += new System.EventHandler(this.bttnStartAnal_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(6, 51);
            // 
            // toolStrip2
            // 
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.pBarLabs,
            this.toolStripSeparator6,
            this.toolStripLabel2,
            this.pbarWorkers,
            this.tsbttnStop});
            this.toolStrip2.Location = new System.Drawing.Point(0, 51);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(884, 25);
            this.toolStrip2.TabIndex = 14;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(77, 22);
            this.toolStripLabel1.Text = "Lab progress:";
            // 
            // pBarLabs
            // 
            this.pBarLabs.Name = "pBarLabs";
            this.pBarLabs.Size = new System.Drawing.Size(80, 22);
            this.pBarLabs.Step = 1;
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(96, 22);
            this.toolStripLabel2.Text = "Worker progress:";
            // 
            // pbarWorkers
            // 
            this.pbarWorkers.Name = "pbarWorkers";
            this.pbarWorkers.Size = new System.Drawing.Size(80, 22);
            this.pbarWorkers.Step = 1;
            // 
            // tsbttnStop
            // 
            this.tsbttnStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbttnStop.Image = ((System.Drawing.Image)(resources.GetObject("tsbttnStop.Image")));
            this.tsbttnStop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbttnStop.Name = "tsbttnStop";
            this.tsbttnStop.Size = new System.Drawing.Size(35, 22);
            this.tsbttnStop.Text = "Stop";
            this.tsbttnStop.Click += new System.EventHandler(this.tsbttnStop_Click);
            // 
            // lblLabSize
            // 
            this.lblLabSize.AutoSize = true;
            this.lblLabSize.Location = new System.Drawing.Point(13, 50);
            this.lblLabSize.Name = "lblLabSize";
            this.lblLabSize.Size = new System.Drawing.Size(74, 13);
            this.lblLabSize.TabIndex = 16;
            this.lblLabSize.Text = "Labirynth size:";
            // 
            // rbManualStartPosition
            // 
            this.rbManualStartPosition.AutoSize = true;
            this.rbManualStartPosition.Location = new System.Drawing.Point(16, 100);
            this.rbManualStartPosition.Name = "rbManualStartPosition";
            this.rbManualStartPosition.Size = new System.Drawing.Size(122, 17);
            this.rbManualStartPosition.TabIndex = 17;
            this.rbManualStartPosition.Text = "Manual start position";
            this.rbManualStartPosition.UseVisualStyleBackColor = true;
            this.rbManualStartPosition.CheckedChanged += new System.EventHandler(this.rbManualStartPosition_CheckedChanged);
            // 
            // rbRandomStartPosition
            // 
            this.rbRandomStartPosition.AutoSize = true;
            this.rbRandomStartPosition.Checked = true;
            this.rbRandomStartPosition.Location = new System.Drawing.Point(16, 65);
            this.rbRandomStartPosition.Name = "rbRandomStartPosition";
            this.rbRandomStartPosition.Size = new System.Drawing.Size(147, 17);
            this.rbRandomStartPosition.TabIndex = 18;
            this.rbRandomStartPosition.TabStop = true;
            this.rbRandomStartPosition.Text = "Start from random position";
            this.rbRandomStartPosition.UseVisualStyleBackColor = true;
            this.rbRandomStartPosition.CheckedChanged += new System.EventHandler(this.rbRandomStartPosition_CheckedChanged);
            // 
            // tbStartPosition
            // 
            this.tbStartPosition.Location = new System.Drawing.Point(38, 123);
            this.tbStartPosition.Name = "tbStartPosition";
            this.tbStartPosition.Size = new System.Drawing.Size(100, 20);
            this.tbStartPosition.TabIndex = 19;
            this.tbStartPosition.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(144, 123);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 13);
            this.label1.TabIndex = 20;
            this.label1.Text = "format: 10,0    ( x, y )";
            this.label1.Visible = false;
            // 
            // lblMissionNumber
            // 
            this.lblMissionNumber.AutoSize = true;
            this.lblMissionNumber.Location = new System.Drawing.Point(13, 27);
            this.lblMissionNumber.Name = "lblMissionNumber";
            this.lblMissionNumber.Size = new System.Drawing.Size(59, 13);
            this.lblMissionNumber.TabIndex = 22;
            this.lblMissionNumber.Text = "Mission id: ";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblLivingWorkerNumber);
            this.groupBox1.Controls.Add(this.lblFinalNumOfWorkers);
            this.groupBox1.Controls.Add(this.lblInitnumofwkr);
            this.groupBox1.Controls.Add(this.lblLabNumber);
            this.groupBox1.Controls.Add(this.lblLabSize);
            this.groupBox1.Controls.Add(this.lblMissionNumber);
            this.groupBox1.Location = new System.Drawing.Point(393, 94);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(368, 192);
            this.groupBox1.TabIndex = 23;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Workflow progress data";
            // 
            // lblLivingWorkerNumber
            // 
            this.lblLivingWorkerNumber.AutoSize = true;
            this.lblLivingWorkerNumber.Location = new System.Drawing.Point(13, 130);
            this.lblLivingWorkerNumber.Name = "lblLivingWorkerNumber";
            this.lblLivingWorkerNumber.Size = new System.Drawing.Size(111, 13);
            this.lblLivingWorkerNumber.TabIndex = 28;
            this.lblLivingWorkerNumber.Text = "Living worker number:";
            // 
            // lblFinalNumOfWorkers
            // 
            this.lblFinalNumOfWorkers.AutoSize = true;
            this.lblFinalNumOfWorkers.Location = new System.Drawing.Point(13, 162);
            this.lblFinalNumOfWorkers.Name = "lblFinalNumOfWorkers";
            this.lblFinalNumOfWorkers.Size = new System.Drawing.Size(223, 13);
            this.lblFinalNumOfWorkers.TabIndex = 27;
            this.lblFinalNumOfWorkers.Text = "Number of workers, when process terminated:";
            // 
            // lblInitnumofwkr
            // 
            this.lblInitnumofwkr.AutoSize = true;
            this.lblInitnumofwkr.Location = new System.Drawing.Point(13, 104);
            this.lblInitnumofwkr.Name = "lblInitnumofwkr";
            this.lblInitnumofwkr.Size = new System.Drawing.Size(127, 13);
            this.lblInitnumofwkr.TabIndex = 26;
            this.lblInitnumofwkr.Text = "Initial number of workers: ";
            // 
            // lblLabNumber
            // 
            this.lblLabNumber.AutoSize = true;
            this.lblLabNumber.Location = new System.Drawing.Point(13, 78);
            this.lblLabNumber.Name = "lblLabNumber";
            this.lblLabNumber.Size = new System.Drawing.Size(39, 13);
            this.lblLabNumber.TabIndex = 25;
            this.lblLabNumber.Text = "Lab id:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkUseML);
            this.groupBox2.Controls.Add(this.rbManualStartPosition);
            this.groupBox2.Controls.Add(this.rbRandomStartPosition);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.tbStartPosition);
            this.groupBox2.Location = new System.Drawing.Point(12, 94);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(312, 192);
            this.groupBox2.TabIndex = 24;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Set up run parameters";
            // 
            // chkUseML
            // 
            this.chkUseML.AutoSize = true;
            this.chkUseML.Checked = true;
            this.chkUseML.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkUseML.Location = new System.Drawing.Point(19, 33);
            this.chkUseML.Name = "chkUseML";
            this.chkUseML.Size = new System.Drawing.Size(128, 17);
            this.chkUseML.TabIndex = 22;
            this.chkUseML.Text = "Use machine learning";
            this.chkUseML.UseVisualStyleBackColor = true;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // frmBatch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 296);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStrip2);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmBatch";
            this.Text = "Run DEM workers in batch mode";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmBatch_FormClosed);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripComboBox cmbDatabaseNames;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton bttnEditCreateWorkers;
        private System.Windows.Forms.ToolStripButton bttnEditCreateLabs;
        private System.Windows.Forms.ToolStripButton bttnShowEditParameters;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripDropDownButton bttnDatabase;
        private System.Windows.Forms.ToolStripMenuItem bttnCreateNewDatabase;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem bttnDeleteAllDBContent;
        private System.Windows.Forms.ToolStripMenuItem bttnConfigFile;
        private System.Windows.Forms.ToolStripMenuItem bttnShowLogbook;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripProgressBar pBarLabs;
        private System.Windows.Forms.ToolStripButton bttnStartAnal;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripMenuItem bttnDeleteWorkers;
        private System.Windows.Forms.Label lblLabSize;
        private System.Windows.Forms.RadioButton rbManualStartPosition;
        private System.Windows.Forms.RadioButton rbRandomStartPosition;
        private System.Windows.Forms.TextBox tbStartPosition;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripButton bttnStartMission;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripProgressBar pbarWorkers;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.Label lblMissionNumber;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblLabNumber;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.CheckBox chkUseML;
        private System.Windows.Forms.Label lblInitnumofwkr;
        private System.Windows.Forms.Label lblFinalNumOfWorkers;
        private System.Windows.Forms.Label lblLivingWorkerNumber;
        private System.Windows.Forms.ToolStripButton tsbttnStop;
    }
}