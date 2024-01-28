namespace DCMaster
{
    partial class frmCreateWorkers
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCreateWorkers));
            this.bttnQuit = new System.Windows.Forms.Button();
            this.bttnCreateNewWorkers = new System.Windows.Forms.Button();
            this.dgvWorkers = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.tbNumOfWorkers = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblWorkercount = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWorkers)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // bttnQuit
            // 
            this.bttnQuit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bttnQuit.Location = new System.Drawing.Point(279, 484);
            this.bttnQuit.Name = "bttnQuit";
            this.bttnQuit.Size = new System.Drawing.Size(69, 41);
            this.bttnQuit.TabIndex = 0;
            this.bttnQuit.Text = "Close";
            this.bttnQuit.UseVisualStyleBackColor = true;
            this.bttnQuit.Click += new System.EventHandler(this.bttnQuit_Click);
            // 
            // bttnCreateNewWorkers
            // 
            this.bttnCreateNewWorkers.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.bttnCreateNewWorkers.Location = new System.Drawing.Point(10, 67);
            this.bttnCreateNewWorkers.Name = "bttnCreateNewWorkers";
            this.bttnCreateNewWorkers.Size = new System.Drawing.Size(142, 41);
            this.bttnCreateNewWorkers.TabIndex = 1;
            this.bttnCreateNewWorkers.Text = "Create new workers";
            this.bttnCreateNewWorkers.UseVisualStyleBackColor = true;
            this.bttnCreateNewWorkers.Click += new System.EventHandler(this.bttnCreateNewWorkers_Click);
            // 
            // dgvWorkers
            // 
            this.dgvWorkers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvWorkers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvWorkers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvWorkers.Location = new System.Drawing.Point(2, 142);
            this.dgvWorkers.MultiSelect = false;
            this.dgvWorkers.Name = "dgvWorkers";
            this.dgvWorkers.Size = new System.Drawing.Size(360, 323);
            this.dgvWorkers.TabIndex = 8;
            this.dgvWorkers.DataSourceChanged += new System.EventHandler(this.dgvWorkers_DataSourceChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Number of workers:";
            // 
            // tbNumOfWorkers
            // 
            this.tbNumOfWorkers.Location = new System.Drawing.Point(10, 41);
            this.tbNumOfWorkers.Name = "tbNumOfWorkers";
            this.tbNumOfWorkers.Size = new System.Drawing.Size(100, 20);
            this.tbNumOfWorkers.TabIndex = 11;
            this.tbNumOfWorkers.Text = "100";
            this.tbNumOfWorkers.TextChanged += new System.EventHandler(this.tbNumOfWorkers_TextChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.tbNumOfWorkers);
            this.groupBox2.Controls.Add(this.bttnCreateNewWorkers);
            this.groupBox2.Location = new System.Drawing.Point(2, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(173, 124);
            this.groupBox2.TabIndex = 20;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Set up workers";
            // 
            // lblWorkercount
            // 
            this.lblWorkercount.AutoSize = true;
            this.lblWorkercount.Location = new System.Drawing.Point(-1, 468);
            this.lblWorkercount.Name = "lblWorkercount";
            this.lblWorkercount.Size = new System.Drawing.Size(0, 13);
            this.lblWorkercount.TabIndex = 21;
            // 
            // frmCreateWorkers
            // 
            this.AcceptButton = this.bttnCreateNewWorkers;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(365, 537);
            this.Controls.Add(this.lblWorkercount);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.dgvWorkers);
            this.Controls.Add(this.bttnQuit);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmCreateWorkers";
            this.Text = "Create workers";
            ((System.ComponentModel.ISupportInitialize)(this.dgvWorkers)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bttnQuit;
        private System.Windows.Forms.Button bttnCreateNewWorkers;
        private System.Windows.Forms.DataGridView dgvWorkers;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbNumOfWorkers;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblWorkercount;
    }
}