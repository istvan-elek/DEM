using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DC
{
    public partial class frmDCMain : Form
    {
        public frmDCMain()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            int locY = Screen.PrimaryScreen.WorkingArea.Height - this.Height;
            //int locY = Screen.PrimaryScreen.WorkingArea.Height / 10;
            this.Location = new Point(0,locY);
        }

        private void bttnAbout_Click(object sender, EventArgs e)
        {
            frmAbout AboutDC = new frmAbout();
            AboutDC.ShowDialog();
        }

        private void bttnGuide_Click(object sender, EventArgs e)
        {
            frmGuide frmG = new frmGuide(); 
            frmG.Show();
        }

        private void bttnDemo_Click(object sender, EventArgs e)
        {
            var exe = Path.Combine(AppContext.BaseDirectory, "DCDemo.exe");
            Process.Start(new ProcessStartInfo(exe) { UseShellExecute = true });
        }

        private void bttnMaster_Click(object sender, EventArgs e)
        {
            var exe = Path.Combine(AppContext.BaseDirectory, "DCMaster.exe");
            Process.Start(new ProcessStartInfo(exe) { UseShellExecute = true });
        }

        private void bttnAnalyser_Click(object sender, EventArgs e)
        {
            var exe = Path.Combine(AppContext.BaseDirectory, "DCAnalyser.exe");
            Process.Start(new ProcessStartInfo(exe) { UseShellExecute = true });
        }

        private void bttnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void bttnMinimize_MouseMove(object sender, MouseEventArgs e)
        {
            this.Focus();
        }

        private void frmDCMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            closeOpenApps();
        }

        void closeOpenApps()
        {
            Process[] procsDataStock = Process.GetProcessesByName("DCMaster");
            foreach (Process item in procsDataStock)
            {
                item.CloseMainWindow();
                item.Kill();
            }
            Process[] procsWorkflowBuilder = Process.GetProcessesByName("DCAnalyser");
            foreach (Process item in procsWorkflowBuilder)
            {
                item.CloseMainWindow();
                item.Kill();
            }
            Process[] procsCatalog = Process.GetProcessesByName("DCDemo");
            foreach (Process item in procsCatalog)
            {
                item.CloseMainWindow();
                item.Kill();
            }

            //Process[] procsMosaic = Process.GetProcessesByName("mosaic");
            //foreach (Process item in procsMosaic)
            //{
            //    item.CloseMainWindow();
            //    item.Kill();
            //}
        }
    }
}
