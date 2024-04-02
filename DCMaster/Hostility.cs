using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DCMaster
{
    internal class Hostility
    {
        labyrinth lab;  
        int movement_costs;
        public Hostility(labyrinth labir, int movement_costs)
        {
            this.movement_costs = movement_costs;
            lab = labir;
         }


        public float ComputeStaticHostility()
        {
            float sSource = 0;
            float sSink = 0;
            float sempty = 0;
            int movement_cost = movement_costs;
            for (int i = 0; i < lab.Size; i++)
            {
                for (int j = 0; j < lab.Size; j++)
                {
                    if (lab.Fields[i, j] < movement_cost)  sSink += lab.Fields[i, j];
                    if (lab.Fields[i, j] > 0) sSource += lab.Fields[i, j];
                    if (lab.Fields[i, j] == movement_cost)  sempty += lab.Fields[i, j];
                }
            }
            sempty = sempty - movement_cost * (sSource + sSink);
            float numoffields = lab.Size * lab.Size;
            float sumemptyfield = sempty / numoffields;
            float summSource = sSource;
            float sumSink = sSink;
            float Hst = (sempty + sSink + sSource) / numoffields;
            return Hst;
        }


        public float ComputeDinamicHostility(string startP)
        {
            Point p1 = new Point(int.Parse(startP.Split(',')[0]), int.Parse(startP.Split(',')[1]));
            List<float> distances = new List<float>();
            float hst = 0;
            int n = lab.Size;
            int k = 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                        Point p2 = new Point(i, j);
                        if (p1 != p2)
                        {
                            float dd = Convert.ToSingle(computeDistance(p1, p2));
                            distances.Add(lab.Fields[i, j] / dd);
                            k++;
                        }
                        else distances.Add(0.1F);
                }
            }

            foreach (float item in distances)
            {
                hst += item;
            }
            hst = hst / lab.Size*lab.Size/1000;

            return hst;
        }

        string computeDistance(Point p1, Point p2)
        {
            double dd;
            string dist;
            dd = Math.Sqrt((p1.X - p2.X) ^ 2 + (p1.Y - p2.Y) ^ 2);
            if (dd == 0) dd = 0.1F;
            dist = dd.ToString();
            if (dist == "NaN")
            {
                dist = "1";
            }
            return dist;
        }
    }
}
