using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace DCMaster
{
    public partial class frmLogBook : Form
    {
        NpgsqlConnectionStringBuilder cnsb = new NpgsqlConnectionStringBuilder();

        public frmLogBook(NpgsqlConnectionStringBuilder csb)
        {
            InitializeComponent();
            cnsb = csb;
            bs.DataSource= loadLogBook();
            dgvLogBook.DataSource = bs;
        }

        DataTable loadLogBook()
        {
            using (NpgsqlConnection cnn = new NpgsqlConnection(cnsb.ConnectionString))
            {
                cnn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    cmd.Connection = cnn;
                    cmd.CommandText = "SELECT * FROM logbook";
                    DataTable dt = new DataTable();
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                    return dt;
                }
            }

        }
    }
}
