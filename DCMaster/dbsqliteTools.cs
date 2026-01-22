using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace DCMaster
{
    internal class dbsqliteTools
    {
        SQLiteConnectionStringBuilder cnsb=new SQLiteConnectionStringBuilder();
        public dbsqliteTools(SQLiteConnectionStringBuilder cns) 
        {
            cnsb = cns;
        }

        public void createNewDBFile()
        {
            SQLiteConnection.CreateFile(cnsb.DataSource);
            cnsb.Version = 3;
            using (SQLiteConnection cnn = new SQLiteConnection(cnsb.ConnectionString))
            {
                cnn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    cmd.Connection = cnn;
                    cmd.CommandText = "CREATE DATABASE " + cnsb.DataSource + " Version=3;";
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (SQLiteException e)
                    {
                        if (e.ErrorCode == -2147467259)
                        {
                            MessageBox.Show("'" + cnsb.DataSource + "' is an existing database. Give an other name, or delete the existing one directly in Postgres", "Error in database creation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }
            }
        }

        public void createIterationTable()
        {
            using (SQLiteConnection cnn = new SQLiteConnection(cnsb.ConnectionString))
            {
                cnn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    cmd.Connection = cnn;
                    cmd.CommandText =
                        "CREATE TABLE iteration" +
                        "(" +
                        "stepcount integer," +
                        "numofworkers integer," +
                        "energy integer," +
                        "entropy float" +
                        ")";
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void saveIteration(string[] iter)
        {
            string[] iter2=new string[iter.Length-1];
            for (int i = 1; i < iter.Length; i++)
            {
                iter2[i - 1] = Regex.Replace(iter[i], @"\s+", " ");
            }
            using (SQLiteConnection cnn = new SQLiteConnection(cnsb.ConnectionString))
            {
                cnn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    cmd.Connection = cnn;
                    foreach (string line in iter2)
                    {
                        string[] s = line.Split(' ');
                        int stepcount = int.Parse(s[0]);
                        int numofworkers = int.Parse(s[1]);
                        int energy = int.Parse(s[2]);
                        float entropy = Convert.ToSingle( s[3]);
                        cmd.CommandText = "Insert into iteration (stepcount, numofworkers, energy, entropy) VALUES (?,?,?,?)";
                        cmd.Parameters.Add(new SQLiteParameter("stepcount", stepcount));
                        cmd.Parameters.Add(new SQLiteParameter("numofworkers", numofworkers));
                        cmd.Parameters.Add(new SQLiteParameter("energy", energy));
                        cmd.Parameters.Add(new SQLiteParameter("entropy", entropy));
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }


        public void createWorkersTable()
        {
            using (SQLiteConnection cnn = new SQLiteConnection(cnsb.ConnectionString))
            {
                cnn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    cmd.Connection = cnn;
                    cmd.CommandText =
                        "CREATE TABLE workers" +
                        "(" +
                        "id integer PRIMARY KEY NOT NULL," +
                        "energy integer," +
                        "entropy float," +
                        "learn boolean," +
                        "parents text," +
                        "current_position text," +
                        "start_location text," +
                        "worker_path character varying," +
                        "imprint character varying," +
                        "edge character varying" +
                        ")";
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void saveWorkersTable(Int32[] wkSequence, Dictionary<Int32, worker> wk)
        {
            using (SQLiteConnection cnn = new SQLiteConnection(cnsb.ConnectionString))
            {
                cnn.Open();
                for (int i=0; i< wkSequence.Length; i++)
                {
                    using (SQLiteCommand cmd = new SQLiteCommand())
                    {
                        cmd.Connection = cnn;
                        cmd.CommandText = "Insert into workers (id,energy,entropy,learn,parents,current_position,start_location, worker_path, imprint,edge) VALUES (?,?,?,?,?,?,?,?,?,?)";
                        cmd.Parameters.Add(new SQLiteParameter("id", wk[wkSequence[i]].ID));
                        cmd.Parameters.Add(new SQLiteParameter("energy", wk[wkSequence[i]].Energy));
                        cmd.Parameters.Add(new SQLiteParameter("entropy", wk[wkSequence[i]].SEntropy));
                        cmd.Parameters.Add(new SQLiteParameter("learn", wk[wkSequence[i]].Learn));
                        cmd.Parameters.Add(new SQLiteParameter("parents", wk[wkSequence[i]].Parent));
                        cmd.Parameters.Add(new SQLiteParameter("current_position", wk[wkSequence[i]].CurrentPosition));
                        cmd.Parameters.Add(new SQLiteParameter("start_location", wk[wkSequence[i]].StartLocation));
                                //string wp = String.Join(",", wk[wkSequence[i]].WorkerPath.ToArray());                       
                        cmd.Parameters.Add(new SQLiteParameter("worker_path", getStringfromList(wk[wkSequence[i]].WorkerPath))); 
                        string imp= String.Join(",", wk[wkSequence[i]].Imprint.ToArray());
                        cmd.Parameters.Add(new SQLiteParameter("imprint", getStringfromList(wk[wkSequence[i]].Imprint)));
                        string ed = String.Join(",", wk[wkSequence[i]].Edge.ToArray());
                        cmd.Parameters.Add(new SQLiteParameter("edge", getStringfromList(wk[wkSequence[i]].Edge)));   
                        cmd.ExecuteNonQuery();
                    }
                }               
            }
        }

        string getStringfromList(List<string> lst)
        {
            string strOut = "";
            foreach(string item in lst)
            {
                strOut += item +";";
            }
            return strOut;
        }


        //string replaceSecondCommas(string strIn)
        //{
        //    string strOut="";
        //    string[] s = strIn.Split(',');
        //    for (int k = 0; k < s.Length; k++)
        //    {
        //        if (k % 2 == 0) { strOut += s[k] + ","; }
        //        else { strOut += s[k] + ";"; }
        //    }
        //    return strOut;
        //}


        public void createLabTable()
        {
            using (SQLiteConnection cnn = new SQLiteConnection(cnsb.ConnectionString))
            {
                cnn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    cmd.Connection = cnn;
                    cmd.CommandText =
                        "CREATE TABLE labirynth" +
                        "(" +
                        "id integer PRIMARY KEY AUTOINCREMENT NOT NULL," +
                        "position text," + 
                        "energy int," +
                        "delay int default 0" +
                        ")";
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void saveLabirynthTable(labyrinth lab, int movementCost)
        {
            using (SQLiteConnection cnn = new SQLiteConnection(cnsb.ConnectionString))
            {
                cnn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(cnn))
                {
                    for (int i = 0; i < lab.Position.Count; i++)
                    {
                        string pos = lab.Position[i];
                        cmd.CommandText = "Insert into labirynth (position,energy, delay) VALUES (?,?,?)"; // csak a nevezetes mezőket mentjük el, az üreseket nem
                        cmd.Parameters.Add(new SQLiteParameter("position", pos));
                        cmd.Parameters.Add(new SQLiteParameter("energy", lab.Fields[int.Parse(pos.Split(',')[0]), int.Parse(pos.Split(',')[1])]));
                        cmd.Parameters.Add(new SQLiteParameter("delay", lab.Delay[int.Parse(pos.Split(',')[0]), int.Parse(pos.Split(',')[1])]));
                        cmd.ExecuteNonQuery();
                    }
                    cmd.CommandText = "Insert into labirynth (energy) VALUES (" + lab.Size + ")";
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
