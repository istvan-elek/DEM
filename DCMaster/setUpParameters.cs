using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace Wump
{
    public partial class setUpParameters : UserControl
    {


        public setUpParameters() 
        {
            InitializeComponent();
        }

        public int MyProperty { get; set; }

        private NpgsqlConnectionStringBuilder _cnsb;
        public NpgsqlConnectionStringBuilder cnsb
        {
            get {return _cnsb;}
            set 
            { 
                _cnsb = value;
                dgvParameters.DataSource = loadTableData("select * from parameters ORDER BY parameter_id");
            }
        }


        public DataTable loadTableData(string sqlCommand)
        {
            DataTable dt = new DataTable();
            using (NpgsqlConnection cnn = new NpgsqlConnection(cnsb.ConnectionString))
            {
                cnn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(sqlCommand, cnn))
                {
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                }
            }
            return dt;
        }

        private void dgvParameters_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            string fieldName = dgvParameters.Columns[e.ColumnIndex].Name;
            if (e.ColumnIndex == 1)
            {
                int fieldValue = Convert.ToInt16(dgvParameters.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                int id = Convert.ToInt16(dgvParameters.Rows[e.RowIndex].Cells["parameter_id"].Value);
                updateField(fieldName, fieldValue, id);
                dgvParameters.DataSource = loadTableData("select * from parameters ORDER BY parameter_id");
            }
        }

        void updateField(string fieldName, int fieldValue, int id)
        {
            using (NpgsqlConnection cnn = new NpgsqlConnection(cnsb.ConnectionString))
            {
                cnn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    cmd.Connection = cnn;
                    cmd.CommandText = "UPDATE parameters SET " + fieldName + " = " + fieldValue + " WHERE parameter_id=" + id;
                    cmd.ExecuteNonQuery();
                }
            }
        }

    }
}
