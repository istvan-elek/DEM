using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace DCMaster
{
    public partial class DisplayCharts2 : Form
    {
        public DisplayCharts2(string fname, string title)
        {
            InitializeComponent();
            //this.TopMost = true;
            string[] dataPoints = loadIterationData(fname, title);
            drawDiagram(title, dataPoints,"stepCount " + title);
        }

        private void ch1_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                var result = ch1.HitTest(e.X, e.Y, ChartElementType.DataPoint);
                if (result.ChartElementType == ChartElementType.DataPoint)
                {
                    var prop = result.Object as DataPoint;
                    lblSelectedPoint.Text = "Selected point: " + prop.ToString();
                    lblSelectedPoint.Visible = true;
                }
                else { lblSelectedPoint.Visible = false; }
            }
            catch (Exception)
            {
            }
        }


        string[] loadIterationData(string fname, string title)
        {
            string[] input;
            try
            {
                input = System.IO.File.ReadAllLines(fname); 
            }
            catch (Exception)
            {
                input = null; ;
            }
            string[] stout = new string[input.Length - 1];
            string[] input2 = new string[input.Length - 1];
            string[] colNames = input[0].Split('\t');

            for (int i = 1; i < input.Length; i++)
            {
                input2[i - 1] = Regex.Replace(input[i], @"\s+", " ");
            }
            if (title == "population")
            {
                for (int i = 0; i < input2.Length; i++)
                {
                    string[] line = input2[i].Split(' ');
                    stout[i] = line[1];
                }
            }
            if (title == "energy")
            {
                for (int i = 0; i < input2.Length; i++)
                {
                    string[] line = input2[i].Split(' ');
                    stout[i] = line[2];
                }
            }
            if (title == "entropy")
            {
                for (int i = 0; i < input2.Length; i++)
                {
                    string[] line = input2[i].Split(' ');
                    stout[i] = line[3];
                }
            }
            if (title=="fittness")
            {
                for (int i = 0; i < input2.Length; i++)
                {
                    string[] line = input2[i].Split(' ');
                    double pop = Convert.ToDouble(line[1]);
                    double erg = Convert.ToDouble(line[2]);
                    double fit = Math.Log10(pop * erg + 0.001D);
                    stout[i] = fit.ToString();
                }
            }
         return stout;
        }

        private void drawDiagram(string title, string[] dataPnt, string colNames)
        {
            ch1.ChartAreas[0].AxisX.TitleFont= new Font("Tahoma", 11, FontStyle.Regular);
            ch1.ChartAreas[0].AxisY.TitleFont = new Font("Tahoma", 11, FontStyle.Regular);
            ch1.ChartAreas[0].AxisX.Title = colNames.Split(' ')[0];   //"X axis";
            ch1.ChartAreas[0].AxisY.Title = colNames.Split(' ')[1];  // "Y axis";
            ch1.BackColor = System.Drawing.Color.White;
            ch1.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.DashDotDot;
            ch1.ChartAreas[0].AxisX.MajorGrid.LineColor = System.Drawing.Color.DarkGray;
            ch1.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.DashDotDot;
            ch1.ChartAreas[0].AxisY.MajorGrid.LineColor = System.Drawing.Color.DarkGray;
            ch1.Titles.Clear();
            //ch1.Titles.Add(title);
            float[] x = new float[Convert.ToInt32(dataPnt.Length)];
            float[] y = new float[Convert.ToInt32(dataPnt.Length)];
            for (int i=0; i<dataPnt.Length; i++)
            {
                x[i] = i;
                y[i] = Convert.ToSingle(dataPnt[i]);
            }
            ch1.ChartAreas[0].AxisX.Minimum = 0;
            ch1.ChartAreas[0].AxisY.Minimum = 0;
            if (title == "entropy")
            {
                ch1.ChartAreas[0].AxisY.Maximum = 0;
                ch1.ChartAreas[0].AxisY.Minimum = y.Min();
                string stick = ((int)(ch1.ChartAreas[0].AxisY.Minimum / 5)).ToString();
                int l = stick.Length;
                string num = stick.Substring(0, 2);
                for (int k=2; k<l; k++) { num += "0"; }
                ch1.ChartAreas[0].AxisY.Minimum = int.Parse(num) * (l+1);
            }
            if (title=="fittness") ch1.ChartAreas[0].AxisY.Title = "fittness = Log10 ( population * energy )";
            ch1.Series[0].ChartType = SeriesChartType.Line;
            ch1.Series[0].BorderWidth = 2;
            ch1.Series[0].Points.DataBindXY(x, y);
            ch1.Series[0].Name = title;   
            ch1.Series[0].Color = System.Drawing.Color.Red;
        }
    }
}
