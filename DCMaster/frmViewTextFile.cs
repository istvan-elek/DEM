using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DCMaster
{
    public partial class frmViewTextFile : Form
    {

        public frmViewTextFile(string filename)
        {
            InitializeComponent();
            loadFile(filename);
        }
        void loadFile(string lbname)
        {
            string[] lines =File.ReadAllLines(lbname);
            tbText.Lines = lines;
            string[] firstLine = tbText.Lines[0].Split('\t');
            for (int i = 0; i < firstLine.Length; i++)
            {
                label1.Text += firstLine[i] + "     ";
            }
            int numOfLines = 1;
            var lin = tbText.Lines;
            var newLines = lin.Skip(numOfLines);
            tbText.Lines = newLines.ToArray();
            tbText.Select(0, 0);
            if (Path.GetExtension(lbname) == ".iter") { this.Text = "File: " + lbname + "   -->>  Line count:" + tbText.Lines.Length; }
            else { this.Text = "File: " + lbname; }
        }
    }
}
