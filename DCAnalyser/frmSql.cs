using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using Npgsql;

namespace DCAnalyser
{
    public partial class frmSql : Form
    {
        SQLiteConnectionStringBuilder cnsb = new SQLiteConnectionStringBuilder();

        public frmSql(SQLiteConnectionStringBuilder cnb)
        {
            InitializeComponent();
            cnsb = cnb;
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
    }
}
