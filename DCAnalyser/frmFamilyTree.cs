using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace DCAnalyser
{
    public partial class frmFamilyTree : Form
    {
        DataTable dtFam;
        int step = 0;
        Boolean fmtree=false;
        int forks;
        List<string> pForks;
        string mainNode;
        public frmFamilyTree(DataTable dt, string title, Boolean fmt)
        {
            InitializeComponent();
            dtFam = dt;
            this.Text = title + "  with " + dt.Rows.Count + " items";
            hScrollB.Maximum = dtFam.Rows.Count ;
            fmtree = fmt;

            //vScrollB.Maximum = 1000;
        }

        public frmFamilyTree(DataTable dt, string title, Boolean fmt, string mNode, int numofforks, List<string> pntForks)
        {
            InitializeComponent();
            dtFam = dt;
            this.Text = title;
            hScrollB.Maximum = dtFam.Rows.Count;
            fmtree = fmt;
            mainNode = mNode;
            forks = numofforks;
            pForks=pntForks;
            this.Text="workerID: " + mainNode + ",   Children: " + forks;
            //vScrollB.Maximum = 1000;
        }

        private void DrawParents(object sender, PaintEventArgs e)
        {
            if (fmtree) { refreshFamTree(); } // ha family tree kirajzolása a kérés
            else { refreshDrawParents();} // ha a szülők kirajzolása a kérés           
        }

        void refreshFamTree()
        {
            Graphics e = CreateGraphics();
            e.Clear(Color.SeaShell);
            int r = 20; // a node körök sugara
            int r2 = 30;
            int ofsety = 150;
            int ofsetx = this.ClientSize.Width / forks+1;// 200;
            int halfofsetx = ofsetx / 2;
            Pen pen = new Pen(Color.Coral, 1);
            Brush myBrush = Brushes.LightSalmon;
            Font drawFont = new Font("Arial", 9);
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;

            for (int i = 0; i < forks; i++)   //meghúzza a vonalakat és a köröket
            {
                float x1 = this.ClientSize.Width / 2 + r / 2;
                float y1 = r;
                float x2 = halfofsetx + i * ofsetx;  
                float y2 = ofsety;      
                e.DrawLine(pen, x1, y1, x2, y2);
                e.FillEllipse(Brushes.YellowGreen, x2-r2/2, y2-r2/2, r2, r2);
                e.DrawString(pForks[i], new Font("Arial", 8), drawBrush, new PointF(x2 - r2/2+4, y2 - r2/2 + 7));
            }
            e.FillEllipse(myBrush, this.ClientSize.Width / 2 - r / 2, 0, 2*r, 2*r); // kirajzolja a fő workert és az ID-jét
            e.DrawString(mainNode, drawFont, drawBrush, new Point(this.ClientSize.Width / 2 + r / 2, r/2 + 2), sf);
        }

        void refreshDrawParents()
        {
            Graphics e = CreateGraphics();
            e.Clear(Color.SeaShell);
            int r = 20; // a node körök sugara
            int ofsety = 60;
            int ofsetx = 70;
            hScrollB.Maximum = dtFam.Rows.Count * ofsetx;
            hScrollB.SmallChange = ofsetx;
            hScrollB.LargeChange = 10*ofsetx;
            Pen pen = new Pen(Color.Coral, 1);
            Brush myBrush = Brushes.LightSalmon;
            Font drawFont = new Font("Arial", 9);
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            int k = 0;
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            foreach (DataRow row in dtFam.Rows)
            {
                string[] p = row[1].ToString().Split(','); // ebben vannak a parents azonosítók

                Point[] points = new Point[p.Length];
                for (int i=0; i<p.Length; i++)
                {
                    points[i] = new Point(k * ofsetx + r - step, ofsety * (i + 1)+1);
                }
                try
                {
                    e.DrawLines(pen, points);
                }
                catch (Exception)
                {
                    //MessageBox.Show("hiba");
                }
                string id = row[0].ToString();
                e.FillRectangle(Brushes.Goldenrod, k * ofsetx - step, 0, 2 * r + 2, 2 * r + 2);
                e.DrawString(id, drawFont, drawBrush, new Point( k * ofsetx + r  - step, r / 2 + 2), sf);
                for (int i = 0; i < p.Length; i++)
                {
                    e.FillEllipse(myBrush, k * ofsetx - step , (i + 1) * ofsety, r + r, r + r);
                    e.DrawString(p[i], drawFont, drawBrush, new Point(points[i].X , points[i].Y + r / 2 + 2), sf);
                }
                k++;
            }
        }


        private void hScrollB_Scroll(object sender, ScrollEventArgs e)
        {
            step=hScrollB.Value;
            this.Invalidate();
        }

        private void frmFamilyTree_MouseMove(object sender, MouseEventArgs e)
        {
  
        }

        private void frmFamilyTree_MouseDown(object sender, MouseEventArgs e)
        { 
            this.Cursor = Cursors.WaitCursor;            
            int ofsety = 60;
            int ofsetx = 70;
            int posx = (int)((hScrollB.Value + e.X) / (ofsetx));
            int posy = (int)((vScrollB.Value + e.Y) / (ofsety));
            string[] s = dtFam.Rows[posx][1].ToString().Split(',');
            string sClicked = "";
            try
            {
                sClicked = s[posy-1].ToString();
                //this.Text = posx + ", " + posy + " ---> value:" + sClicked;
            }
            catch (Exception) { }
            int forks = 0;
            List<string> lp = new List<string>();
            foreach (DataRow dr in dtFam.Rows) 
            {
                string[] selRow = dr[1].ToString().Split(',');
                string[] sellp = dr[0].ToString().Split(',');
                try
                {
                    if (selRow[posy - 1] == sClicked)
                    {
                        lp.Add(dr[0].ToString());                       
                        forks++;
                    }
                }
                catch (Exception)
                {
                }
            }
            if (forks == 0) return;
            this.Text = "Selected workerID:" + sClicked + " with children:" + forks;
            frmFamilyTree ftree = new frmFamilyTree(dtFam, "ftree", true,sClicked.ToLower(), forks, lp);
            ftree.Show();
            this.Cursor = Cursors.Default;
        }
    }
}
