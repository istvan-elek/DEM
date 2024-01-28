using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DC
{
    public partial class frmGuide : Form
    {
        public frmGuide()
        {
            InitializeComponent();
            var uri = new Uri(Application.StartupPath + @"\DCusersguide.pdf");
            this.webBrowser1.Navigate(uri);
        }
    }
}
