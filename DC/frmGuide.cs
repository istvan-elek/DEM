using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Web.WebView2.WinForms;
using System.IO;

namespace DC
{
    public partial class frmGuide : Form
    {
        public frmGuide()
        {
            InitializeComponent();
            var uri = new Uri(Application.StartupPath + @"\dcusersguide.pdf");
            this.webBrowserGuide.Source = uri;
            var uriTheory = new Uri(Application.StartupPath + @"\theory.pdf");
            this.webBrowserTheory.Source = uriTheory;
            
        }
    }
}
