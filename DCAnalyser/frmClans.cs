using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace DCAnalyser
{
    public partial class frmClans : Form
    {
        int numOfClans;
        string[] clanGodfathers;
        List<int> Members = new List<int>();
        int ofset;
        int step = 0;
        public frmClans(string[] gNames, List<int> counts)
        {
            InitializeComponent();
            clanGodfathers = gNames;
            Members=counts;
            numOfClans=clanGodfathers.Length;
            ofset = this.ClientSize.Width / 10;
            //hScrollB.Maximum = gNames.Length;
        }

        private void frmClans_Paint(object sender, PaintEventArgs ee)
        {
            Graphics e = CreateGraphics();
            e.Clear(Color.SeaShell);
            //hScrollB.Maximum = clanGodfathers.Length * ofset;
            hScrollB.SmallChange = ofset;
            hScrollB.LargeChange = 10 * ofset;
            Brush myBrush = Brushes.Orange;
            Font drawFont = new Font("Arial", 8);
            int fh=drawFont.Height;
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            List<string> centers = new List<string>();
            float r=0;
            float r3 = 0;
            for (int i = 0; i < numOfClans; i++) 
            {
                float r2 = (float)Math.Log10( Members[i]) *100;
                e.FillEllipse(myBrush, r3-step, 0,  r2, r2 );
                string c=((r3 - step) + r2 / 2).ToString() + ';' + (r2/2).ToString();
                centers.Add(c);
                r3 += r2;
                r = 1.5F * r2 + ofset;
            }
            hScrollB.Maximum = clanGodfathers.Length * /*ofset**/ (int)(r/2);
            r = 0;
            r3 = 0;
            for (int i = 0; i < numOfClans; i++)
            {
                float r2 = (float)Math.Log10(Members[i]) * 100;
                r3 += r2 + r;
                e.DrawString(clanGodfathers[i], new Font("Arial", 8), drawBrush, Convert.ToSingle(centers[i].Split(';')[0]), Convert.ToSingle(centers[i].Split(';')[1]), sf);
                e.DrawString(Members[i].ToString(), new Font("Arial", 7), Brushes.Green, Convert.ToSingle(centers[i].Split(';')[0]), Convert.ToSingle(centers[i].Split(';')[1]) + fh, sf);
                r = 1.5F * r2 + ofset;
            }

        }

        private void hScrollB_Scroll(object sender, ScrollEventArgs e)
        {
            step = hScrollB.Value;
            this.Invalidate();
        }

        private void frmClans_MouseMove(object sender, MouseEventArgs e)
        {
            this.Text=e.X.ToString() + ", " + e.Y.ToString();
        }
    }
}
