namespace DCMaster
{
    partial class DisplayCharts
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DisplayCharts));
            this.ch1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.lblSelectedPoint = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ch1)).BeginInit();
            this.SuspendLayout();
            // 
            // ch1
            // 
            this.ch1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea1.Name = "ChartArea1";
            this.ch1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.ch1.Legends.Add(legend1);
            this.ch1.Location = new System.Drawing.Point(0, 25);
            this.ch1.Name = "ch1";
            series1.BackImageWrapMode = System.Windows.Forms.DataVisualization.Charting.ChartImageWrapMode.TileFlipX;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Legend = "Legend1";
            series1.MarkerSize = 3;
            series1.Name = "Series1";
            this.ch1.Series.Add(series1);
            this.ch1.Size = new System.Drawing.Size(631, 384);
            this.ch1.TabIndex = 0;
            this.ch1.Text = "chart1";
            this.ch1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ch1_MouseMove);
            // 
            // lblSelectedPoint
            // 
            this.lblSelectedPoint.AutoSize = true;
            this.lblSelectedPoint.Location = new System.Drawing.Point(12, 9);
            this.lblSelectedPoint.Name = "lblSelectedPoint";
            this.lblSelectedPoint.Size = new System.Drawing.Size(81, 13);
            this.lblSelectedPoint.TabIndex = 2;
            this.lblSelectedPoint.Text = "Selected point: ";
            this.lblSelectedPoint.Visible = false;
            // 
            // DisplayCharts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(633, 411);
            this.Controls.Add(this.ch1);
            this.Controls.Add(this.lblSelectedPoint);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DisplayCharts";
            this.Text = "DisplayCharts";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.ch1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataVisualization.Charting.Chart ch1;
        private System.Windows.Forms.Label lblSelectedPoint;
    }
}