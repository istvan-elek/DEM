using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using System.Data;

namespace Wump
{
    class dbTools  // dbTools surves data functionalty, save workers, labirynths, worker paths and sets a live status false, if a worker stepped to wumpus or Trap
    {
        NpgsqlConnectionStringBuilder cnsb = new NpgsqlConnectionStringBuilder();
        worker wkr;
        labirynth lab = new labirynth();

        public dbTools(NpgsqlConnectionStringBuilder connsb, worker wk, labirynth lb)
        {
            cnsb = connsb;
            wkr = wk;
            lab = lb;
        }

        public dbTools(NpgsqlConnectionStringBuilder connsb, worker wk)
        {
            cnsb = connsb;
            wkr = wk;
        }

        public dbTools(NpgsqlConnectionStringBuilder connsb, labirynth lb)
        {
            cnsb = connsb;
            lab = lb;
        }

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

        public DataTable getLabirynthIDs()
        {
            DataTable dt = new DataTable();
            using (NpgsqlConnection cnn = new NpgsqlConnection(cnsb.ConnectionString))
            {
                cnn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    cmd.Connection = cnn;
                    cmd.CommandText = "SELECT lab_id FROM lab";
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                }
            }
            return dt;
        }

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

        public void AddNewWorker(Int32 parent, Int32 energy)
        {
            using (NpgsqlConnection cnn = new NpgsqlConnection(cnsb.ConnectionString))
            {
                cnn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    cmd.Connection = cnn;
                    //save worker, which comes from its parent
                    string tableName = "workers";
                    string field = " (live,  parent, energy) ";
                    Boolean value_live = true;
                    Int32 value_parent = parent;
                    Int32 value_energy = energy;                    
                    string sqlCommand = "INSERT INTO " + tableName + field + " VALUES(" + value_live + "," + value_parent + "," + value_energy + ") RETURNING worker_id";
                    cmd.CommandText = sqlCommand;
                    Int32 wkid = Convert.ToInt32(cmd.ExecuteScalar());
                    //add its parent's knowledge to the knowledge table
                    field = " (worker_id, lab_id, worker_path, value) ";
                    DataTable dtab = loadTableData("SELECT worker_id, lab_id, worker_path, value FROM knowledge WHERE worker_id = " + parent + "AND value <> -1");
                    string eachfield = "";
                    foreach(DataRow row in dtab.Rows)
                    {
                        eachfield = wkid + "," + row["lab_id"].ToString() + ",'" + row["worker_path"].ToString() + "'," + row["value"].ToString();
                        sqlCommand = "INSERT INTO knowledge " + field + " VALUES(" + eachfield + ")";
                        cmd.CommandText = sqlCommand;
                        cmd.ExecuteNonQuery();
                    }
                    
                }
            }
        }

        public Int32 saveWorker()  //saves workers to 'workers' table
        {
            using (NpgsqlConnection cnn=new NpgsqlConnection(cnsb.ConnectionString))
            {
                cnn.Open();
                using (NpgsqlCommand cmd=new NpgsqlCommand())
                {
                    cmd.Connection = cnn;
                    string tableName = "workers";
                    string field = " live,  parent, energy ";
                    Boolean value_live = wkr.Live;
                    Int32 value_parent = wkr.Parent;
                    Int32 value_energy = wkr.Energy;
                    string sqlCommand = "INSERT INTO " + tableName + " (" + field + ") VALUES(" + value_live + "," + value_parent + "," + value_energy +") RETURNING worker_id";
                    cmd.CommandText = sqlCommand;
                    wkr.ID = Convert.ToInt16(cmd.ExecuteScalar());
                    return wkr.ID;  //returns the primary key of the last saved record 
                }
            }
        }


        public void setupWorkersLive(Int32 id)   //sets a live status false, if a worker stepped to wumpus or Trap
        {
            using (NpgsqlConnection cnn = new NpgsqlConnection(cnsb.ConnectionString))
            {
                cnn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    cmd.Connection = cnn;
                    cmd.CommandText = "UPDATE workers SET live=false WHERE worker_id=" + id;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void update_worker_energy(Int32 energyValue, Int32 id)
        {
            using (NpgsqlConnection cnn = new NpgsqlConnection(cnsb.ConnectionString))
            {
                cnn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    cmd.Connection = cnn;
                    cmd.CommandText = "UPDATE workers SET energy=" + energyValue + " WHERE worker_id=" + id;
                    cmd.ExecuteNonQuery();
                }
            }
        }



        public int saveLabirynth()   // //saves labirynth into 'lab' table. Return value is the lab_id
        {
            using (NpgsqlConnection cnn = new NpgsqlConnection(cnsb.ConnectionString))
            {
                cnn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    cmd.Connection = cnn;
                    string tableName = "lab";
                    string fields = "lab_size,number_of_wumpus,wumpus_position,Trap_position,gold_position";
                    string values = lab.Size + "," + lab.numOfWumpusTrapsGold + ",'" + lab.WumpusPosition + "','" + lab.TrapPosition + "','" + lab.GoldPosition + "'";
                    string sqlCommand = "INSERT INTO " + tableName + " (" + fields + ") VALUES(" + values + ") RETURNING lab_id";
                    cmd.CommandText = sqlCommand;
                    int id=Convert.ToInt16(cmd.ExecuteScalar());
                    return id;    //returns the primary key of the last saved record                
                }
            }
        }

        public void deleteTable(string tableName)   // delete content of the given tableName
        {
            using (NpgsqlConnection cnn = new NpgsqlConnection(cnsb.ConnectionString))
            {
                cnn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand("DELETE FROM " + tableName, cnn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void resetAllSequences()
        {
            using (NpgsqlConnection cnn = new NpgsqlConnection(cnsb.ConnectionString))
            {
                cnn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    cmd.Connection = cnn;
                    cmd.CommandText = "ALTER SEQUENCE labid_seq  RESTART WITH 1;ALTER SEQUENCE workerpath_seq  RESTART WITH 1;ALTER SEQUENCE workers_worker_id_seq  RESTART WITH 1;ALTER SEQUENCE logbook_id_seq  RESTART WITH 1;ALTER SEQUENCE knowledge_row_id_seq  RESTART WITH 1;ALTER SEQUENCE mission_id_seq  RESTART WITH 1";
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void resetSequence(string tableName)
        {
            using (NpgsqlConnection cnn = new NpgsqlConnection(cnsb.ConnectionString))
            {
                cnn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    string seqName="";
                    cmd.Connection = cnn;
                    switch (tableName)
                    {                             
                        case "workers":
                            seqName = "workers_worker_id_seq";
                            break;
                        case "lab":
                            seqName ="labid_seq";
                            break;
                        case "worker_path":
                            seqName = "workerpath_seq";
                            break;
                        case "logbook":
                            seqName = "logbook_id_seq";
                            break;
                        case "mission":
                            seqName = "mission_id_seq";
                            break;
                        case "knowledge":
                            seqName = "knowledge_row_id_seq";
                            break; 
                    }
                    cmd.CommandText = "ALTER SEQUENCE " + seqName + " RESTART WITH 1";
                    cmd.ExecuteNonQuery();
                }
            }
        }


        public void deleteDeadWorkers()
        {
            using (NpgsqlConnection cnn = new NpgsqlConnection(cnsb.ConnectionString))
            {
                cnn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    cmd.Connection = cnn;
                    cmd.CommandText = "DELETE FROM worker_path WHERE worker_id IN (SELECT worker_id FROM workers WHERE live=false)"; // delete dead workers' path
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "DELETE FROM knowledge WHERE worker_id IN (SELECT worker_id FROM workers WHERE live=false)"; //delete dead workers' knowledge
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "DELETE FROM workers WHERE live=false"; // delete dead workers
                    cmd.ExecuteNonQuery();


                }
            }
        }


        public DataTable loadTableData(string sqlCommand)
        {
            DataTable dt = new DataTable();
            using (NpgsqlConnection cnn = new NpgsqlConnection(cnsb.ConnectionString))
            {
                cnn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(sqlCommand, cnn))
                {
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                    //NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
                    //da.Fill(dt);
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

        public void saveTableData(string sourceDB, string destinationDB)
        {
            using (NpgsqlConnection cnn = new NpgsqlConnection(cnsb.ConnectionString))
            {
                cnn.Open();
                string sqlCommand= "SELECT * INTO " + destinationDB + ".lab FROM " + sourceDB + ".lab";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sqlCommand, cnn))
                {
                    cmd.ExecuteScalar();
                }
            }
        }


        public void saveTableData(DataTable dt)
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

        public void saveMissionData(int mission_number, Int32 num_of_alive_workers, Int32 gathered_energy, Single population_rate, Single energy_rate)
        {
            using (NpgsqlConnection cnn = new NpgsqlConnection(cnsb.ConnectionString))
            {
                cnn.Open();
                string sqlCommand = "INSERT INTO mission (num_of_alive_workers,gathered_energy, population_rate, energy_rate) VALUES(" + num_of_alive_workers + "," + gathered_energy + "," + population_rate + "," + energy_rate + ")";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sqlCommand, cnn))
                {
                    cmd.ExecuteScalar();
                }
            }
        }

        public void saveKnowledge(Int32 worker_id, Int32 lab_id)
        {
                DataTable dtab = loadTableData("SELECT worker_id, lab_id, worker_path, value FROM worker_path WHERE (worker_id = " + worker_id + " AND lab_id=" + lab_id + " AND value <> -1) GROUP BY worker_path, worker_id, lab_id, value ORDER BY worker_id, lab_id");
                string fields = "";
                string field = "";
                foreach (DataColumn Col in dtab.Columns)
                {
                    field += Col.ColumnName +",";
                }
                fields = field.Substring(0,field.Length - 1);
                foreach (DataRow row in dtab.Rows)
                {
                    string values = "";
                    values += row[0].ToString() + "," + row[1].ToString() + ",'" + row[2].ToString() + "'," + row[3].ToString();
                    using (NpgsqlConnection cnn = new NpgsqlConnection(cnsb.ConnectionString))
                    {
                        cnn.Open();
                        string sqlCommand = "INSERT INTO knowledge (" + fields + ") VALUES(" + values + ")";
                        using (NpgsqlCommand cmd = new NpgsqlCommand(sqlCommand, cnn))
                        {
                            cmd.ExecuteScalar();
                        }
                    }
                }
         }


    }
}
