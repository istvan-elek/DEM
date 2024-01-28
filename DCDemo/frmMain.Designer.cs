namespace DCDemo
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.dgv = new System.Windows.Forms.DataGridView();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.stLblLastField = new System.Windows.Forms.ToolStripStatusLabel();
            this.stLbLLive = new System.Windows.Forms.ToolStripStatusLabel();
            this.stLblGotcha = new System.Windows.Forms.ToolStripStatusLabel();
            this.stLblWorkerPathCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.stLblLastEnergyLevel = new System.Windows.Forms.ToolStripStatusLabel();
            this.bttnPlay = new System.Windows.Forms.ToolStripButton();
            this.bttnNewLabirynth = new System.Windows.Forms.ToolStripButton();
            this.toolStripStartLoc = new System.Windows.Forms.ToolStripLabel();
            this.tsTbStartLoc = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.tsTbLabSize = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.tsTbNumOfWorkers = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
            this.tstbNumOfWump = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbttnLegend = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.AllowUserToResizeColumns = false;
            this.dgv.AllowUserToResizeRows = false;
            this.dgv.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.ColumnHeadersVisible = false;
            this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv.Location = new System.Drawing.Point(0, 48);
            this.dgv.MultiSelect = false;
            this.dgv.Name = "dgv";
            this.dgv.ReadOnly = true;
            this.dgv.RowHeadersVisible = false;
            this.dgv.RowTemplate.Height = 50;
            this.dgv.Size = new System.Drawing.Size(793, 531);
            this.dgv.TabIndex = 1;
            this.dgv.Visible = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stLblLastField,
            this.stLbLLive,
            this.stLblGotcha,
            this.stLblWorkerPathCount,
            this.stLblLastEnergyLevel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 633);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(873, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Visible = false;
            // 
            // stLblLastField
            // 
            this.stLblLastField.Name = "stLblLastField";
            this.stLblLastField.Size = new System.Drawing.Size(61, 17);
            this.stLblLastField.Text = "Final field:";
            // 
            // stLbLLive
            // 
            this.stLbLLive.ForeColor = System.Drawing.Color.White;
            this.stLbLLive.Name = "stLbLLive";
            this.stLbLLive.Size = new System.Drawing.Size(31, 17);
            this.stLbLLive.Text = "Live:";
            // 
            // stLblGotcha
            // 
            this.stLblGotcha.Name = "stLblGotcha";
            this.stLblGotcha.Size = new System.Drawing.Size(48, 17);
            this.stLblGotcha.Text = "Gotcha:";
            // 
            // stLblWorkerPathCount
            // 
            this.stLblWorkerPathCount.Name = "stLblWorkerPathCount";
            this.stLblWorkerPathCount.Size = new System.Drawing.Size(109, 17);
            this.stLblWorkerPathCount.Text = "Worker path count:";
            // 
            // stLblLastEnergyLevel
            // 
            this.stLblLastEnergyLevel.Name = "stLblLastEnergyLevel";
            this.stLblLastEnergyLevel.Size = new System.Drawing.Size(97, 17);
            this.stLblLastEnergyLevel.Text = "Last energy level:";
            // 
            // bttnPlay
            // 
            this.bttnPlay.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bttnPlay.Enabled = false;
            this.bttnPlay.Image = ((System.Drawing.Image)(resources.GetObject("bttnPlay.Image")));
            this.bttnPlay.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.bttnPlay.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bttnPlay.Name = "bttnPlay";
            this.bttnPlay.Size = new System.Drawing.Size(46, 45);
            this.bttnPlay.Text = "Run workers";
            this.bttnPlay.Click += new System.EventHandler(this.bttnPlay_Click);
            // 
            // bttnNewLabirynth
            // 
            this.bttnNewLabirynth.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bttnNewLabirynth.Image = ((System.Drawing.Image)(resources.GetObject("bttnNewLabirynth.Image")));
            this.bttnNewLabirynth.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.bttnNewLabirynth.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bttnNewLabirynth.Name = "bttnNewLabirynth";
            this.bttnNewLabirynth.Size = new System.Drawing.Size(46, 45);
            this.bttnNewLabirynth.Text = "Create a labyrinth";
            this.bttnNewLabirynth.Click += new System.EventHandler(this.bttnNewLabirynth_Click);
            // 
            // toolStripStartLoc
            // 
            this.toolStripStartLoc.Name = "toolStripStartLoc";
            this.toolStripStartLoc.Size = new System.Drawing.Size(80, 45);
            this.toolStripStartLoc.Text = "Start location:";
            // 
            // tsTbStartLoc
            // 
            this.tsTbStartLoc.Name = "tsTbStartLoc";
            this.tsTbStartLoc.Size = new System.Drawing.Size(50, 48);
            this.tsTbStartLoc.Text = "0,0";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 48);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(82, 45);
            this.toolStripLabel2.Text = "Labyrinth size:";
            // 
            // tsTbLabSize
            // 
            this.tsTbLabSize.Name = "tsTbLabSize";
            this.tsTbLabSize.Size = new System.Drawing.Size(30, 48);
            this.tsTbLabSize.Text = "10";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 48);
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(98, 45);
            this.toolStripLabel3.Text = "Num of workers: ";
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bttnPlay,
            this.bttnNewLabirynth,
            this.toolStripSeparator9,
            this.toolStripStartLoc,
            this.tsTbStartLoc,
            this.toolStripSeparator1,
            this.toolStripLabel2,
            this.tsTbLabSize,
            this.toolStripSeparator2,
            this.toolStripLabel3,
            this.tsTbNumOfWorkers,
            this.toolStripSeparator4,
            this.toolStripLabel4,
            this.tstbNumOfWump,
            this.toolStripSeparator5,
            this.tsbttnLegend});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(793, 48);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(6, 48);
            // 
            // tsTbNumOfWorkers
            // 
            this.tsTbNumOfWorkers.Name = "tsTbNumOfWorkers";
            this.tsTbNumOfWorkers.Size = new System.Drawing.Size(30, 48);
            this.tsTbNumOfWorkers.Text = "1";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 48);
            // 
            // toolStripLabel4
            // 
            this.toolStripLabel4.Name = "toolStripLabel4";
            this.toolStripLabel4.Size = new System.Drawing.Size(162, 45);
            this.toolStripLabel4.Text = "Num of wumpus, traps, gold:";
            // 
            // tstbNumOfWump
            // 
            this.tstbNumOfWump.Name = "tstbNumOfWump";
            this.tstbNumOfWump.Size = new System.Drawing.Size(30, 48);
            this.tstbNumOfWump.Text = "1";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 48);
            // 
            // tsbttnLegend
            // 
            this.tsbttnLegend.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbttnLegend.Image = ((System.Drawing.Image)(resources.GetObject("tsbttnLegend.Image")));
            this.tsbttnLegend.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbttnLegend.Name = "tsbttnLegend";
            this.tsbttnLegend.Size = new System.Drawing.Size(79, 45);
            this.tsbttnLegend.Text = "Show legend";
            this.tsbttnLegend.Click += new System.EventHandler(this.tsbttnLegend_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(793, 579);
            this.Controls.Add(this.dgv);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(20, 20);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Demo";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMain_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel stLblLastField;
        private System.Windows.Forms.ToolStripStatusLabel stLbLLive;
        private System.Windows.Forms.ToolStripStatusLabel stLblGotcha;
        private System.Windows.Forms.ToolStripStatusLabel stLblWorkerPathCount;
        private System.Windows.Forms.ToolStripButton bttnPlay;
        private System.Windows.Forms.ToolStripButton bttnNewLabirynth;
        private System.Windows.Forms.ToolStripLabel toolStripStartLoc;
        private System.Windows.Forms.ToolStripTextBox tsTbStartLoc;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripTextBox tsTbLabSize;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripTextBox tsTbNumOfWorkers;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripLabel toolStripLabel4;
        private System.Windows.Forms.ToolStripTextBox tstbNumOfWump;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton tsbttnLegend;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripStatusLabel stLblLastEnergyLevel;
    }
}

