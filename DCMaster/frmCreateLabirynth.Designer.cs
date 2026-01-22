namespace DCMaster
{
    partial class frmCreateLabirynth
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCreateLabirynth));
            this.bttnCreateLab = new System.Windows.Forms.Button();
            this.bttnClose = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbLabSize = new System.Windows.Forms.TextBox();
            this.tbNumberOfEnergySources = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.bttnLoadLab = new System.Windows.Forms.Button();
            this.tbNumberOfEnergySinks = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tbLab = new System.Windows.Forms.TextBox();
            this.lblMin = new System.Windows.Forms.Label();
            this.lblMax = new System.Windows.Forms.Label();
            this.grp_static_hostility = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.lblSinkEnergySum = new System.Windows.Forms.Label();
            this.lblSourceEnergySum = new System.Windows.Forms.Label();
            this.lblSumEnergy = new System.Windows.Forms.Label();
            this.lblEmptyEnergySum = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.grp_static_hostility.SuspendLayout();
            this.SuspendLayout();
            // 
            // bttnCreateLab
            // 
            this.bttnCreateLab.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.bttnCreateLab.Location = new System.Drawing.Point(10, 113);
            this.bttnCreateLab.Name = "bttnCreateLab";
            this.bttnCreateLab.Size = new System.Drawing.Size(92, 42);
            this.bttnCreateLab.TabIndex = 0;
            this.bttnCreateLab.Text = "Create lab";
            this.bttnCreateLab.UseVisualStyleBackColor = true;
            this.bttnCreateLab.Click += new System.EventHandler(this.bttnCreateLab_Click);
            // 
            // bttnClose
            // 
            this.bttnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bttnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bttnClose.Location = new System.Drawing.Point(12, 501);
            this.bttnClose.Name = "bttnClose";
            this.bttnClose.Size = new System.Drawing.Size(75, 42);
            this.bttnClose.TabIndex = 1;
            this.bttnClose.Text = "Close";
            this.bttnClose.UseVisualStyleBackColor = true;
            this.bttnClose.Click += new System.EventHandler(this.bttnClose_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Labyrinth size:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(134, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Number of energy sources:";
            // 
            // tbLabSize
            // 
            this.tbLabSize.Location = new System.Drawing.Point(86, 22);
            this.tbLabSize.Name = "tbLabSize";
            this.tbLabSize.Size = new System.Drawing.Size(77, 20);
            this.tbLabSize.TabIndex = 6;
            this.tbLabSize.Text = "100";
            // 
            // tbNumberOfEnergySources
            // 
            this.tbNumberOfEnergySources.Location = new System.Drawing.Point(168, 51);
            this.tbNumberOfEnergySources.Name = "tbNumberOfEnergySources";
            this.tbNumberOfEnergySources.Size = new System.Drawing.Size(58, 20);
            this.tbNumberOfEnergySources.TabIndex = 7;
            this.tbNumberOfEnergySources.Text = "25";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.bttnLoadLab);
            this.groupBox2.Controls.Add(this.tbNumberOfEnergySinks);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.tbLabSize);
            this.groupBox2.Controls.Add(this.bttnCreateLab);
            this.groupBox2.Controls.Add(this.tbNumberOfEnergySources);
            this.groupBox2.Location = new System.Drawing.Point(14, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(238, 172);
            this.groupBox2.TabIndex = 20;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Set up labyrinth";
            // 
            // bttnLoadLab
            // 
            this.bttnLoadLab.Location = new System.Drawing.Point(126, 113);
            this.bttnLoadLab.Name = "bttnLoadLab";
            this.bttnLoadLab.Size = new System.Drawing.Size(100, 42);
            this.bttnLoadLab.TabIndex = 10;
            this.bttnLoadLab.Text = "Load existing lab";
            this.bttnLoadLab.UseVisualStyleBackColor = true;
            this.bttnLoadLab.Click += new System.EventHandler(this.bttnLoadLab_Click);
            // 
            // tbNumberOfEnergySinks
            // 
            this.tbNumberOfEnergySinks.Location = new System.Drawing.Point(168, 80);
            this.tbNumberOfEnergySinks.Name = "tbNumberOfEnergySinks";
            this.tbNumberOfEnergySinks.Size = new System.Drawing.Size(58, 20);
            this.tbNumberOfEnergySinks.TabIndex = 9;
            this.tbNumberOfEnergySinks.Text = "25";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(121, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Number of energy sinks:";
            // 
            // trackBar1
            // 
            this.trackBar1.LargeChange = 50;
            this.trackBar1.Location = new System.Drawing.Point(2, 47);
            this.trackBar1.Maximum = 140;
            this.trackBar1.Minimum = -140;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(232, 45);
            this.trackBar1.TabIndex = 22;
            this.trackBar1.TickFrequency = 20;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(197, 31);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 13);
            this.label4.TabIndex = 23;
            this.label4.Text = "Friendly";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 31);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(39, 13);
            this.label5.TabIndex = 24;
            this.label5.Text = "Hostile";
            // 
            // tbLab
            // 
            this.tbLab.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbLab.Location = new System.Drawing.Point(258, 12);
            this.tbLab.Multiline = true;
            this.tbLab.Name = "tbLab";
            this.tbLab.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbLab.Size = new System.Drawing.Size(317, 531);
            this.tbLab.TabIndex = 28;
            // 
            // lblMin
            // 
            this.lblMin.AutoSize = true;
            this.lblMin.Location = new System.Drawing.Point(8, 70);
            this.lblMin.Name = "lblMin";
            this.lblMin.Size = new System.Drawing.Size(23, 13);
            this.lblMin.TabIndex = 29;
            this.lblMin.Text = "min";
            // 
            // lblMax
            // 
            this.lblMax.AutoSize = true;
            this.lblMax.Location = new System.Drawing.Point(214, 70);
            this.lblMax.Name = "lblMax";
            this.lblMax.Size = new System.Drawing.Size(26, 13);
            this.lblMax.TabIndex = 30;
            this.lblMax.Text = "max";
            // 
            // grp_static_hostility
            // 
            this.grp_static_hostility.Controls.Add(this.label6);
            this.grp_static_hostility.Controls.Add(this.label5);
            this.grp_static_hostility.Controls.Add(this.lblMax);
            this.grp_static_hostility.Controls.Add(this.label4);
            this.grp_static_hostility.Controls.Add(this.lblMin);
            this.grp_static_hostility.Controls.Add(this.trackBar1);
            this.grp_static_hostility.Location = new System.Drawing.Point(12, 326);
            this.grp_static_hostility.Name = "grp_static_hostility";
            this.grp_static_hostility.Size = new System.Drawing.Size(240, 121);
            this.grp_static_hostility.TabIndex = 31;
            this.grp_static_hostility.TabStop = false;
            this.grp_static_hostility.Text = "Static hostility";
            this.grp_static_hostility.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(114, 80);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 13);
            this.label6.TabIndex = 31;
            this.label6.Text = "label6";
            // 
            // lblSinkEnergySum
            // 
            this.lblSinkEnergySum.AutoSize = true;
            this.lblSinkEnergySum.Location = new System.Drawing.Point(20, 247);
            this.lblSinkEnergySum.Name = "lblSinkEnergySum";
            this.lblSinkEnergySum.Size = new System.Drawing.Size(88, 13);
            this.lblSinkEnergySum.TabIndex = 32;
            this.lblSinkEnergySum.Text = "Sink energy sum:";
            // 
            // lblSourceEnergySum
            // 
            this.lblSourceEnergySum.AutoSize = true;
            this.lblSourceEnergySum.Location = new System.Drawing.Point(20, 225);
            this.lblSourceEnergySum.Name = "lblSourceEnergySum";
            this.lblSourceEnergySum.Size = new System.Drawing.Size(101, 13);
            this.lblSourceEnergySum.TabIndex = 33;
            this.lblSourceEnergySum.Text = "Source energy sum:";
            // 
            // lblSumEnergy
            // 
            this.lblSumEnergy.AutoSize = true;
            this.lblSumEnergy.Location = new System.Drawing.Point(20, 197);
            this.lblSumEnergy.Name = "lblSumEnergy";
            this.lblSumEnergy.Size = new System.Drawing.Size(123, 13);
            this.lblSumEnergy.TabIndex = 34;
            this.lblSumEnergy.Text = "Energy sum of entire lab:";
            // 
            // lblEmptyEnergySum
            // 
            this.lblEmptyEnergySum.AutoSize = true;
            this.lblEmptyEnergySum.Location = new System.Drawing.Point(20, 270);
            this.lblEmptyEnergySum.Name = "lblEmptyEnergySum";
            this.lblEmptyEnergySum.Size = new System.Drawing.Size(96, 13);
            this.lblEmptyEnergySum.TabIndex = 35;
            this.lblEmptyEnergySum.Text = "Empty field energy:";
            // 
            // frmCreateLabirynth
            // 
            this.AcceptButton = this.bttnCreateLab;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.bttnClose;
            this.ClientSize = new System.Drawing.Size(587, 547);
            this.Controls.Add(this.lblEmptyEnergySum);
            this.Controls.Add(this.lblSumEnergy);
            this.Controls.Add(this.lblSourceEnergySum);
            this.Controls.Add(this.lblSinkEnergySum);
            this.Controls.Add(this.grp_static_hostility);
            this.Controls.Add(this.tbLab);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.bttnClose);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmCreateLabirynth";
            this.Text = "Create labyrinth";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.grp_static_hostility.ResumeLayout(false);
            this.grp_static_hostility.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bttnCreateLab;
        private System.Windows.Forms.Button bttnClose;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbLabSize;
        private System.Windows.Forms.TextBox tbNumberOfEnergySources;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.TextBox tbLab;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbNumberOfEnergySinks;
        private System.Windows.Forms.Label lblMin;
        private System.Windows.Forms.Label lblMax;
        private System.Windows.Forms.GroupBox grp_static_hostility;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblSinkEnergySum;
        private System.Windows.Forms.Label lblSourceEnergySum;
        private System.Windows.Forms.Label lblSumEnergy;
        private System.Windows.Forms.Label lblEmptyEnergySum;
        private System.Windows.Forms.Button bttnLoadLab;
    }
}