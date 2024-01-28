namespace DCAnalyser
{
    partial class frmIterationCompare
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmIterationCompare));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbDb1 = new System.Windows.Forms.ComboBox();
            this.cmbDb2 = new System.Windows.Forms.ComboBox();
            this.ch1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbDb3 = new System.Windows.Forms.ComboBox();
            this.chkDb3 = new System.Windows.Forms.CheckBox();
            this.cmbSelectingTable = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.bttnCapture = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ch1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Select database #1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Select database #2";
            // 
            // cmbDb1
            // 
            this.cmbDb1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbDb1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDb1.FormattingEnabled = true;
            this.cmbDb1.Location = new System.Drawing.Point(6, 22);
            this.cmbDb1.Name = "cmbDb1";
            this.cmbDb1.Size = new System.Drawing.Size(308, 21);
            this.cmbDb1.Sorted = true;
            this.cmbDb1.TabIndex = 3;
            // 
            // cmbDb2
            // 
            this.cmbDb2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbDb2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDb2.FormattingEnabled = true;
            this.cmbDb2.Location = new System.Drawing.Point(6, 83);
            this.cmbDb2.Name = "cmbDb2";
            this.cmbDb2.Size = new System.Drawing.Size(308, 21);
            this.cmbDb2.Sorted = true;
            this.cmbDb2.TabIndex = 4;
            // 
            // ch1
            // 
            chartArea1.Name = "ChartArea1";
            this.ch1.ChartAreas.Add(chartArea1);
            this.ch1.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            legend1.IsTextAutoFit = false;
            legend1.Name = "Legend1";
            this.ch1.Legends.Add(legend1);
            this.ch1.Location = new System.Drawing.Point(0, 0);
            this.ch1.Name = "ch1";
            series1.BackImageWrapMode = System.Windows.Forms.DataVisualization.Charting.ChartImageWrapMode.TileFlipX;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Color = System.Drawing.Color.Red;
            series1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            series1.Legend = "Legend1";
            series1.MarkerSize = 3;
            series1.Name = "Series1";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Color = System.Drawing.Color.LimeGreen;
            series2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            series2.Legend = "Legend1";
            series2.MarkerSize = 3;
            series2.Name = "Series2";
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series3.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            series3.Enabled = false;
            series3.Legend = "Legend1";
            series3.Name = "Series3";
            this.ch1.Series.Add(series1);
            this.ch1.Series.Add(series2);
            this.ch1.Series.Add(series3);
            this.ch1.Size = new System.Drawing.Size(806, 638);
            this.ch1.TabIndex = 5;
            this.ch1.Text = "chart1";
            this.ch1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ch1_MouseMove);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.bttnCapture);
            this.splitContainer1.Panel1.Controls.Add(this.label5);
            this.splitContainer1.Panel1.Controls.Add(this.cmbDb3);
            this.splitContainer1.Panel1.Controls.Add(this.chkDb3);
            this.splitContainer1.Panel1.Controls.Add(this.cmbSelectingTable);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.cmbDb2);
            this.splitContainer1.Panel1.Controls.Add(this.cmbDb1);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.label6);
            this.splitContainer1.Panel2.Controls.Add(this.ch1);
            this.splitContainer1.Size = new System.Drawing.Size(1137, 638);
            this.splitContainer1.SplitterDistance = 327;
            this.splitContainer1.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 143);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Select database #3";
            this.label5.Visible = false;
            // 
            // cmbDb3
            // 
            this.cmbDb3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDb3.FormattingEnabled = true;
            this.cmbDb3.Location = new System.Drawing.Point(6, 159);
            this.cmbDb3.Name = "cmbDb3";
            this.cmbDb3.Size = new System.Drawing.Size(308, 21);
            this.cmbDb3.Sorted = true;
            this.cmbDb3.TabIndex = 11;
            this.cmbDb3.Visible = false;
            // 
            // chkDb3
            // 
            this.chkDb3.AutoSize = true;
            this.chkDb3.Location = new System.Drawing.Point(171, 136);
            this.chkDb3.Name = "chkDb3";
            this.chkDb3.Size = new System.Drawing.Size(118, 17);
            this.chkDb3.TabIndex = 10;
            this.chkDb3.Text = "Show 3rd database";
            this.chkDb3.UseVisualStyleBackColor = true;
            this.chkDb3.CheckedChanged += new System.EventHandler(this.chkDb3_CheckedChanged);
            // 
            // cmbSelectingTable
            // 
            this.cmbSelectingTable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSelectingTable.FormattingEnabled = true;
            this.cmbSelectingTable.Items.AddRange(new object[] {
            "numofworkers",
            "energy",
            "fittness",
            "entropy"});
            this.cmbSelectingTable.Location = new System.Drawing.Point(6, 224);
            this.cmbSelectingTable.Name = "cmbSelectingTable";
            this.cmbSelectingTable.Size = new System.Drawing.Size(157, 21);
            this.cmbSelectingTable.TabIndex = 7;
            this.cmbSelectingTable.SelectedIndexChanged += new System.EventHandler(this.cmbSelectingTable_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 198);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Compare what?";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(2, 3);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(81, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "Selected point: ";
            this.label6.Visible = false;
            // 
            // bttnCapture
            // 
            this.bttnCapture.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bttnCapture.AutoSize = true;
            this.bttnCapture.Image = ((System.Drawing.Image)(resources.GetObject("bttnCapture.Image")));
            this.bttnCapture.Location = new System.Drawing.Point(12, 571);
            this.bttnCapture.Name = "bttnCapture";
            this.bttnCapture.Size = new System.Drawing.Size(54, 55);
            this.bttnCapture.TabIndex = 7;
            this.bttnCapture.Text = "Capture";
            this.bttnCapture.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.bttnCapture.UseVisualStyleBackColor = true;
            this.bttnCapture.Click += new System.EventHandler(this.bttnCapture_Click);
            // 
            // frmIterationCompare
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1137, 638);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmIterationCompare";
            this.Text = "Comparison of simulations";
            this.Load += new System.EventHandler(this.frmIteration_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ch1)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbDb1;
        private System.Windows.Forms.ComboBox cmbDb2;
        private System.Windows.Forms.DataVisualization.Charting.Chart ch1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ComboBox cmbSelectingTable;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbDb3;
        private System.Windows.Forms.CheckBox chkDb3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button bttnCapture;
    }
}