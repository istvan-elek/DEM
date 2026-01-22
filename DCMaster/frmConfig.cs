using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DCMaster
{
    public partial class frmConfig : Form
    {
        string configFileName;

        public frmConfig(string fname)
        {
            InitializeComponent();
            configFileName = fname;
            loadConfigFile();
        }

        void loadConfigFile()
        {
            string[] cfgString = System.IO.File.ReadAllLines(configFileName);
            foreach (string line in cfgString)
            {
                tbConfig.AppendText(line + Environment.NewLine);
            }
        }

        private void bttnQuit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bttnSave_Click(object sender, EventArgs e)
        {
            System.IO.File.WriteAllText(configFileName, tbConfig.Text);
            //MessageBox.Show("Changes have been saved", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }




    }
}
