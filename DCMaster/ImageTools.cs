using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DCMaster
{
    internal class ImageTools
    {
        //labyrinth lab;
        public ImageTools() { }

        public Image lab2Bmp(labyrinth lab)
        {
            int sz = lab.Size;
            Bitmap lBmp = new System.Drawing.Bitmap(sz, sz, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            Color clrEmpty = Color.Black;
            Color source = Color.OrangeRed;
            Color sink = Color.SteelBlue;

            for (int i = 0; i < sz; i++)
            {
                for (int j = 0; j < sz; j++)
                {
                    lBmp.SetPixel(j, i, clrEmpty);
                }
            }
            for (int i = 0; i < lab.Position.Count; i++)
            {
                int x = int.Parse(lab.Position[i].Split(',')[0]);
                int y = int.Parse(lab.Position[i].Split(',')[1]);
                if (lab.Fields[x, y] < 0) { lBmp.SetPixel(x, y, sink); }
                if (lab.Fields[x, y] > 0) { lBmp.SetPixel(x, y, source); }
            }
            return lBmp;
        }

        public Image Imprint2Image(int labsize, Dictionary<Int32, worker> wk/*, Int32[] wkSequence*/)
        {
            int sz = labsize;
            Bitmap Bmp = new System.Drawing.Bitmap(sz, sz, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            Color clrEmpty = Color.Black;
            Color source = Color.OrangeRed;
            Color sink = Color.SteelBlue;

            for (int i = 0; i < sz; i++)
            {
                for (int j = 0; j < sz; j++)
                {
                    Bmp.SetPixel(j, i, clrEmpty);
                }
            }
            foreach (System.Collections.Generic.KeyValuePair<int, worker> w in wk)
            {
                w.Value.Imprint.ForEach(delegate (string elements)
                {
                    int x = int.Parse(elements.Split(',')[0]);
                    int y = int.Parse(elements.Split(',')[1]);
                    int val = int.Parse(elements.Split(',')[2]);
                    if (val <0 ) { Bmp.SetPixel(x, y, sink); }
                    else { Bmp.SetPixel(x,y, source); }
                    //imageMatrix[x, y] += 1;

                });
            }
            return Bmp;
        }

        public Image workerPath2Image(int LabSize, Dictionary<Int32, worker> wk/*, Int32[] wkSequence*/)  // display all workers's worker_path 
        {
            int size = LabSize;
            Bitmap img = new Bitmap(size, size);
            int[,] imageMatrix = new int[size, size];

            foreach(System.Collections.Generic.KeyValuePair<int,worker> w in wk)
            {
                w.Value.WorkerPath.ForEach(delegate (string elements)
                {
                    int x = int.Parse(elements.Split(',')[0]);
                    int y = int.Parse(elements.Split(',')[1]);
                    imageMatrix[x, y] += 1;
                });
            }
            int rgb;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    rgb = imageMatrix[i, j];
                    if (rgb > 255) rgb = 255;
                    img.SetPixel(i, j, Color.FromArgb(0, rgb, rgb));
                }
            }
            return img;
        }


        public Image workerPath2Image(int LabSize, worker wk)  // display selected workers's path 
        {
            int size = LabSize;
            Bitmap img = new Bitmap(size, size);
            int[,] imageMatrix = new int[size, size];

            wk.WorkerPath.ForEach(delegate (string elements)
            {
                int x = int.Parse(elements.Split(',')[0]);
                int y = int.Parse(elements.Split(',')[1]);
                imageMatrix[x, y] += 1;
            });

            int rgb;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    rgb = imageMatrix[i, j];
                    if (rgb > 255) rgb = 255;
                    img.SetPixel(i, j, Color.FromArgb(0, rgb, rgb));
                }
            }
            return img;
        }

    }
}
