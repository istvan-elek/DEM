namespace DCMaster
{
    partial class DisplayCharts2
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.lblSelectedPoint = new System.Windows.Forms.Label();
            this.ch1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.ch1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblSelectedPoint
            // 
            this.lblSelectedPoint.AutoSize = true;
            this.lblSelectedPoint.Location = new System.Drawing.Point(12, 7);
            this.lblSelectedPoint.Name = "lblSelectedPoint";
            this.lblSelectedPoint.Size = new System.Drawing.Size(78, 13);
            this.lblSelectedPoint.TabIndex = 1;
            this.lblSelectedPoint.Text = "Selected point:";
            // 
            // ch1
            // 
            this.ch1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea2.Name = "ChartArea1";
            this.ch1.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.ch1.Legends.Add(legend2);
            this.ch1.Location = new System.Drawing.Point(1, 25);
            this.ch1.Name = "ch1";
            series2.BackImageWrapMode = System.Windows.Forms.DataVisualization.Charting.ChartImageWrapMode.TileFlipX;
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            series2.Legend = "Legend1";
            series2.MarkerSize = 3;
            series2.Name = "Series1";
            this.ch1.Series.Add(series2);
            this.ch1.Size = new System.Drawing.Size(797, 424);
            this.ch1.TabIndex = 2;
            this.ch1.Text = "chart1";
            this.ch1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ch1_MouseMove);
            // 
            // DisplayCharts2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.ch1);
            this.Controls.Add(this.lblSelectedPoint);
            this.Name = "DisplayCharts2";
            this.Text = "DisplayCharts2";
            ((System.ComponentModel.ISupportInitialize)(this.ch1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblSelectedPoint;
        private System.Windows.Forms.DataVisualization.Charting.Chart ch1;
    }
}