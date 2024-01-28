using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace DCAnalyser
{
    public partial class frmBrowseReports : Form
    {
        public frmBrowseReports(string fname)
        {
            InitializeComponent();
            LoadReport(fname);
            this.Text = "File: " + Path.GetFileNameWithoutExtension(fname);
        }

        void LoadReport(string fname)
        {
            lblReport.Text = System.IO.File.ReadAllText(fname);
        }
    }
}
