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

namespace DCAnalyser
{
    public partial class frmGuide : Form
    {
        public frmGuide(string fname)
        {
            InitializeComponent();

            string path = Path.Combine(AppContext.BaseDirectory, "docs", "dcusersguide.pdf");
            var uri = new Uri(path);
            this.webView21.Source = uri;

            
        }

    }
}
