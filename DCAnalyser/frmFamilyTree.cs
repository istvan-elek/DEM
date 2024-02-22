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
        string mainNode;
        public frmFamilyTree(DataTable dt, string title, Boolean fmt)
        {
            InitializeComponent();
            dtFam = dt;
            this.Text = title;
            hScrollB.Maximum = dtFam.Rows.Count ;
            fmtree = fmt;

            //vScrollB.Maximum = 1000;
        }

        public frmFamilyTree(DataTable dt, string title, Boolean fmt, string mNode, int numofforks)
        {
            InitializeComponent();
            dtFam = dt;
            this.Text = title;
            hScrollB.Maximum = dtFam.Rows.Count;
            fmtree = fmt;
            mainNode = mNode;
            forks = numofforks;
            this.Text="node: " + mainNode + ",   forks: " + forks;
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
            int ofsety = 200;
            int ofsetx = 200;
            float angle = (float)(Math.PI / forks);
            Pen pen = new Pen(Color.Coral, 1);
            Brush myBrush = Brushes.LightSalmon;
            Font drawFont = new Font("Arial", 9);
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;

            //PointF p1 = new Point(this.ClientSize.Width / 2 + r / 2, r );
            //float x1 = this.ClientSize.Width / 2 + r / 2;
            //float y1 = r;
            //float x2 = p1.X - (float)(Math.Cos( angle) * ofsetx);
            //float y2 = p1.X - (float)( Math.Sin( angle) * ofsety);
            //e.DrawLine(pen, x1, y1, x2, y2);
            float halfAngle=angle/2;
            for (int i = 0; i < forks; i++)
            {
                float x1 = this.ClientSize.Width / 2 + r / 2;
                float y1 = r;
                float x2 = x1 - (float)(Math.Cos(Math.PI + halfAngle + angle*i) * ofsetx);
                float y2 = y1 - (float)(Math.Sin(Math.PI + halfAngle + angle *i) * ofsety);
                e.DrawLine(pen, x1, y1, x2, y2);
                //PointF p2 = new PointF((float)(Math.Cos(angle * i) * ofsetx), (float)(Math.Sin(angle * i) * ofsety));
                //e.DrawLine(pen, p1, p2);
            }
            e.FillEllipse(myBrush, this.ClientSize.Width / 2 - r / 2, 0, r + r, r + r);
            e.DrawString(mainNode, drawFont, drawBrush, new Point(this.ClientSize.Width / 2 + r / 2, r/2+2), sf);
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
                string[] p = row[1].ToString().Split(','); // ebben van a worker ID-je
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
          int ofsety = 60;
            int ofsetx = 70;
            int posx = (int)((hScrollB.Value + e.X) / (ofsetx));
            int posy = (int)((vScrollB.Value + e.Y) / (ofsety));
            string[] s = dtFam.Rows[posx][1].ToString().Split(',');
            string sClicked = "";
            try
            {
                sClicked = s[posy-1].ToString();
                this.Text = posx + ", " + posy + " ---> value:" + sClicked;
            }
            catch (Exception) { }
            int k = 0;
            foreach (DataRow dr in dtFam.Rows) 
            {
                string[] selRow = dr[1].ToString().Split(','); //dtFam.Rows[posx][1].ToString().Split(',');
                try
                {
                    if (selRow[posy-1] == sClicked) k++;
                }
                catch (Exception)
                {
                }

            }
            this.Text = posx + ", " + posy + " ---> value:" + sClicked + ",  k:" + k.ToString();
            frmFamilyTree ftree = new frmFamilyTree(dtFam, "ftree", true,sClicked.ToLower(), k);
            ftree.Show();
        }
    }
}
