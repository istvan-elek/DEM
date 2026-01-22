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
    public partial class frmSelectGeneration : Form
    {
        public string kvantor="";
        public string generation="";
        public Boolean displayData = false;

        public frmSelectGeneration()
        {
            InitializeComponent();
            kvantor = "<";
            generation = "0";
            cmbCompare.SelectedItem = kvantor;
            cmbGenerationNumber.SelectedItem = generation;
        }


        private void bttnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void bttnOK_Click(object sender, EventArgs e)
        {
            if (kvantor != string.Empty && generation !=string.Empty)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Missing kvantor or generation number");
            }
        }

        private void cmbCompare_SelectedIndexChanged(object sender, EventArgs e)
        {
            kvantor = cmbCompare.SelectedItem.ToString();      
        }

        private void cmbGenerationNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            generation = cmbGenerationNumber.SelectedItem.ToString();
        }

        private void chbDisplayData_CheckedChanged(object sender, EventArgs e)
        {
            displayData = chbDisplayData.Checked;
 
        }
    }
}
