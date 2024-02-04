using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DCAnalyser
{
    public partial class frmCompareImprints : Form
    {

        SQLiteConnectionStringBuilder[] cnsb = new SQLiteConnectionStringBuilder[2];
        DataTable dt1;
        DataTable dt2;
        List<String> filenames = new List<String>();
        string fold = "";
        string appFolder;
        int labSize;
        int k=0;

        public frmCompareImprints(int labsize, string appfold)
        {
            InitializeComponent();
            labSize= labsize;   
            appFolder = appfold;
        }

        private void bttnLoadDB_Click(object sender, EventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();
            of.Filter = "*.s3db|*.s3db";
            if (of.ShowDialog() == DialogResult.OK)
            {
                if (k<2)
                {
                    cnsb[k] = new SQLiteConnectionStringBuilder();
                    cnsb[k].DataSource = of.FileName;
                    loadImprint(of.FileName);
                    filenames.Add(of.FileName);
                    k++;
                }
                else
                {
                    MessageBox.Show("Too many files were selected, try again");
                    filenames.Clear();
                    k = 0;
                }
            }
        }

        void loadImprint(string filename)
        {
            cnsb[k].DataSource = filename;
            string sqlCommand = "SELECT imprint FROM workers";
            DataTable dtwork = loadTableData(sqlCommand, cnsb[k]);
            Image imgImpr = imprint2Bmp(dtwork);
            frmPicture2 frmPic = new frmPicture2("Imprints of " + Path.GetFileNameWithoutExtension(filename), imgImpr, dtwork, appFolder);
            frmPic.Enabled = false;
            frmPic.Show();
        }


        Bitmap imprint2Bmp(DataTable dt)
        {
            Bitmap impBmp = new System.Drawing.Bitmap(labSize, labSize, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            Color clrEmpty = Color.Black;
            Color source = Color.Red;
            Color sink = Color.RoyalBlue;

            for (int i = 0; i < labSize; i++)
            {
                for (int j = 0; j < labSize; j++)
                {
                    impBmp.SetPixel(j, i, clrEmpty);
                }
            }

            for (int j = 0; j < dt.Rows.Count ; j++)
            {

                string[] pos = dt.Rows[j][0].ToString().Split(';');
                for (int k = 0; k < pos.Length-1; k++)
                {
                    string[] coord = pos[k].Split(',');
                    if (int.Parse(coord[2]) < 0)
                    { impBmp.SetPixel(int.Parse(coord[0]), int.Parse(coord[1]), sink); }
                    else { impBmp.SetPixel(int.Parse(coord[0]), int.Parse(coord[1]), source); }
                }


            }
            return impBmp;
        }

        DataTable loadTableData(string sqlCommand, SQLiteConnectionStringBuilder cnsb)
        {
            this.Cursor = Cursors.WaitCursor;
            DataTable dt = new DataTable();
            using (SQLiteConnection cnn = new SQLiteConnection(cnsb.ConnectionString))
            {
                cnn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(sqlCommand, cnn))
                {
                    try
                    {
                        SQLiteDataReader dr = cmd.ExecuteReader();
                        dt.Load(dr);
                        this.Cursor = Cursors.Default;
                        return dt;
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show(err.Message);
                    }
                }
            }
            return dt;
        }

        private void bttnCompare_Click(object sender, EventArgs e)
        {
            frmPicture2 frmPic = new frmPicture2("Compare Imprints", loadImprints());
            frmPic.Enabled = false;
            frmPic.Show();
        }

        Bitmap compareBitmaps(Bitmap bmp1, Bitmap bmp2)
        {
            Bitmap bmp = new Bitmap(labSize,labSize);
            for (int i = 0; i<labSize; i++)
            {
                for (int j = 0; j<labSize; j++)
                {
                    if (bmp1.GetPixel(i,j) == bmp2.GetPixel(i, j)) { bmp.SetPixel(i,j, Color.Black); }
                    else { bmp.SetPixel(i, j, Color.White); }
                }
            }
            return bmp;
        }


        Bitmap loadImprints()
        {
            cnsb[0].DataSource = filenames[0];
            cnsb[1].DataSource = filenames[1];
            string sqlCommand = "SELECT imprint FROM workers";
            DataTable dtwork1 = loadTableData(sqlCommand, cnsb[0]);
            DataTable dtwork2 = loadTableData(sqlCommand, cnsb[1]);
            Bitmap bmp1 = imprint2Bmp(dtwork1);
            Bitmap bmp2 = imprint2Bmp(dtwork2);
            return compareBitmaps(bmp1, bmp2);
        }

        private void frmCompareImprints_FormClosed(object sender, FormClosedEventArgs e)
        {
            for (int i = Application.OpenForms.Count - 1; i >= 0; i--)
            {
                if (Application.OpenForms[i].Text.Contains("Imprints") )
                    Application.OpenForms[i].Close();
            }
        }
    }
}
