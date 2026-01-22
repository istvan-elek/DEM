using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;


namespace DCMaster
{
    public partial class DisplayCharts : Form
    {
        int tbLThick = 2;
        string Title = "";
        DataTable dt;
        string filename;

        public DisplayCharts(string flag, string fname)
        {
            filename = fname;
            InitializeComponent();
            this.Tag = flag;
            ch1.ChartAreas[0].AxisX.Minimum = 0;
            ch1.ChartAreas[0].AxisY.Minimum = 0;
            dt=loadIterationData();

            if (flag == "poptotal")
            {
                ch1.Series[0].BorderWidth = tbLThick;
                ch1.Titles.Add("Population");
                showPolpulation();
            }

            if (flag == "energy")
            {
                ch1.Series[0].BorderWidth = tbLThick;
                ch1.Titles.Add("Gathered energy");
                showGathered_energy();
            }

            if (flag == "fittness")
            {
                ch1.Series[0].BorderWidth = tbLThick;
                ch1.Titles.Add("Fittness");
                showFittness();
            }

            if (flag == "entropy")
            {
                ch1.Series[0].BorderWidth = tbLThick;
                ch1.Titles.Add("Entropy");
                showEntropy();
            }
        }

        DataTable loadIterationData()
        {
            DataTable tbl = new DataTable();
            tbl.Columns.Add("col0");
            tbl.Columns.Add("Col1");
            tbl.Columns.Add("Col2");
            tbl.Columns.Add("Col3");
            string[] input;
            //string appfolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) ;
            try
            {
                input = File.ReadAllLines( filename); // Application.StartupPath + "\\iteration.txt");
            }
            catch (Exception)
            {

                input = null; ;
            }


            string[] input2 = new string[input.Length - 1];
            string[] colNames= input[0].Split('\t');

            for (int i=1; i < input.Length; i++)
            {
                input2[i-1] = Regex.Replace(input[i], @"\s+", " ");
            }
            tbl.Columns[0].ColumnName = colNames[0];
            tbl.Columns[1].ColumnName = colNames[1];
            tbl.Columns[2].ColumnName = colNames[2];
            tbl.Columns[3].ColumnName = colNames[3];

            for (int i=0; i< input2.Length; i++)
            {
                DataRow row = tbl.NewRow();
                string[] line = input2[i].Split(' ');
                foreach(string s in line)
                {
                    row[0] = line[0]; //step count
                    row[1] = line[1]; //num of workers
                    row[2] = line[2]; //gathered energy
                    row[3] = line[3]; //entropy
                }
                tbl.Rows.Add(row);
            }
            return tbl;
        }



        private void drawDiagram(string title, int flag)
        {
            if (dt != null)
            {
                ch1.ChartAreas[0].AxisX.Title = dt.Columns[0].ColumnName;   //"X axis";
                ch1.ChartAreas[0].AxisY.Title = dt.Columns[flag].ColumnName;  // "Y axis";
                ch1.BackColor = System.Drawing.Color.White;
                ch1.Titles.Clear();
                ch1.Titles.Add(title);
                if (Title != "")
                {
                    ch1.Titles.Clear();
                    ch1.Titles.Add(Title);
                }
                Single[] x = new Single[dt.Rows.Count];
                Single[] y = new Single[dt.Rows.Count];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    try
                    {
                    x[i] = Convert.ToSingle(dt.Rows[i][0]);
                    y[i] = Convert.ToSingle(dt.Rows[i][flag]);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Only numeric fields can be displayed");
                        return;
                    }
                }
                ch1.Series[0].ChartType = SeriesChartType.Line;
                ch1.Series[0].BorderWidth = Convert.ToInt16(tbLThick);
                ch1.Series[0].Points.DataBindXY(x, y);
                ch1.Series[0].Name = title;   // "Selected data";
                ch1.Series[0].Color = System.Drawing.Color.Red;
                if (flag==3) 
                {
                    Int32 mini = 0;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (Convert.ToInt32(dt.Rows[i][3]) < mini) mini = Convert.ToInt32(dt.Rows[i][3]);
                    }
                    var t = Math.Log10(-mini);
                    string u = t.ToString("##");
                    Int32 v = Int32.Parse(u) /3;
                    double min = Math.Pow(10, Int32.Parse(u));
                    int tick = (int)min / 5;
                    ch1.ChartAreas[0].AxisY.Maximum = 0;
                    ch1.ChartAreas[0].AxisY.Minimum = -min; 
                    ch1.ChartAreas[0].AxisY.MajorGrid.Interval = tick;
                }
            }
        }


        void showFittness()
        {
            drawDiagram("Population's fittness", 4);
        }

        void showEntropy()
        {
            drawDiagram("Population's SUM entropy", 3);
        }


        void showGathered_energy()
        {
            drawDiagram("Population's SUM gathered energy", 2);
        }


        void showPolpulation()
        {
            drawDiagram("Population", 1);
        }




        private void ch1_MouseMove(object sender, MouseEventArgs e)
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
    }
}
