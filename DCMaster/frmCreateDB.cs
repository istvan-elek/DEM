using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace DCMaster
{
    public partial class frmCreateDB : Form
    {
        NpgsqlConnectionStringBuilder cnsbAdmin = new NpgsqlConnectionStringBuilder();
        public string dbName;
        int initial_worker_energy; //=100;
        int gold_energy;  // = 1000;
        int trap_energy;  // = -400;
        int wumpus_energy;  // = -1000;
        int movement_costs;  // = -1;
        int replication_energy_level;  // = 5000;
        int replication_rate;  // = 2;

        public frmCreateDB(NpgsqlConnectionStringBuilder cnb)
        {
            InitializeComponent();
            cnsbAdmin = cnb;
            tbDBName.Focus();
            tbDBName.Select();
        }

        void createParametersTable() //creates parameters table, which store set up parameters for adjustable runs
        {
            using (NpgsqlConnection cnn = new NpgsqlConnection(cnsbAdmin.ConnectionString))
            {
                cnn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    cmd.Connection = cnn;

                    cmd.CommandText =
                        "CREATE SEQUENCE public.parameters_id_seq " +
                        "INCREMENT 1 " +
                        "START 0 " +
                        "MINVALUE 0 " +
                        "MAXVALUE 9223372036854775807 " +
                        "CACHE 1; " +
                        "ALTER SEQUENCE public.parameters_id_seq " +
                        "OWNER TO wumpus;" +
                        "CREATE TABLE public.parameters " +
                        "(" +
                        "Name character varying(30) NOT NULL," +
                        "Value integer NOT NULL," +
                        "parameter_id bigint NOT NULL DEFAULT nextval('parameters_id_seq'::regclass)" +
                        ")" +
                        " WITH (" +
                        "OIDS = FALSE" +
                        ")" +
                        " TABLESPACE pg_default;" +
                        " ALTER TABLE public.parameters OWNER to wumpus";
                    cmd.ExecuteNonQuery();
                }
            }
            loadInitParameters();
            initParametersTable();
        }

        void loadInitParameters()
        {
            string fileName = Application.StartupPath + @"\init.par";
            string[] ParameterString = System.IO.File.ReadAllLines(fileName);
            foreach(string line in ParameterString)
            {
                switch (line.Split(';')[0])
                {
                    case "initial_worker_energy":
                        initial_worker_energy = Convert.ToInt16(line.Split(';')[1]);
                        break;
                    case "gold_energy":
                        gold_energy = Convert.ToInt16(line.Split(';')[1]);
                        break;
                    case "trap_energy":
                        trap_energy = Convert.ToInt16(line.Split(';')[1]);
                        break;
                    case "wumpus_energy":
                        wumpus_energy = Convert.ToInt16(line.Split(';')[1]);
                        break;
                    case "movement_costs":
                        movement_costs = Convert.ToInt16(line.Split(';')[1]);
                        break;
                    case "replication_energy_level":
                        replication_energy_level = Convert.ToInt16(line.Split(';')[1]);
                        break;
                    case "replication_rate":
                        replication_rate = Convert.ToInt16(line.Split(';')[1]);
                        break;
                }
            }
        }

        void initParametersTable()
        {
            using (NpgsqlConnection cnn = new NpgsqlConnection(cnsbAdmin.ConnectionString))
            {
                cnn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    cmd.Connection = cnn;
                    cmd.CommandText = "INSERT INTO parameters (Name, Value) VALUES('Initial_worker_energy', " + initial_worker_energy + ")";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "INSERT INTO parameters (Name, Value) VALUES('Gold_energy', " + gold_energy + ")";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "INSERT INTO parameters (Name, Value) VALUES('Movement_costs', " + movement_costs + ")";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "INSERT INTO parameters (Name, Value) VALUES('Trap_energy', " + trap_energy + ")";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "INSERT INTO parameters (Name, Value) VALUES('Wumpus_energy', " + wumpus_energy + ")";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "INSERT INTO parameters (Name, Value) VALUES('Replication_energy_level', " + replication_energy_level + ")";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "INSERT INTO parameters (Name, Value) VALUES('Replication_rate', " + replication_rate + ")";
                    cmd.ExecuteNonQuery();
                }
            }
        }


        void createWorkersTable()  // creates sequence and workers table
        {
            using (NpgsqlConnection cnn = new NpgsqlConnection(cnsbAdmin.ConnectionString))
            {
                cnn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    cmd.Connection = cnn;

                    cmd.CommandText =
                        "CREATE SEQUENCE public.workers_worker_id_seq " +
                        "INCREMENT 1 " +
                        "START 0 " +
                        "MINVALUE 0 " +
                        "MAXVALUE 9223372036854775807 " +
                        "CACHE 1; " +
                        "ALTER SEQUENCE public.workers_worker_id_seq " +
                        "OWNER TO wumpus;" +

                        "CREATE TABLE public.workers " +
                        "(" +
                        "worker_id bigint NOT NULL DEFAULT nextval('workers_worker_id_seq'::regclass)," +
                        "live boolean NOT NULL," +
                        "energy bigint DEFAULT 500," +
                        "location varchar," +
                        "loc_wumpus varchar ," +
                        "loc_trap varchar ," +
                        "loc_gold varchar , " +
                        "parent varchar NOT NULL)" +
                        //"startlocation varchar NOT NULL)" +
                        " WITH (" +
                        "OIDS = FALSE" +
                        ")" +
                        " TABLESPACE pg_default;" +
                        " ALTER TABLE public.workers OWNER to wumpus";
                    cmd.ExecuteNonQuery();
                }
            }
        }



        void createLabTable() // creates sequence and labirynth table
        {
            using (NpgsqlConnection cnn = new NpgsqlConnection(cnsbAdmin.ConnectionString))
            {
                cnn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    cmd.Connection = cnn;
                    cmd.CommandText =
                        "CREATE SEQUENCE public.labid_seq" +
                        " INCREMENT 1" +
                        " START 0" +
                        " MINVALUE 0" +
                        " MAXVALUE 9223372036854775807" +
                        " CACHE 1;" +
                        " ALTER SEQUENCE public.labid_seq OWNER TO wumpus;" +

                        "CREATE TABLE public.lab " +
                        "(" +
                        "lab_id bigint NOT NULL DEFAULT nextval('labid_seq'::regclass)," +
                        " lab_size character varying(20) COLLATE pg_catalog.default NOT NULL," +
                        " number_of_wumpus integer NOT NULL," + 
                        " wumpus_position varchar COLLATE pg_catalog.default NOT NULL," +
                        " Trap_position varchar COLLATE pg_catalog.default NOT NULL," +
                        " gold_position varchar COLLATE pg_catalog.default NOT NULL," +
                        " CONSTRAINT Lab_pkey PRIMARY KEY (lab_id)" +
                        ") " +
                        "WITH (" +
                        "OIDS = FALSE" +
                        ")" +
                        " TABLESPACE pg_default;" +
                        " ALTER TABLE public.lab" +
                        " OWNER to wumpus;";
                    cmd.ExecuteNonQuery();
                }
            }
        }


        void createWorker_PathTable()  // creates sequence and worker_path table
        {
            using (NpgsqlConnection cnn = new NpgsqlConnection(cnsbAdmin.ConnectionString))
            {
                cnn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    cmd.Connection = cnn;
                    cmd.CommandText =
                        "CREATE SEQUENCE public.workerpath_seq " +
                        "INCREMENT 1 " +
                        "START 0 " +
                        "MINVALUE 0 " +
                        "MAXVALUE 9223372036854775807 " +
                        "CACHE 1; " +
                        "ALTER SEQUENCE public.workerpath_seq OWNER TO wumpus;" +

                        "CREATE TABLE public.worker_path " +
                        "(" +
                        "row_id bigint NOT NULL DEFAULT nextval('workerpath_seq'::regclass)," +
                        "worker_path character varying(20) COLLATE pg_catalog.default NOT NULL," +
                        "worker_id bigint NOT NULL," +
                        //"lab_id bigint NOT NULL," +
                        "value integer," +  
                        "CONSTRAINT worker_path_pkey PRIMARY KEY (row_id)" +
                        ") " +
                        "WITH (" +
                        "OIDS = FALSE" +
                        ") " +
                        "TABLESPACE pg_default;" +
                        "ALTER TABLE public.worker_path " +
                        "OWNER to wumpus;";
                    cmd.ExecuteNonQuery();
                }
            }
        }


        void createIteration()
        {
            using (NpgsqlConnection cnn = new NpgsqlConnection(cnsbAdmin.ConnectionString))
            {
                cnn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    cmd.Connection = cnn;
                    cmd.CommandText =
                    "CREATE SEQUENCE public.iteration_id_seq " +
                    "INCREMENT 1 " +
                    "START 0 " +
                    "MINVALUE 0 " +
                    "MAXVALUE 9223372036854775807 " +
                    "CACHE 1; " +
                    "ALTER SEQUENCE public.iteration_id_seq " +
                    "OWNER TO wumpus;" +

                    "CREATE TABLE public.iteration " +
                    "(" +
                    " iteration_id bigint NOT NULL DEFAULT nextval('iteration_id_seq'::regclass)," +
                    " num_of_alive_workers bigint NOT NULL," +
                    " gathered_energy bigint NOT NULL," +
                    " PRIMARY KEY (iteration_id)" +
                    ")" +
                    " WITH (" +
                    " OIDS = FALSE" +
                    ")" +
                    " TABLESPACE pg_default;" +

                    " ALTER TABLE public.iteration " +
                    " OWNER to wumpus;";
                    cmd.ExecuteNonQuery();
                }
            }
        }


        void createKnowledge()
        {
            using (NpgsqlConnection cnn = new NpgsqlConnection(cnsbAdmin.ConnectionString))
            {
                cnn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    cmd.Connection = cnn;
                    cmd.CommandText =
                    "CREATE SEQUENCE public.knowledge_row_id_seq " +
                    "INCREMENT 1 " +
                    "START 0 " +
                    "MINVALUE 0 " +
                    "MAXVALUE 9223372036854775807 " +
                    "CACHE 1; " +
                    "ALTER SEQUENCE public.knowledge_row_id_seq " +
                    "OWNER TO wumpus;" +

                    "CREATE TABLE public.knowledge " +
                    "(" +
                    " row_id bigint NOT NULL DEFAULT nextval('knowledge_row_id_seq'::regclass)," +
                    " worker_id bigint NOT NULL, " +
                    " lab_id integer NOT NULL, " +
                    " loc_wumpus character varying NOT NULL, " +
                    " loc_trap character varying NOT NULL, " +
                    " loc_gold character varying NOT NULL, " +
                    " PRIMARY KEY (row_id) " +
                    ")" +
                    " WITH (" +
                    " OIDS = FALSE " +
                    ") " +
                    " TABLESPACE pg_default; " +

                    " ALTER TABLE public.knowledge " +
                    " OWNER to wumpus;";
                    cmd.ExecuteNonQuery();
                }
            }
        }

        //void createEdge()
        //{
        //    using (NpgsqlConnection cnn = new NpgsqlConnection(cnsbAdmin.ConnectionString))
        //    {
        //        cnn.Open();
        //        using (NpgsqlCommand cmd = new NpgsqlCommand())
        //        {
        //            cmd.Connection = cnn;
        //            cmd.CommandText =
        //            "CREATE SEQUENCE public.edge_row_id_seq " +
        //            "INCREMENT 1 " +
        //            "START 0 " +
        //            "MINVALUE 0 " +
        //            "MAXVALUE 9223372036854775807 " +
        //            "CACHE 1; " +
        //            "ALTER SEQUENCE public.edge_row_id_seq " +
        //            "OWNER TO wumpus;" +

        //            "CREATE TABLE public.edge " +
        //            "(" +
        //            " row_id bigint NOT NULL DEFAULT nextval('edge_row_id_seq'::regclass)," +
        //            " knowledge_row_id bigint," +
        //            " worker_path_row_id bigint," +
        //            " edge_value integer NOT NULL," +
        //            " PRIMARY KEY (row_id) " +
        //            ")" +
        //            " WITH (" +
        //            " OIDS = FALSE " +
        //            ") " +
        //            " TABLESPACE pg_default; " +

        //            " ALTER TABLE public.edge " +
        //            " OWNER to wumpus;";
        //            cmd.ExecuteNonQuery();
        //        }
        //    }
        //}

        void createLogBook()
        {
            using (NpgsqlConnection cnn = new NpgsqlConnection(cnsbAdmin.ConnectionString))
            {
                cnn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    cmd.Connection = cnn;
                    cmd.CommandText =
                    "CREATE SEQUENCE public.logbook_id_seq " +
                    "INCREMENT 1 " +
                    "START 0 " +
                    "MINVALUE 0 " +
                    "MAXVALUE 9223372036854775807 " +
                    "CACHE 1; " +
                    "ALTER SEQUENCE public.workers_worker_id_seq " +
                    "OWNER TO wumpus;" +
                    "CREATE TABLE public.logbook (" +
                    "row_id bigint NOT NULL DEFAULT nextval('logbook_id_seq'::regclass)," +
                    "worker_id bigint NOT NULL," +
                    "start_position character varying(20) COLLATE pg_catalog.default NOT NULL," +
                    "start_energy bigint NOT NULL," +
                    "iteration_id integer NOT NULL," +
                    "CONSTRAINT logbook_pkey PRIMARY KEY (row_id))" +
                    " WITH ( OIDS = FALSE) " +
                    " TABLESPACE pg_default;" +
                    " ALTER TABLE public.logbook OWNER to wumpus;";
                    cmd.ExecuteNonQuery();
                }
            }
        }



        void createWumpusDatabase()   //creates a wumpus database
        {
            using (NpgsqlConnection cnn = new NpgsqlConnection(cnsbAdmin.ConnectionString))
            {
                cnn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    cmd.Connection = cnn;
                    cmd.CommandText = "DROP DATABASE IF EXISTS " + dbName;
                    cmd.ExecuteNonQuery();
                    cmd.CommandText =
                        "CREATE DATABASE " + dbName +// wumpus2 "+
                        " WITH " +
                        "OWNER = wumpus " +
                        "ENCODING = 'UTF8' " +
                        "TABLESPACE = pg_default " +
                        "CONNECTION LIMIT = -1;";
                    cmd.ExecuteNonQuery();
                    cnsbAdmin.Database = dbName;//"wumpus2";
                }
            }
        }



        private void bttnOk_Click(object sender, EventArgs e)
        {
            dbName = tbDBName.Text.ToLower();
            this.Cursor = Cursors.WaitCursor;
            createWumpusDatabase();
            createWorkersTable();
            createLabTable();
            createWorker_PathTable();
            createParametersTable();
            createLogBook();
            //createKnowledge();
            createIteration();
            //createEdge();
            this.Cursor = Cursors.Default;
            MessageBox.Show("'" + dbName + "' database has been created", "Successful creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private void bttnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
