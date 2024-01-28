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
using System.IO;

namespace DCMaster
{
    public partial class frmSetupParameters : Form
    {
        NpgsqlConnectionStringBuilder cnsb = new NpgsqlConnectionStringBuilder();
        //string[] parameters;
        public IList<String> parameters = new List<String>();
        string parameterFileName = Application.StartupPath + "\\parameters.txt";   //@"\init.par";

        //public frmSetupParameters(NpgsqlConnectionStringBuilder cnb)
        //{
        //    InitializeComponent();
        //    cnsb = cnb;
        //    dbTools dbtLoadTableData = new dbTools(cnb);
        //    dgvParameters.DataSource = dbtLoadTableData.loadTableData("select * from parameters order by parameter_id");
        //    dgvParameters.Columns[0].ReadOnly = true;
        //    dgvParameters.Columns[2].ReadOnly = true;
        //}

        public frmSetupParameters()
        {
            InitializeComponent();
            parameters = System.IO.File.ReadAllLines(parameterFileName);
            dgvParameters.Columns.Add("Name", "Name");
            dgvParameters.Columns.Add("Value", "Value");
            foreach (String line in parameters) { dgvParameters.Rows.Add(line.Split(';')[0], line.Split(';')[1]); };
        }


        private void dgvParameters_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //string fieldName = dgvParameters.Columns[e.ColumnIndex].Name;
            if (e.ColumnIndex==1)
            {
                string val = dgvParameters.SelectedCells[0].Value.ToString();
                parameters[e.RowIndex] = dgvParameters.Rows[e.RowIndex].Cells[0].Value.ToString() + ";" + val;
            }
            File.WriteAllLines(parameterFileName, parameters);
        }

        //void updateField(string fieldName, int fieldValue, int id)
        //{
        //    using (NpgsqlConnection cnn = new NpgsqlConnection(cnsb.ConnectionString))
        //    {
        //        cnn.Open();
        //        using (NpgsqlCommand cmd = new NpgsqlCommand())
        //        {
        //            cmd.Connection = cnn;
        //            cmd.CommandText = "UPDATE parameters SET " + fieldName + " = " + fieldValue + " WHERE parameter_id=" + id;
        //            cmd.ExecuteNonQuery();
        //        }
        //    }
        //}

    }
}
