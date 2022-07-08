using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using DTO.DTO;
namespace DataLayer.DAO
{
    public class DispatcherTable
    {
        public static String SQL_SELECT = "SELECT * FROM \"Dispatcher\"";
        public static String SQL_SELECT_ID = "SELECT * FROM \"Dispatcher\" WHERE disID = @id";
        public static String SQL_INSERT = "INSERT INTO \"Dispatcher\" VALUES(@id,@eID,@hours_worked)";
        public static String SQL_UPDATE = "UPDATE \"Dispatcher\" SET hours_worked= @hours_worked WHERE disID = @id";
        public static String SQL_DELETE_ID = "DELETE FROM \"Dispatcher\" WHERE disID = @id";
       // public static String SQL_DAILY = "dbo.Daily(@dID)";
        public static int Insert(Dispatcher dispatcher, Database pDB = null)
        {
            Database db;
            if (pDB == null)
            {
                db = new Database();
                db.Connect();
            }
            else
                db = (Database)pDB;
            SqlCommand command = db.CreateCommand(SQL_INSERT);
            PrepareCommand(command, dispatcher);
            int ret = db.ExecuteNonQuery(command);
            if (pDB == null)
            {
                db.Close();
            }
            return ret;
        }
        /*
         * f_4.1.2.2 
        */
        public static int Update(Dispatcher dispatcher, Database pDB = null)
        {
            Database db;
            if (pDB == null)
            {
                db = new Database();
                db.Connect();
            }
            else
                db = (Database)pDB;
            SqlCommand command = db.CreateCommand(SQL_INSERT);
            PrepareCommand(command, dispatcher);
            int ret = db.ExecuteNonQuery(command);
            if (pDB == null)
            {
                db.Close();
            }
            return ret;
        }
        /*
         * f_4.1.2.3 
        */
        public static int Delete(int idUser, Database pDB = null)
        {
            Database db;
            if (pDB == null)
            {
                db = new Database();
                db.Connect();
            }
            else
            {
                db = (Database)pDB;
            }
            SqlCommand command = db.CreateCommand(SQL_DELETE_ID);

            command.Parameters.AddWithValue("@id", idUser);
            int ret = db.ExecuteNonQuery(command);

            if (pDB == null)
            {
                db.Close();
            }

            return ret;
        }
        /*
         * f_4.1.2.4
        */
        public static Collection<Dispatcher> Select(Database pDB = null)
        {
            Database db;
            if (pDB == null)
            {
                db = new Database();
                db.Connect();
            }
            else
                db = (Database)pDB;
            SqlCommand command = db.CreateCommand(SQL_SELECT);
            SqlDataReader reader = db.Select(command);

            Collection<Dispatcher> dispatchers = Read(reader);
            reader.Close();
            if (pDB == null)
            {
                db.Close();
            }
            return dispatchers;
        }
        public static Dispatcher SelectID(int id, Database pDB = null)
        {
            Database db;
            if (pDB == null)
            {
                db = new Database();
                db.Connect();
            }
            else
                db = (Database)pDB;
            SqlCommand command = db.CreateCommand(SQL_SELECT_ID);
            command.Parameters.AddWithValue("@id", id);
            SqlDataReader reader = db.Select(command);
            Dispatcher dispatcher= null;
            Collection<Dispatcher> dispatchers = Read(reader);
            reader.Close();
            if (dispatchers.Count == 1)
            {
                dispatcher = dispatchers[0];
            }
            if (pDB == null)
            {
                db.Close();
            }
            return dispatcher;
        }
        /*
         * 4.1.2.5 
        */
        private static Collection<Dispatcher> Read(SqlDataReader reader)
        {
            Collection<Dispatcher> dispatchers = new Collection<Dispatcher>();
            while (reader.Read())
            {
                int i = -1;
                Dispatcher dispatcher = new Dispatcher();
                dispatcher.disID = reader.GetInt32(++i);
                dispatcher.eID = reader.GetInt32(++i);
                dispatcher.Hours_worked = reader.GetInt32(++i);
                dispatchers.Add(dispatcher);
            }
            return dispatchers;
        }
        private static void PrepareCommand(SqlCommand command, Dispatcher dispatcher)
        {
            command.Parameters.AddWithValue("@id", dispatcher.disID);
            command.Parameters.AddWithValue("@eID", dispatcher.eID);
            command.Parameters.AddWithValue("@hours_worked", dispatcher.Hours_worked);
        }
    }
}
