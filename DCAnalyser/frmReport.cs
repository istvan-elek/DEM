using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DCAnalyser
{
    public partial class frmReport : Form
    {
        string fname;
        public frmReport(string filename)
        {
            InitializeComponent();
            fname = filename;
        }

        private void frmReport_Load(object sender, EventArgs e)
        {
            string myfolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\DCreports\";
            try
            {
                label1.Text = System.IO.File.ReadAllText(myfolder + fname + ".rep");
                stlblFileName.Text = myfolder + fname + ".rep";
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message + ".   Report file is missing since the process has not completed yet. Try it later when process terminated." , "Missing report file", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
            }
        }

 

    }
}
