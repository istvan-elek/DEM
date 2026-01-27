using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using Npgsql;
using System.Windows.Forms.DataVisualization.Charting;
using System.Data.SQLite;
using System.IO;

namespace DCAnalyser
{
    public partial class frmIterationCompare : Form
    {
        SQLiteConnectionStringBuilder cnsb1 = new SQLiteConnectionStringBuilder();
        SQLiteConnectionStringBuilder cnsb2 = new SQLiteConnectionStringBuilder();
        SQLiteConnectionStringBuilder cnsb3 = new SQLiteConnectionStringBuilder();
        SQLiteConnectionStringBuilder cnsb = new SQLiteConnectionStringBuilder();
        DataTable dt1;
        DataTable dt2;
        DataTable dt3;
        //string selectedDb = "";
        string[] filenames;
        string fold = "";
        string appFolder;

        public frmIterationCompare(string[] filenam, string appfold) 
        {
            InitializeComponent();
            filenames = filenam;
            appFolder = appfold;
        }

        private void frmIteration_Load(object sender, EventArgs e)
        {
            if (filenames.Length > 1)
            {
                fold = Path.GetDirectoryName(filenames[0]);
                for(int i=0; i< filenames.Length; i++)
                {
                    filenames[i] = Path.GetFileNameWithoutExtension(filenames[i]);
                }
                cnsb1.DataSource = filenames[0];
                if (filenames.Length > 1)
                {
                    cnsb2.DataSource = filenames[1];
                    if (filenames.Length == 0)
                    {
                        cmbDb1.Items.AddRange(filenames);
                        cnsb3.DataSource = filenames[1];
                    }
                    cmbDb1.Items.AddRange(filenames);
                    cmbDb2.Items.AddRange(filenames);
                    cmbDb3.Items.AddRange(filenames);
                }
            }
            else
            {
                MessageBox.Show("There is only one simulation, comparison is impossible"); this.Close(); return;
            }
        }


