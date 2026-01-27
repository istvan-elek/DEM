using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace DC
{
    public partial class frmAbout : Form
    {
        public frmAbout()
        {
            FileVersionInfo masterverionInfo = FileVersionInfo.GetVersionInfo(Application.StartupPath + "\\dcmaster.exe");
            string masterVersion = masterverionInfo.FileVersion;
            FileVersionInfo analysererionInfo = FileVersionInfo.GetVersionInfo(Application.StartupPath + "\\dcanalyser.exe");
            string analyserVersion = analysererionInfo.FileVersion;
            InitializeComponent();
            this.Text = "Information about DEM centre";
            lblCompanyName.Text = "Company name:" + Application.CompanyName;
            lblProductName.Text = "Product name:" + Application.ProductName + " (DC)";
            lblVersionNameDC.Text = "DC Versions -- " + "DCMaster: " + masterVersion + ",  DCAnalyser: " + analyserVersion;
            //var versInfo = FileVersionInfo.GetVersionInfo(Application.StartupPath + @"\DCMaster.exe");
            //lblVersionDCMaster.Text = "DC Master Version:" + versInfo.ProductVersion; 
            //versInfo = FileVersionInfo.GetVersionInfo(Application.StartupPath + @"\DCAnalyser.exe");
            //lblVersionDCAnalyser.Text = "DC Analyser Version:" + versInfo.ProductVersion;
            lblCopyright.Text = "Copyright \u00A9 István Elek, 2023, 2024, 2025, 2026";
            textBox1.Text = "This is the DEM centre, which is the heart of digital evolution. You can create any size of labirynths with any DEM workers and wumpuses, traps and gold. The Analyser  helps you to understand what happened to the evolutional workers in this peculiar world. The system saves all events and workers into a Postgres database, so PostgreSQL 9.6 has to be installed previously. Use the DCMaster to start thousands of DEM workers to discover the world. Start DCAnalyser in order to analyse, compare invasions, missions and DEM workers' fates." + Environment.NewLine + "You can use this software free of charge, but only for scientific research. You need to refer to DEM Centre if you are publishing the results achieved with it. If you download this software you accept these restrictions.";
            //textBox2.Text = "You can use this software free of charge, but only for scientific research. You need to refer to DEM Centre if you are publishing the results achieved with it. If you download this software you accept restrictions above.";
        }
    }
}
