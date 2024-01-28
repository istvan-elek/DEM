using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using System.Data;
using System.Windows.Forms;

namespace DCMaster
{
    class dbTools  // dbTools surves data functionalty, save workers, labirynths, worker paths and sets a live status false, if a worker stepped to wumpus or Trap
    {
        NpgsqlConnectionStringBuilder cnsb = new NpgsqlConnectionStringBuilder();
        worker wkr;
        labyrinth lab = new labyrinth();

        //public dbTools(NpgsqlConnectionStringBuilder connsb, worker wk, labirynth lb)
        //{
        //    cnsb = connsb;
        //    wkr = wk;
        //    lab = lb;
        //}

        public dbTools(NpgsqlConnectionStringBuilder connsb, worker wk)
        {
            cnsb = connsb;
            wkr = wk;
        }

        //public dbTools(NpgsqlConnectionStringBuilder connsb, labirynth lb)
        //{
        //    cnsb = connsb;
        //    lab = lb;
        //}

        public dbTools(NpgsqlConnectionStringBuilder connsb)
        {
            cnsb = connsb;
        }


        public DataTable getLabirynthData()
        {
            DataTable dt = new DataTable();
            using (NpgsqlConnection cnn = new NpgsqlConnection(cnsb.ConnectionString))
            {
                cnn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    cmd.Connection = cnn;
                    cmd.CommandText = "SELECT * FROM lab ORDER BY lab_id";
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                }
            }
            return dt;
        }


        //public DataTable getLabirynthIDs()
        //{
        //    DataTable dt = new DataTable();
        //    using (NpgsqlConnection cnn = new NpgsqlConnection(cnsb.ConnectionString))
        //    {
        //        cnn.Open();
        //        using (NpgsqlCommand cmd = new NpgsqlCommand())
        //        {
        //            cmd.Connection = cnn;
        //            cmd.CommandText = "SELECT lab_id FROM lab";
        //            NpgsqlDataReader dr = cmd.ExecuteReader();
        //            dt.Load(dr);
        //        }
        //    }
        //    return dt;
        //}


        public DataTable getParametersTable(NpgsqlConnectionStringBuilder connsb)
        {
            DataTable dt = new DataTable();
            using (NpgsqlConnection cnn = new NpgsqlConnection(connsb.ConnectionString))
            {
                cnn.Open();
                string selectCommand = "select * from parameters order by parameter_id";

                using (NpgsqlDataAdapter da = new NpgsqlDataAdapter(selectCommand, cnn))
                {
                    da.Fill(dt);
                }
            }
            return dt;
        }



        public DataTable loadTableData(string sqlCommand, string dbName)
        {
            DataTable dt = new DataTable();
            NpgsqlConnectionStringBuilder cnsb2 = new NpgsqlConnectionStringBuilder();

            cnsb2 = cnsb;
            cnsb2.Database = dbName;
            using (NpgsqlConnection cnn = new NpgsqlConnection(cnsb2.ConnectionString))
            {
                cnn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(sqlCommand, cnn))
                {
                    NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            return dt;
        }


        public void saveLabTableData(DataTable dt)
        {
            using (NpgsqlConnection cnn = new NpgsqlConnection(cnsb.ConnectionString))
            {
                cnn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    cmd.Connection = cnn;
                    string tableName = "lab";
                    foreach (DataRow row in dt.Rows)
                    {
                        string fields = "lab_size,number_of_wumpus,wumpus_position,Trap_position,gold_position";
                        string values = row["lab_Size"] + "," + row["number_of_wumpus"] + ",'" + row["wumpus_position"] + "','" + row["Trap_position"] + "','" + row["gold_position"] + "'";
                        string sqlCommand = "INSERT INTO " + tableName + " (" + fields + ") VALUES(" + values + ")";
                        cmd.CommandText = sqlCommand;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }


        //public void saveIterationData(/*int mission_number,*/ Int32 num_of_alive_workers, Int32 gathered_energy)
        //{
        //    using (NpgsqlConnection cnn = new NpgsqlConnection(cnsb.ConnectionString))
        //    {
        //        cnn.Open();
        //        string sqlCommand = "INSERT INTO iteration (num_of_alive_workers, gathered_energy) VALUES(" + num_of_alive_workers + "," + gathered_energy  + ")";
        //        using (NpgsqlCommand cmd = new NpgsqlCommand(sqlCommand, cnn))
        //        {
        //            cmd.ExecuteScalar();
        //        }
        //    }
        //}


        //public void saveWkKnowledge( worker wk, int lab_id) //Int32 worker_id, string loc_wumpus, string loc_trap, string loc_gold)
        //{
        //    string loc_wumpus=wk.Knowledge_wumpus_fields;
        //    string loc_trap=wk.Knowledge_trap_fields; 
        //    string loc_gold=wk.Knowledge_gold_fields;
        //    using (NpgsqlConnection cnn = new NpgsqlConnection(cnsb.ConnectionString))
        //    {
        //        cnn.Open();
        //        using (NpgsqlCommand cmd = new NpgsqlCommand())
        //        {
        //            cmd.Connection=cnn;
        //            string sqlCommand = "";
        //            //if (missionNumber != 1)
        //            //{
        //                //sqlCommand = "INSERT INTO knowledge (worker_id, lab_id, loc_wumpus, loc_trap, loc_gold) VALUES(" + wk.ID + "," + lab_id + ",'" + loc_wumpus + "','" + loc_trap + "','" + loc_gold + "')";
        //            //}
        //            //else
        //            //{
        //            //    sqlCommand = "UPDATE knowledge SET (worker_id, lab_id, loc_wumpus, loc_trap, loc_gold) = (" + wk.ID + "," + lab_id + ",'" + loc_wumpus + "','" + loc_trap + "','" + loc_gold + "') WHERE worker_id=" + wk.ID + " AND lab_id=" + lab_id;
        //            //}
        //            cmd.CommandText = sqlCommand;
        //            cmd.ExecuteNonQuery();
        //        }
        //    }
        //}

        //public void deleteDatabase(string dbName)   // delete content of the given tableName
        //{
        //    //string sqlcmd = "REVOKE CONNECT ON DATABASE " + dbName + " FROM PUBLIC, " + cnsb.UserName + "; SELECT pg_terminate_backend(pg_stat_activity.pid) FROM pg_stat_activity WHERE pg_stat_activity.datname = '" + dbName + "' AND pid<> pg_backend_pid();";
        //    string dropcmd = " DROP DATABASE " + dbName;

        //    using (NpgsqlConnection cnn = new NpgsqlConnection(cnsb.ConnectionString))
        //    {
        //        cnn.Open();
        //        //using (NpgsqlCommand cmd1=new NpgsqlCommand(sqlcmd,cnn))
        //        //{
        //        //    cmd1.ExecuteNonQuery();

        //        //}
        //        using (NpgsqlCommand cmd = new NpgsqlCommand(dropcmd, cnn)) 
        //        {
        //            try
        //            {
        //                cmd.ExecuteNonQuery();
        //                MessageBox.Show("Deletion succeeded","Deletion succeeded", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            }
        //            catch (NpgsqlException e)
        //            {
        //                MessageBox.Show("Database deletion failed. " + e.Message + ". Close the program and start again.", "Failed DROP DATABASE", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            }

        //        }
        //    }
        //}


    }
}
