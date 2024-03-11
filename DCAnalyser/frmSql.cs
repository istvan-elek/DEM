using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DCAnalyser
{
    public partial class frmSql : Form
    {
        SQLiteConnectionStringBuilder cnsb = new SQLiteConnectionStringBuilder();
        int labSize;
        int level;

        public frmSql(SQLiteConnectionStringBuilder cnb)
        {
            InitializeComponent();
            cnsb = cnb;
        }


        public frmSql(SQLiteConnectionStringBuilder cnb, string sqlcommand, int lbSize, string title)
        {
            InitializeComponent();
            cnsb = cnb;
            DataTable dTab = loadSqlData(sqlcommand);
            if (dTab.Rows.Count > 0)
            {
                bsSql.DataSource = dTab;
                tbSql.Text = sqlcommand;
                bnSql.BindingSource = bsSql;
                dgvSql.DataSource = bsSql;
                labSize = lbSize;
                this.Text = title;
                level = int.Parse(title.Split('#')[1]);
            }
            else { MessageBox.Show("Selection is empty"); this.Text = "Selection is empty. Close this window.";  }
        }

        DataTable loadSqlData(string sqlCommand)
        {
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
                    }
                    catch (SQLiteException ex)
                    {
                        MessageBox.Show("Erron in SQL Command: " + ex.Message, "Error in SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return null;
                    }
                }
            }
            return dt;
        }

        private void tbSql_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.Cursor = Cursors.WaitCursor;
                DataTable dt = loadSqlData(tbSql.Text);
                bsSql.DataSource = dt;
                dgvSql.DataSource = bsSql;
                if (dt != null)
                {
                    cmbSql.Items.Add(tbSql.Text);
                }
                this.Cursor = Cursors.Default;
            }
        }

        private void cmbSql_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbSql.Text = cmbSql.SelectedItem.ToString();
        }

        private void bttnLoadSavedSql_Click(object sender, EventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();
            of.Filter = "Sql command text files|*.sql";
            if (of.ShowDialog() == DialogResult.OK)
            {
                tbSql.Text = System.IO.File.ReadAllText(of.FileName);
                tbSql.Focus();
                tbSql.SelectionStart = tbSql.Text.Length;
            }
        }

        private void bttnSaveSqlCommand_Click(object sender, EventArgs e)
        {
            SaveFileDialog sf = new SaveFileDialog();
            sf.Filter = "Sql command text file|*.sql";
            if (sf.ShowDialog() == DialogResult.OK)
            {
                System.IO.File.WriteAllText(sf.FileName, tbSql.Text);
            }
        }

        private void dgvSql_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex < 0) { return; }
            if (e.Button == MouseButtons.Right) 
            {
                string[] point;
                if ((dgvSql.Columns[e.ColumnIndex].Name == "imprint") || (dgvSql.Columns[e.ColumnIndex].Name == "worker_path"))
                {
                    string sl = dgvSql.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                    string[] s = sl.Split(';');
                    point = new string[s.Length];
                    DataTable dtcell = new DataTable();
                    dtcell.Columns.Add(); dtcell.Columns.Add(); dtcell.Columns.Add();
                    for (int i = 0; i < s.Length - 1; i++)
                    {
                        dtcell.Rows.Add();
                        point[i] = s[i].ToString();
                        string[] ln = point[i].Split(',');
                        for (int j = 0; j < 3; j++)
                        {
                            dtcell.Rows[i][j] = ln[j].ToString();
                        }
                    }
                        string selectedWorker = dgvSql.Rows[e.RowIndex].Cells["id"].Value.ToString();
                    if (dgvSql.Columns[e.ColumnIndex].Name == "imprint")
                    {
                        frmPicture2 frmPic = new frmPicture2("Imprint of the clicked worker: " + selectedWorker, cell2Bmp(dtcell, dgvSql.Columns[e.ColumnIndex].Name), loadSqlData("select * from labirynth"), "");
                        frmPic.Show();
                    }
                    else
                    {
                        Image img = cell2Bmp(dtcell, dgvSql.Columns[e.ColumnIndex].Name);
                        DataTable dtlab = loadSqlData("select * from labirynth");
                        Image imgLab = lab2Bmp(dtlab);

                        frmPicture2 frmPic = new frmPicture2("Workerpaths of the clicked worker: " + selectedWorker, img, imgLab, dtlab, "");
                        frmPic.Show();
                    }
                }
            }

            Bitmap lab2Bmp(DataTable dt)
            {
                //int labSize=dt.Rows.Count;
                Bitmap lBmp = new System.Drawing.Bitmap(labSize, labSize, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                Color clrEmpty = Color.Black;
                Color source = Color.Red;
                Color sink = Color.RoyalBlue;

                for (int i = 0; i < labSize; i++)
                {
                    for (int j = 0; j < labSize; j++)
                    {
                        lBmp.SetPixel(j, i, clrEmpty);
                    }
                }

                for (int j = 0; j < dt.Rows.Count - 1; j++)
                {
                    string[] pos = dt.Rows[j][1].ToString().Split(',');
                    if (int.Parse(dt.Rows[j][2].ToString()) < 0) lBmp.SetPixel(int.Parse(pos[0]), int.Parse(pos[1]), sink);
                    if (int.Parse(dt.Rows[j][2].ToString()) > 0) lBmp.SetPixel(int.Parse(pos[0]), int.Parse(pos[1]), source);
                }
                return lBmp;
            }

            Bitmap cell2Bmp(DataTable dt, string colname)
            {
                Bitmap lBmp = new System.Drawing.Bitmap(labSize, labSize, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                if (colname == "worker_pah")
                {
                    Color clrEmpty = Color.Black;
                    Color source = Color.Red;
                    Color sink = Color.RoyalBlue;

                    for (int i = 0; i < labSize; i++)
                    {
                        for (int j = 0; j < labSize; j++)
                        {
                            lBmp.SetPixel(j, i, clrEmpty);
                        }
                    }

                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        if (int.Parse(dt.Rows[j][2].ToString()) < 0) lBmp.SetPixel(Convert.ToInt32(dt.Rows[j][0]), Convert.ToInt32(dt.Rows[j][1]), sink);
                        if (int.Parse(dt.Rows[j][2].ToString()) < 0) lBmp.SetPixel(Convert.ToInt32(dt.Rows[j][0]), Convert.ToInt32(dt.Rows[j][1]), source);
                    }
                }
                else
                {
                    Color green = Color.LightGreen;
                    Color clrEmpty = Color.Black;
                    for (int i = 0; i < labSize; i++)
                    {
                        for (int j = 0; j < labSize; j++)
                        {
                            lBmp.SetPixel(j, i, clrEmpty);
                        }
                    }
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        lBmp.SetPixel(Convert.ToInt32(dt.Rows[j][0]), Convert.ToInt32(dt.Rows[j][1]), green);
                    }
                }
                return lBmp;
            }
        }

        private void ToolStripMenuShowGraphically_Click(object sender, EventArgs e)
        {
            string wher = "";
            int ind = tbSql.Text.LastIndexOf("where");
            if (ind != -1) wher = tbSql.Text.Substring(tbSql.Text.LastIndexOf("where"));
            frmFamilyTree familyTree = new frmFamilyTree(loadSqlData("select id, ltrim(parents,',') from workers  " + wher), this.Text, false);
            familyTree.Show();
        }

        //private void bttnShowFamilyTree_Click(object sender, EventArgs e)
        //{
        //    string wher="";
        //    int ind = tbSql.Text.LastIndexOf("where");
        //    if (ind != -1) { wher = tbSql.Text.Substring(tbSql.Text.LastIndexOf("where"));}           
        //    DataTable dtlist = loadSqlData("select id, parents from workers " + wher);
        //    frmFamilyTree familyTree = new frmFamilyTree(dtlist, this.Text, true);
        //    familyTree.Show();
        //}


        List<string> getParentsList(DataTable dtparent)
        {
            List<string> plist = new List<string>();
            foreach (DataRow dr in dtparent.Rows) 
            { 
                if (dr["parents"].ToString() != "") plist.Add(dr["parents"].ToString().Trim(',')); 
            }  
            return plist;
        }

        List<string> getParentsList2(DataTable dtparent)
        {
            List<string> plist = new List<string>();
            foreach (DataRow dr in dtparent.Rows)
            {
                if (dr["parents"].ToString()=="") plist.Add(dr["id"].ToString().TrimStart(','));
                else plist.Add(dr["parents"].ToString().TrimStart(',') + "," + dr["id"].ToString());
            }
            return plist;
        }


        private void largestClansToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string cmd = tbSql.Text.Replace("*", "id, parents");
            DataTable dtlist = loadSqlData(cmd);
            List<string> ParentList = getParentsList(dtlist);
            ParentList.Sort();
            var distinctNames = (from d in ParentList select d.Split(',')[0]).Distinct();
            List<int> wcountInclans = new List<int>();
            
            foreach ( var d in distinctNames)
            {
                int count = ParentList.Count(name => name.Contains(d));
                wcountInclans.Add(count);
            }
            string[] gnames = distinctNames.Select(v => v.ToString()).ToArray();

            frmClans clans = new frmClans(gnames, wcountInclans);
            clans.Show();
        }


        private void showWorkersTreeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string wher = "";
            int ind = tbSql.Text.LastIndexOf("where");
            if (ind != -1) wher = tbSql.Text.Substring(tbSql.Text.LastIndexOf("where"));
            //frmFamilyTree familyTree = new frmFamilyTree(loadSqlData("select id, ltrim(parents,',') from workers  " + wher), this.Text, false);
            List<string> workerNodes = new List<string>();
            workerNodes = getParentsList2(loadSqlData("select ltrim(parents,',') as parents, id from workers  " + wher));
            frmWorkersTree frmWorkerTree = new frmWorkersTree(workerNodes, this.Text);
            frmWorkerTree.Show();
        }

        private void frmSql_Load(object sender, EventArgs e)
        {
            if (dgvSql.Rows.Count == 0) { this.Close(); }
        }
    }
}
