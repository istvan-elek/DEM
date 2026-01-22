using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Data.SQLite;
using System.IO;
using System.Xml.Linq;

namespace DCAnalyser
{
    public partial class DisplayCharts : Form
    {
        SQLiteConnectionStringBuilder cnsb = new SQLiteConnectionStringBuilder();
        string appfolder;  // = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        string wkrId;
        int labID;


        public DisplayCharts(SQLiteConnectionStringBuilder connStrBld, string appfold, string flag, string wkr_id, int labId)
        {
            InitializeComponent();
            appfolder = appfold;
            this.Text = "Current database: " + Path.GetFileNameWithoutExtension( connStrBld.DataSource);
            wkrId = wkr_id;
            labID = labId;
            cnsb = connStrBld;
            this.Tag = flag;
            splitContainer1.Panel2Collapsed = true;
            ch1.ChartAreas[0].AxisX.Minimum = 0;
            ch1.ChartAreas[0].AxisY.Minimum = 0;

            if (flag == "poptotal")
            {
                ch1.Series[0].BorderWidth = 2;//Convert.ToInt16(tbLThick.Text);
                ch1.Titles.Add("Population");
                showPolpulation();
            }

            if (flag == "energy")
            {
                ch1.Series[0].BorderWidth = 1; // Convert.ToInt16(tbLThick.Text);
                ch1.Titles.Add("Energy");
                showGathered_energy();
            }

            if (flag == "fittness")
            {
                ch1.Series[0].BorderWidth = 2; // Convert.ToInt16(tbLThick.Text);
                ch1.Titles.Add("Fittness");
                showFittness();
            }
            if (flag=="entropy")
            {
                ch1.Series[0].BorderWidth = 2;
                ch1.Titles.Add("Entropy");
                showEntropy();
            }
            if (flag == "any")
            {
                ch1.Series[0].BorderWidth = 2; // Convert.ToInt16(tbLThick.Text);
                splitContainer1.Panel2Collapsed = false;
                ch1.Titles.Add(tbSql.Text);
            }
        }



        DataTable loadTableData(string sqlCommand)
        {
            DataTable dtab = new DataTable();
            using (SQLiteConnection cnn = new SQLiteConnection(cnsb.ConnectionString))
            {
                cnn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(sqlCommand, cnn))
                {
                    try
                    {
                    SQLiteDataReader dr = cmd.ExecuteReader();
                    lblLastSql.Text = cmd.CommandText;
                    dtab.Load(dr);
                    return dtab;
                    }
                    catch (SQLiteException e)
                    {
                        MessageBox.Show("Error in the SQL command: " + e.Message, "Sql error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return null;
                    }
                }
            }
        }