        private void cmbSelectingTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDb1.SelectedIndex != cmbDb2.SelectedIndex)
            {
                string db1Name = fold + "\\" + cmbDb1.SelectedItem.ToString() + ".s3db";
                cnsb1.DataSource = db1Name;
                string db2Name = fold + "\\" + cmbDb2.SelectedItem.ToString() + ".s3db";
                cnsb2.DataSource = db2Name;
                string db3Name = "";
                if (chkDb3.Checked)
                {
                    db3Name = fold + "\\" + cmbDb3.SelectedItem.ToString() + ".s3db";
                    cnsb3.DataSource = db3Name;
                }
                string sql;

                if (cmbSelectingTable.SelectedItem.ToString() == "fittness")
                {
                    sql = "select stepcount, log10(energy*numofworkers+0.01) from iteration order by stepcount";
                    dt1 = loadTableData(sql, cnsb1);
                    dt2 = loadTableData(sql, cnsb2);
                    if (chkDb3.Checked) dt3 = loadTableData(sql, cnsb3);
                    drawDiagram(dt1, dt2, dt3, db1Name, db2Name, db3Name, "Comparison of  '" + cmbSelectingTable.SelectedItem + "'");
                }
                if (cmbSelectingTable.SelectedItem.ToString() == "entropy")
                {
                    sql = "SELECT stepcount," + cmbSelectingTable.SelectedItem + " FROM iteration order by stepcount";
                    dt1 = loadTableData(sql, cnsb1);
                    dt2 = loadTableData(sql, cnsb2);
                    if (chkDb3.Checked) dt3 = loadTableData(sql, cnsb3);
                    drawEntropyDiagram(dt1, dt2, dt3, db1Name, db2Name, db3Name, "Comparison of  '" + cmbSelectingTable.SelectedItem + "'");
                }
                if ((cmbSelectingTable.SelectedItem.ToString() == "numofworkers") || (cmbSelectingTable.SelectedItem.ToString() == "energy"))
                {
                    sql = "SELECT stepcount," + cmbSelectingTable.SelectedItem + " FROM iteration order by stepcount";
                    dt1 = loadTableData(sql, cnsb1);
                    dt2 = loadTableData(sql, cnsb2);
                    if (chkDb3.Checked) dt3 = loadTableData(sql, cnsb3);
                    drawDiagram(dt1, dt2, dt3, db1Name, db2Name, db3Name, "Comparison of  '" + cmbSelectingTable.SelectedItem + "'");
                }

                //drawDiagram(dt1, dt2, dt3, db1Name, db2Name, db3Name, "Comparison of  '" + cmbSelectingTable.SelectedItem + "'");

            }
            else { MessageBox.Show("Identical databases"); }
        }



        DataTable loadTableData(string sqlCommand, SQLiteConnectionStringBuilder connsb)
        {
            DataTable dtab = new DataTable();
            using (SQLiteConnection cnn = new SQLiteConnection(connsb.ConnectionString))
            {
                cnn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(sqlCommand, cnn))
                {
                    SQLiteDataReader dr = cmd.ExecuteReader();
                    dtab.Load(dr);
                    return dtab;
                }
            }
        }



        private void drawDiagram(DataTable dt1, DataTable dt2, DataTable dt3, string title1, string title2, string title3, string mainTitle)
        {
            if (dt1 != null)
            {
                ch1.ChartAreas[0].AxisX.Title = dt1.Columns[0].ColumnName;   //"X axis";
                ch1.ChartAreas[0].AxisY.Title = dt1.Columns[1].ColumnName;  // "Y axis";
                ch1.ChartAreas[0].AxisX.Minimum = 0;
                ch1.ChartAreas[0].AxisY.Minimum = 0;
                ch1.BackColor = System.Drawing.Color.White;
                ch1.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.DashDotDot;
                ch1.ChartAreas[0].AxisX.MajorGrid.LineColor = System.Drawing.Color.DarkGray;
                ch1.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.DashDotDot;
                ch1.ChartAreas[0].AxisY.MajorGrid.LineColor = System.Drawing.Color.DarkGray;
                ch1.Titles.Clear();              
                ch1.Titles.Add(mainTitle);
                ch1.Titles[0].Font = new System.Drawing.Font("Microsoft Sans Serif", 12, FontStyle.Regular);

                Single[] x1 = new Single[dt1.Rows.Count];
                Single[] y1 = new Single[dt1.Rows.Count];

                Single[] x2 = new Single[dt2.Rows.Count];
                Single[] y2 = new Single[dt2.Rows.Count];

                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    x1[i] = Convert.ToSingle(dt1.Rows[i][0]);
                    y1[i] = Convert.ToSingle(dt1.Rows[i][1]);
                }

                ch1.Series[0].ChartType = SeriesChartType.Line;
                ch1.Series[0].BorderWidth = 2; // Convert.ToInt16(tbLThick.Text);
                ch1.Series[0].Points.DataBindXY(x1, y1);
                ch1.Series[0].Name = Path.GetFileNameWithoutExtension(title1); 
                ch1.Series[0].Color = System.Drawing.Color.Red;

                for (int i = 0; i < dt2.Rows.Count; i++)
                {
                    x2[i] = Convert.ToSingle(dt2.Rows[i][0]);
                    y2[i] = Convert.ToSingle(dt2.Rows[i][1]);
                }
                ch1.Series[1].ChartType = SeriesChartType.Line;
                ch1.Series[1].BorderWidth = ch1.Series[0].BorderWidth;
                ch1.Series[1].Points.DataBindXY(x2, y2);
                ch1.Series[1].Name = Path.GetFileNameWithoutExtension(title2);
                ch1.Series[1].Color = System.Drawing.Color.Green;
                ch1.Series[1].BorderDashStyle = ChartDashStyle.Dot;
                if (chkDb3.Checked)
                {
                    Single[] x3 = new Single[dt3.Rows.Count];
                    Single[] y3 = new Single[dt3.Rows.Count];
                    for (int i = 0; i < dt3.Rows.Count; i++)
                    {
                        x3[i] = Convert.ToSingle(dt3.Rows[i][0]);
                        y3[i] = Convert.ToSingle(dt3.Rows[i][1]);
                    }
                    ch1.Series[2].ChartType = SeriesChartType.Line;
                    ch1.Series[2].BorderWidth = ch1.Series[0].BorderWidth;
                    ch1.Series[2].Points.DataBindXY(x3, y3);
                    ch1.Series[2].Name = Path.GetFileNameWithoutExtension(title3);
                    ch1.Series[2].Color = System.Drawing.Color.Blue;
                    ch1.Series[2].BorderDashStyle = ChartDashStyle.DashDotDot;
                    ch1.Series[2].Font = new System.Drawing.Font("Microsoft Sans Serif", 10, FontStyle.Regular);
                    ch1.Series[2].Enabled = true;
                }
                ch1.Series[0].Font = new System.Drawing.Font("Microsoft Sans Serif", 10, FontStyle.Regular);
                ch1.Series[1].Font = new System.Drawing.Font("Microsoft Sans Serif", 10, FontStyle.Regular);
                ch1.ChartAreas[0].AxisX.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 12, FontStyle.Regular);
                ch1.ChartAreas[0].AxisY.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 12, FontStyle.Regular);
                ch1.ChartAreas[0].AxisX.LabelStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10, FontStyle.Regular);
                ch1.ChartAreas[0].AxisY.LabelStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10, FontStyle.Regular);
            }
        }

        void drawEntropyDiagram(DataTable dt1, DataTable dt2, DataTable dt3, string title1, string title2, string title3, string mainTitle)
        {
            ch1.ChartAreas[0].AxisX.Title = dt1.Columns[0].ColumnName;   //"X axis";
            ch1.ChartAreas[0].AxisY.Title = dt1.Columns[1].ColumnName;  // "Y axis";
            ch1.ChartAreas[0].AxisX.Minimum = 0;
            ch1.ChartAreas[0].AxisY.Minimum = 0;
            ch1.BackColor = System.Drawing.Color.White;
            ch1.Titles.Clear();
            ch1.Titles.Add(mainTitle);
            ch1.Titles[0].Font = new System.Drawing.Font("Microsoft Sans Serif", 12, FontStyle.Regular);

            Single[] x1 = new Single[dt1.Rows.Count];
            Single[] y1 = new Single[dt1.Rows.Count];

            Single[] x2 = new Single[dt2.Rows.Count];
            Single[] y2 = new Single[dt2.Rows.Count];

            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                x1[i] = Convert.ToSingle(dt1.Rows[i][0]);
                y1[i] = Convert.ToSingle(dt1.Rows[i][1]);
            }

            ch1.Series[0].ChartType = SeriesChartType.Line;
            ch1.Series[0].BorderWidth = 2;  // Convert.ToInt16(tbLThick.Text);
            ch1.Series[0].Points.DataBindXY(x1, y1);
            ch1.Series[0].Name = Path.GetFileNameWithoutExtension(title1);
            ch1.Series[0].Color = System.Drawing.Color.Red;

            for (int i = 0; i < dt2.Rows.Count; i++)
            {
                x2[i] = Convert.ToSingle(dt2.Rows[i][0]);
                y2[i] = Convert.ToSingle(dt2.Rows[i][1]);
            }
            ch1.Series[1].ChartType = SeriesChartType.Line;
            ch1.Series[1].BorderWidth = ch1.Series[0].BorderWidth;
            ch1.Series[1].Points.DataBindXY(x2, y2);
            ch1.Series[1].Name = Path.GetFileNameWithoutExtension(title2);
            ch1.Series[1].Color = System.Drawing.Color.Green;
            ch1.Series[1].BorderDashStyle = ChartDashStyle.Dot;
            if (chkDb3.Checked)
            {
                Single[] x3 = new Single[dt3.Rows.Count];
                Single[] y3 = new Single[dt3.Rows.Count];
                for (int i = 0; i < dt3.Rows.Count; i++)
                {
                    x3[i] = Convert.ToSingle(dt3.Rows[i][0]);
                    y3[i] = Convert.ToSingle(dt3.Rows[i][1]);
                }
                ch1.Series[2].ChartType = SeriesChartType.Line;
                ch1.Series[2].BorderWidth = ch1.Series[0].BorderWidth;
                ch1.Series[2].Points.DataBindXY(x3, y3);
                ch1.Series[2].Name = Path.GetFileNameWithoutExtension(title3);
                ch1.Series[2].Color = System.Drawing.Color.Blue;
                ch1.Series[2].BorderDashStyle = ChartDashStyle.DashDotDot;
                ch1.Series[2].Font = new System.Drawing.Font("Microsoft Sans Serif", 10, FontStyle.Regular);
                ch1.Series[2].Enabled = true;
            }

            ch1.ChartAreas[0].AxisY.Minimum = y1.Min();
            string stick = ((int)(ch1.ChartAreas[0].AxisY.Minimum / 5)).ToString();
            int l = stick.Length;
            string num = stick.Substring(0, 2);
            for (int k = 2; k < l; k++) { num += "0"; }
            ch1.ChartAreas[0].AxisY.Minimum = int.Parse(num) * l;

            ch1.Series[0].ChartType = SeriesChartType.Line;
            ch1.Series[0].BorderWidth = 2; // Convert.ToInt16(tbLThick.Text);
            //ch1.Series[0].Points.DataBindXY(x, y);
            //ch1.Series[0].Name = title3;   // "Selected data";
            ch1.Series[0].Color = System.Drawing.Color.Red;
            
        }

        private void chkDb3_CheckedChanged(object sender, EventArgs e)
        {
            cmbDb3.Visible = !cmbDb3.Visible;
            if (chkDb3.Checked)
            { 
                chkDb3.Text = "Hide 3rd database";
                label5.Visible = true;
                ch1.Series[2].Enabled = !ch1.Series[2].Enabled;
            }
            else
            { 
                chkDb3.Text = "Show 3rd database";
                label5.Visible = false;
                ch1.Series[2].Enabled = !ch1.Series[2].Enabled;
            }
        }



        private void ch1_MouseMove(object sender, MouseEventArgs e)
        {
            var result = ch1.HitTest(e.X, e.Y, ChartElementType.DataPoint);
            if (result.ChartElementType == ChartElementType.DataPoint)
            {
                var prop = result.Object as DataPoint;
                label6.Text = "Selected point: " + prop.ToString();
                label6.Visible = true;
            }
            else { label6.Visible = false; }

        }

        Rectangle setupRectangle(Control cont)
        {
            int locx, locy;
            locx = (cont.Location.X + (cont.Size.Width - cont.ClientSize.Width) / 2);
            locy = (cont.Location.Y + (cont.Size.Height - cont.ClientSize.Height) / 2) + 4;
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

        private void bttnCapture_Click(object sender, EventArgs e)
        {
            string dirname = appFolder + "Figures\\";
            Directory.CreateDirectory(dirname);
            string[] files = Directory.GetFiles(dirname);
            List<int> file = new List<int>();
            foreach (string fname in files)
            {
                file.Add(int.Parse(Path.GetFileNameWithoutExtension(fname)));
            }
            file.Sort();
            string fn = dirname + (file.Count + 1) + ".png";
            Image img = captureForm(ch1, setupRectangle(this));
            img.Save(fn);
            MessageBox.Show("Form content has captured");
        }
    }
}
