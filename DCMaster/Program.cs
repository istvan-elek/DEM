using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DCMaster
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>		lab.Fields[x,y]	error CS0103: The name 'lab' does not exist in the current context	

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMain());
        }
    }
}
