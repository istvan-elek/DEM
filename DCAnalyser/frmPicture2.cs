using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace DCAnalyser
{
    public partial class frmPicture2 : Form
    {
        Bitmap bmpLab;
        Bitmap bmpWorkerPath;
        Bitmap lastBmpWorkerPath;
        string Title;
        int diff;
        Point startPos;
        Point endPos;
        Boolean push=false;
        DataTable lab;
        string appfolder;

        public frmPicture2(string title, Image img)
        {
            InitializeComponent();
            //appfolder = appfold;
            this.Text = title;
            Title = title;
            bmpLab = new Bitmap(img);
            picBox.Image = img;
            resetPicBox();
            diff = picBox.Width / int.Parse(tstbZoomRate.Text);
            //lab = lb;
        }


        public frmPicture2(string title, Image img, DataTable lb, string appfold)  //amikor csak a labirintust jeleníti meg
        {
            InitializeComponent();
            appfolder = appfold;
            this.Text = title;
            Title = title;
            bmpLab = new Bitmap(img);
            picBox.Image = img;
            resetPicBox();
            diff = picBox.Width / int.Parse(tstbZoomRate.Text);
            lab = lb;
        }

        public frmPicture2(string title, Image img, Image imgLab, DataTable lb, string appfold)  //amikor a labirintust és a bejárt mezőket jeleníti meg
        {
            InitializeComponent();
            appfolder = appfold;
            lab = lb;
            tsbtnChangeColor.Visible = true;
            this.Text = title;
            Title = title;
            bmpLab = new Bitmap(imgLab);
            bmpWorkerPath = new Bitmap(img);
            picBox.Image = mergeImages(bmpLab, bmpWorkerPath);
            lastBmpWorkerPath = new Bitmap(bmpWorkerPath);
            resetPicBox();
            diff = picBox.Width / int.Parse(tstbZoomRate.Text);
        }

        Bitmap mergeImages(Bitmap bmplb, Bitmap bmpwp)
        {
            Bitmap bmpMerged = new Bitmap(bmpwp);
            for (int i=0; i<bmplb.Width; i++)
            {
                for (int j=0; j<bmplb.Height; j++)
                {
                    Color clr = bmplb.GetPixel(i, j);
                    if (clr!=Color.FromArgb(255, 0, 0, 0))
                    {
                        bmpMerged.SetPixel(i, j, bmplb.GetPixel(i, j)); 
                    }
                }
            }
            return bmpMerged;
        }

        void resetPicBox()
        {
            picBox.Location = new Point(0,0);
            picBox.Width = panel1.ClientSize.Width;
            picBox.Height = panel1.ClientSize.Height;
        }

        private void tsbtnZoomIn_Click(object sender, EventArgs e)
        {
            tsbtnZoomIn.Checked = true;
            tsbtnPan.Checked = false;
            tsbtnHand.Checked = false;
            picBox.Cursor = new Cursor("lupe.ico");
        }

        private void tsbtnZoomOut_Click(object sender, EventArgs e)
        {
            if ((picBox.Width > panel1.ClientSize.Width) || (picBox.Height > panel1.ClientSize.Height))
            {
                picBox.Width -= diff*2;
                picBox.Height -= diff*2;
                picBox.Location = new Point(picBox.Location.X + diff, picBox.Location.Y + diff);
            }
        }

        private void tsbtnZoomFull_Click(object sender, EventArgs e)
        {
            resetPicBox();
        }

        private void picBox_MouseMove(object sender, MouseEventArgs e)
        {
            //tslblCursorPos.Text = e.Location.ToString();

            Color clr=new Color();
            Single rate = Convert.ToSingle(picBox.Image.Width) / Convert.ToSingle(picBox.ClientSize.Width);
            int xpos = (int)(Convert.ToSingle(e.X) * rate + 0.5F);
            if (xpos == picBox.Image.Width) { xpos = picBox.Image.Width - 1; }
            int ypos = (int)(Convert.ToSingle(e.Y) * rate + 0.5F);
            if (ypos == picBox.Image.Height) { ypos = picBox.Image.Height - 1; }
            string a = "position = '" + (xpos + "," + ypos) + "'";
            DataRow[] rows = lab.Select(a);
            string fieldValue="";
            if (rows.Length !=0)
            {
                fieldValue = rows[0][2].ToString();
            }

            try
            {
                clr = bmpLab.GetPixel(xpos, ypos);
            }
            catch (Exception)
            {
            }          
            string strClr = "#" + clr.R.ToString("X2") + clr.G.ToString("X2") + clr.B.ToString("X2");
            string RoyalBlue = "#" + Color.FromKnownColor(KnownColor.RoyalBlue).R.ToString("X2") + Color.FromKnownColor(KnownColor.RoyalBlue).G.ToString("X2") + Color.FromKnownColor(KnownColor.RoyalBlue).B.ToString("X2");
            string redClr = "#" + Color.FromKnownColor(KnownColor.Red).R.ToString("X2") + Color.FromKnownColor(KnownColor.Red).G.ToString("X2") + Color.FromKnownColor(KnownColor.Red).B.ToString("X2");
            string blackClr = "#" + Color.FromKnownColor(KnownColor.Black).R.ToString("X2") + Color.FromKnownColor(KnownColor.Black).G.ToString("X2") + Color.FromKnownColor(KnownColor.Black).B.ToString("X2"); 
            if (strClr == RoyalBlue) { this.Text = Title + " - SINK(" + fieldValue +  ") at x:" + xpos + ", y:" + ypos + " position"; }
            if (strClr == redClr) { this.Text = Title + " - SOURCE(" + fieldValue + ") at x:" + xpos + ", y:" + ypos + " position"; }
            if (strClr == blackClr) { this.Text = Title + " - empty field(" + fieldValue + ") at x:" + xpos + ", y:" + ypos + " position"; }
        }

        private void panel1_Resize(object sender, EventArgs e)
        {
            picBox.Size = new Size(panel1.ClientSize.Height, panel1.ClientSize.Height);
            picBox.Location = new Point(panel1.ClientSize.Width / 2 - picBox.Width / 2, 0);
        }

        private void picBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (tsbtnZoomIn.Checked)
            {
                picBox.Width += diff * 2;
                picBox.Height += diff * 2;
                picBox.Location = new Point(picBox.Location.X - diff, picBox.Location.Y - diff);
                int posx = panel1.ClientSize.Width / 2 - e.X;
                int posy = panel1.ClientSize.Height / 2 - e.Y;
                picBox.Location = new Point(posx, posy);
            }
            if (tsbtnPan.Checked)
            {
                int posx = panel1.ClientSize.Width / 2 - e.X;
                int posy = panel1.ClientSize.Height / 2 - e.Y;
                picBox.Location = new Point(posx, posy);
            }
            if (tsbtnHand.Checked)
            {
                endPos = e.Location;
                //if (endPos.X >= picBox.Width) endPos.X = picBox.Width;
                int posx = picBox.Location.X + (endPos.X - startPos.X);
                int posy = picBox.Location.Y + (endPos.Y - startPos.Y);
                picBox.Location = new Point(posx, posy);
            }

        }

        private void tstbZoomRate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Return) diff = picBox.Width / int.Parse(tstbZoomRate.Text);
        }

        private void tsbtnPan_Click(object sender, EventArgs e)
        {
            tsbtnPan.Checked = true;
            tsbtnZoomIn.Checked = false;
            tsbtnHand.Checked = false;
            picBox.Cursor = new Cursor("cross.ico");
        }

        private void tsbtnHand_Click(object sender, EventArgs e)
        {
            picBox.Cursor = new Cursor("hand.ico");
            tsbtnHand.Checked = true;
            tsbtnZoomIn.Checked = false;
            tsbtnPan.Checked = false;
        }

        private void picBox_MouseDown(object sender, MouseEventArgs e)
        {
            startPos = new Point(e.Location.X, e.Location.Y);
        }

        private void tsbtnChangeColor_Click(object sender, EventArgs e)
        {
            if (!push)
            for (int i=0; i<picBox.Image.Height; i++) 
            {
                for (int j=0; j<picBox.Image.Width; j++)
                {
                    if (bmpWorkerPath.GetPixel(j, i).G > 0) bmpWorkerPath.SetPixel(j, i, Color.LightGreen);
                }
                    tsbtnChangeColor.ToolTipText = "Change to Gradient color";
            }
            else
            {
                bmpWorkerPath = new Bitmap( lastBmpWorkerPath);
                tsbtnChangeColor.ToolTipText = "Change to Green";
            }
            picBox.Image = mergeImages(bmpLab, bmpWorkerPath);
            push = !push;
        }

        private void tsbttnCapture_Click(object sender, EventArgs e)
        {
            string dirname = appfolder + "Figures\\";
            Directory.CreateDirectory(dirname);
            string[] files = Directory.GetFiles(dirname);
            List<int> file = new List<int>();
            foreach (string fname in files)
            {
                file.Add(int.Parse(Path.GetFileNameWithoutExtension(fname)));
            }
            file.Sort();
            string fn;
            //if (file.Count == 0) fn = dirname + "1.png";
           /* else*/ fn = dirname + (file.Count + 1) + ".png";
            Image img = captureForm(this, setupRectangle(this));
            img.Save(fn);
            MessageBox.Show("Image has captured");
        }

        Rectangle setupRectangle(Control cont)
        {
            int locx, locy;
            locx = (cont.Location.X + (cont.Size.Width - cont.ClientSize.Width)/2);
            locy = (cont.Location.Y + (cont.Size.Height - cont.ClientSize.Height) / 2) + (int)(1.5F *(Single)(toolStrip1.Height));
            int sizex, sizey;
            sizex = cont.Size.Width - 1 * (cont.Size.Width - cont.ClientSize.Width);
            sizey = cont.Size.Height- toolStrip1.Height - (int)((Single)(cont.Size.Height - cont.ClientSize.Height) );
            Rectangle rec = new Rectangle(new Point(locx, locy), new Size(sizex, sizey));
            return rec;
        }

        Bitmap captureForm(Control wnd, Rectangle rc)
        {
            Bitmap bitmap = null;
            using (Graphics graphics = wnd.CreateGraphics())
            {
                bitmap = new Bitmap(rc.Width, rc.Height, graphics);
                using (Graphics graphics2 = Graphics.FromImage(bitmap))
                {
                    graphics2.CopyFromScreen(rc.X, rc.Y, 0, 0, rc.Size, CopyPixelOperation.SourceCopy);
                }
            }
            return bitmap;
        }
    }
}
