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
            string path = Path.Combine(AppContext.BaseDirectory, "docs", "dcusersguide.pdf");
            var uri = new Uri(path);
            this.webBrowserGuide.Source = uri;
            var uriTheory = new Uri(Path.Combine(AppContext.BaseDirectory, "docs", "theory.pdf")); 
            this.webBrowserTheory.Source = uriTheory;
            
        }
    }
}
