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
    public partial class AboutWumpus : Form
    {
        //var versionInfo = FileVersionInfo.GetVersionInfo(Assembly.GetEntryAssembly().Location);

        public AboutWumpus()
        {
            InitializeComponent();
            this.Text = "About the probabilistic Wumpus-world";
            lblCompanyName.Text= "Company name:" + Application.CompanyName;
            lblProductName.Text = "Product name:" + Application.ProductName;
            lblVersionName.Text = "Version:" + Application.ProductVersion;
            lblCopyright.Text = "Copyright \u00A9 Istvan Elek, 2017";
            textBox1.Text = "This is a probabilistic Wumpus-word handling system. You can create any size of labirynths with any DEM workers and wumpuses, traps and gold. Wumpus analyzer helps you to understand what happened to the actors in this peculiar world. The system can save all events and actors into a Postgres database, so PostgreSQL 9.6 has to be installed previously. This window show you what DEM workers do, if you click Start button. If you click Batch button, you can start the process with database connection without any graphic illustration.";
        }
    }
}