        private void drawDiagram(DataTable dt, string title)
        {
            if (dt != null)
            {
                try
                {
                    ch1.ChartAreas[0].AxisX.Title = dt.Columns[0].ColumnName;   //"X axis";
                    ch1.ChartAreas[0].AxisY.Title = dt.Columns[1].ColumnName;  // "Y axis";
                }
                catch (Exception)
                {
                    if (dt.Columns.Count==1) MessageBox.Show("Failed query display. The query result may be wrong. Column0=" + dt.Columns[0].ColumnName + ", Column1 is missing", "Failed query display", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                ch1.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.DashDotDot;
                ch1.ChartAreas[0].AxisX.MajorGrid.LineColor = System.Drawing.Color.DarkGray;
                ch1.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.DashDotDot;
                ch1.ChartAreas[0].AxisY.MajorGrid.LineColor = System.Drawing.Color.DarkGray;
                ch1.BackColor = System.Drawing.Color.White;
                ch1.Titles.Clear();
                ch1.Titles.Add(title);
                if (tbTitle.Text != "")
                {
                    ch1.Titles.Clear();
                    ch1.Titles.Add(tbTitle.Text);
                }
                Single[] x = new Single[dt.Rows.Count];
                Single[] y = new Single[dt.Rows.Count];

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    try
                    {
                    x[i] = Convert.ToSingle(dt.Rows[i][0]);
                    y[i] = Convert.ToSingle(dt.Rows[i][1]);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Only numeric fields can be displayed");
                        return;
                    }
                }
                ch1.ChartAreas[0].AxisX.Minimum= 0;
                if (title.Contains("entropy"))
                {
                    ch1.ChartAreas[0].AxisY.Maximum = 0;
                    ch1.ChartAreas[0].AxisY.Minimum = y.Min();
                    string stick = ((int)(ch1.ChartAreas[0].AxisY.Minimum / 5)).ToString();
                    int l = stick.Length;
                    string num = stick.Substring(0, 2);
                    for (int k = 2; k < l; k++) { num += "0"; }
                    ch1.ChartAreas[0].AxisY.Minimum = int.Parse(num) * l;                    
                }

                ch1.Series[0].ChartType = SeriesChartType.Line;
                ch1.Series[0].BorderWidth = 2; // Convert.ToInt16(tbLThick.Text);
                ch1.Series[0].Points.DataBindXY(x, y);
                ch1.Series[0].Name = title;   // "Selected data";
                ch1.Series[0].Color = System.Drawing.Color.Red;
            }
        }
        void drawEntropyDiagram(DataTable dt, string title)
        {
            if (dt != null)
            {
                try
                {
                    ch1.ChartAreas[0].AxisX.Title = dt.Columns[0].ColumnName;   //"X axis";
                    ch1.ChartAreas[0].AxisY.Title = dt.Columns[1].ColumnName;  // "Y axis";
                }
                catch (Exception)
                {
                    if (dt.Columns.Count == 1) MessageBox.Show("Failed query display. The query result may be wrong. Column0=" + dt.Columns[0].ColumnName + ", Column1 is missing", "Failed query display", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                ch1.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.DashDotDot;
                ch1.ChartAreas[0].AxisX.MajorGrid.LineColor = System.Drawing.Color.DarkGray;
                ch1.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.DashDotDot;
                ch1.ChartAreas[0].AxisY.MajorGrid.LineColor = System.Drawing.Color.DarkGray;
                ch1.BackColor = System.Drawing.Color.White;
                ch1.Titles.Clear();
                ch1.Titles.Add(title);
                if (tbTitle.Text != "")
                {
                    ch1.Titles.Clear();
                    ch1.Titles.Add(tbTitle.Text);
                }
                Single[] x = new Single[dt.Rows.Count];
                Single[] y = new Single[dt.Rows.Count];

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    try
                    {
                        x[i] = Convert.ToSingle(dt.Rows[i][0]);
                        y[i] = Convert.ToSingle(dt.Rows[i][1]);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Only numeric fields can be displayed");
                        return;
                    }
                }
                ch1.ChartAreas[0].AxisX.Minimum = 0;
                ch1.ChartAreas[0].AxisY.Maximum = 0;
                ch1.ChartAreas[0].AxisY.Minimum = y.Min();
                string stick = ((int)(ch1.ChartAreas[0].AxisY.Minimum / 5)).ToString();
                int l = stick.Length;
                string num = stick.Substring(0, 2);
                for (int k = 2; k < l; k++) { num += "0"; }
                ch1.ChartAreas[0].AxisY.Minimum = int.Parse(num) * l;

                ch1.Series[0].ChartType = SeriesChartType.Line;
                ch1.Series[0].BorderWidth = 2; // Convert.ToInt16(tbLThick.Text);
                ch1.Series[0].Points.DataBindXY(x, y);
                ch1.Series[0].Name = title;   // "Selected data";
                ch1.Series[0].Color = System.Drawing.Color.Red;
            }
        }


        private void tbSql_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                drawDiagram(loadTableData(tbSql.Text.Trim()), tbTitle.Text);
            }
        }

        void showFittness()
        {
            DataTable dt = loadTableData("select numofworkers, energy from iteration where stepcount=0");
            int init_num_of_workers = Convert.ToInt16(dt.Rows[0][0]);
            Int32 init_energy = Convert.ToInt32(dt.Rows[0][1]);
            string sqlCommand = "select stepcount, log10((energy*numofworkers)/" + (init_energy) + "+0.1) as fittness from iteration order by stepcount";
            tbSql.Text = sqlCommand;
            drawDiagram(loadTableData(sqlCommand), "Fittness");
        }

