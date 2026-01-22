using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DCAnalyser
{
    public partial class frmPicture : Form
    {
        Bitmap bmp;
        string Title;

        public frmPicture(string title, Image img)
        {
            InitializeComponent();
            this.Text = title;
            Title = title;
            bmp = new Bitmap(img);
            pBox.Image = img;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            Single rate = Convert.ToSingle(pBox.Image.Width) / Convert.ToSingle(pBox.ClientSize.Width);
            int xpos = (int)(Convert.ToSingle(e.X) * rate + 0.5F);
            if (xpos == pBox.Image.Width) { xpos = pBox.Image.Width - 1; }
            int ypos = (int)(Convert.ToSingle(e.Y) * rate + 0.5F);
            if (ypos == pBox.Image.Height) { ypos = pBox.Image.Height - 1; }
            Color clr = bmp.GetPixel(xpos, ypos);
            string strClr = "#" + clr.R.ToString("X2") + clr.G.ToString("X2") + clr.B.ToString("X2");
            string dimGray = "#" + Color.FromKnownColor(KnownColor.DimGray).R.ToString("X2") + Color.FromKnownColor(KnownColor.DimGray).G.ToString("X2") + Color.FromKnownColor(KnownColor.DimGray).B.ToString("X2");
            string redClr = "#" + Color.FromKnownColor(KnownColor.DarkRed).R.ToString("X2") + Color.FromKnownColor(KnownColor.DarkRed).G.ToString("X2") + Color.FromKnownColor(KnownColor.DarkRed).B.ToString("X2");
            string goldClr = "#" + Color.FromKnownColor(KnownColor.DarkGoldenrod).R.ToString("X2") + Color.FromKnownColor(KnownColor.DarkGoldenrod).G.ToString("X2") + Color.FromKnownColor(KnownColor.DarkGoldenrod).B.ToString("X2");
            string blackClr = "#" + Color.FromKnownColor(KnownColor.Black).R.ToString("X2") + Color.FromKnownColor(KnownColor.Black).G.ToString("X2") + Color.FromKnownColor(KnownColor.Black).B.ToString("X2");
            if (strClr == dimGray) { this.Text = Title + " - WUMPUS at x:" + xpos + ", y:" + ypos + " position"; }
            if (strClr == redClr) { this.Text = Title + " - TRAP at x:" + xpos + ", y:" + ypos + " position"; }
            if (strClr == goldClr) { this.Text = Title + " - GOLD at x:" + xpos + ", y:" + ypos + " position"; }
            if (strClr == blackClr) { this.Text = Title + " - empty field at x:" + xpos + ", y:" + ypos + " position"; }   
        }

        private void frmPicture_Resize(object sender, EventArgs e)
        {
            pBox.Size = new Size(this.ClientSize.Height, this.ClientSize.Height );
            pBox.Location = new Point(this.ClientSize.Width / 2 - pBox.Width / 2, 0);
        }

        //private void btnZoomIn_Click(object sender, EventArgs e)
        //{
        //    pBox.Width = (int)(pBox.Width*1.2);
        //    pBox.Height = (int)(pBox.Height * 1.2);
        //}
    }
}