        void showEntropy()
        {
            string sqlcommand = "SELECT stepcount, entropy from iteration order by stepcount";
            DataTable dt = loadTableData(sqlcommand);
            //int init_num_of_workers = Convert.ToInt16(dt.Rows[0][0]);
            //Int32 init_energy = Convert.ToInt32(dt.Rows[0][1]);
            //string sqlCommand = "select stepcount, log10((energy*numofworkers)/" + (init_energy) + "+0.1) as fittness from iteration order by stepcount";
            tbSql.Text = sqlcommand;
            drawEntropyDiagram(dt, "Entropy");
        }


        void showGathered_energy()
        {
            string sqlCommand = "SELECT stepcount, energy from iteration  order by stepcount";
            tbSql.Text = sqlCommand;
            drawDiagram(loadTableData(sqlCommand), "Energy");
        }

        void showPolpulation()
        {
            string sqlCommand = "SELECT stepcount, numofworkers from iteration order by stepcount";
            tbSql.Text = sqlCommand;
            drawDiagram(loadTableData(sqlCommand),"Population");
        }

        private void bttnHideShow_Click(object sender, EventArgs e)
        {
            splitContainer1.Panel2Collapsed = !splitContainer1.Panel2Collapsed;
        }


        private void ch1_MouseMove(object sender, MouseEventArgs e)
        {
            var result = ch1.HitTest(e.X, e.Y, ChartElementType.DataPoint);
            if (result.ChartElementType == ChartElementType.DataPoint)
            {
                var prop = result.Object as DataPoint;
                label5.Text = "Selected point: " + prop.ToString();
                label5.Visible = true;
            }
            else { label5.Visible = false; }

        }

        private void bttnCapture_Click(object sender, EventArgs e)
        {
            string dirname = appfolder + "Figures\\";
            Directory.CreateDirectory(dirname);
            string[] files = Directory.GetFiles(dirname);
            List<int> file = new List<int>();
            foreach (string fname in files)
            {
                file.Add(int.Parse(Path.GetFileNameWithoutExtension(fname)));
            }
            file.Sort();
            string fn=dirname + (file.Count+1) + ".png";
            Image img = captureForm(this, setupRectangle(this));
            img.Save(fn);
            MessageBox.Show("Form content has captured");
        }

        Rectangle setupRectangle(Control cont)
        {
            int locx, locy;
            locx = (cont.Location.X + (cont.Size.Width - cont.ClientSize.Width)/2);
            locy = (cont.Location.Y + (cont.Size.Height - cont.ClientSize.Height)/2)+4;  
            int sizex, sizey;
            sizex = cont.Size.Width - 2 * (cont.Size.Width - cont.ClientSize.Width);
            sizey = cont.Size.Height - (int)(1.3 * (Single)(cont.Size.Height - cont.ClientSize.Height));

            //locx = (cont.Location.X + (cont.Size.Width - cont.ClientSize.Width) / 2);
            //locy = (cont.Location.Y + (cont.Size.Height - cont.ClientSize.Height) / 2) + (int)(1.5F * (Single)(toolStrip1.Height));
            //int sizex, sizey;
            //sizex = cont.Size.Width - 1 * (cont.Size.Width - cont.ClientSize.Width);
            //sizey = cont.Size.Height - toolStrip1.Height - (int)((Single)(cont.Size.Height - cont.ClientSize.Height));
            Rectangle rec = new Rectangle(new Point(locx, locy), new Size(sizex, sizey));
            return rec;
        }

        Bitmap captureForm(Control wnd, Rectangle rc)
        {
            Bitmap bitmap = null;
            using (Graphics graphics = wnd.CreateGraphics())
            {
                bitmap = new Bitmap(rc.Width, rc.Height, graphics);
                using (Graphics graphics2 = Graphics.FromImage(bitmap))
                {
                    graphics2.CopyFromScreen(rc.X, rc.Y, 0, 0, rc.Size, CopyPixelOperation.SourceCopy);
                }
            }
            return bitmap;
        }
    }
}
