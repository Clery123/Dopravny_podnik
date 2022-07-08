using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Text;
using DTO.DTO;
namespace DataLayer.DAO
{
    public class PathTable
    {
        public static String SQL_SELECT = "SELECT * FROM \"Path\"";
        public static String SQL_SELECT_ID = "SELECT * FROM \"Path\" WHERE pID = @id";
        public static String SQL_INSERT = "INSERT INTO \"Path\" VALUES(@id,@from,@to,@number_of_stops,@distance)";
        public static String SQL_UPDATE = "UPDATE \"Path\" SET from = @from, to= @to, number_of_stops= @number_of_stops, distance = @distance WHERE vID = @id";
        public static String SQL_DELETE_ID = "DELETE FROM \"Path\" WHERE pID = @id";
        
        /*
         * f_4.1.2.1
        */
        public static int Insert(Path path, Database pDB = null)
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
            PrepareCommand(command, path);
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
        public static int Update(Path path, Database pDB = null)
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
            PrepareCommand(command, path);
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
        public static Collection<Path> Select(Database pDB = null)
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

            Collection<Path> paths = Read(reader);
            reader.Close();
            if (pDB == null)
            {
                db.Close();
            }
            return paths;
        }
        public static Path SelectID(int id, Database pDB = null)
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
            Path path = null;
            Collection<Path> paths= Read(reader);
            reader.Close();
            if (paths.Count == 1)
            {
                path = paths[0];
            }
            if (pDB == null)
            {
                db.Close();
            }
            return path;
        }
        /*
         * 4.1.2.5 
        */
        private static Collection<Path> Read(SqlDataReader reader)
        {
            Collection<Path> paths = new Collection<Path>();
            while (reader.Read())
            {
                int i = -1;
                Path path = new Path();
                path.pID = reader.GetInt32(++i);
                path.From= reader.GetString(++i);
                path.To = reader.GetString(++i);
                path.Number_of_stops = reader.GetInt32(++i);
                path.Distance = reader.GetDouble(++i);
                paths.Add(path);
            }
            return paths;
        }
        /*
         * 4.1.2.5
        */
       
        private static void PrepareCommand(SqlCommand command, Path path)
        {
            command.Parameters.AddWithValue("@id", path.pID);
            command.Parameters.AddWithValue("@from", path.From);
            command.Parameters.AddWithValue("@to", path.To);
            command.Parameters.AddWithValue("@number_of_stops", path.Number_of_stops);
            //command.Parameters.AddWithValue("@telephone", User.Telephone == null ? DBNull.Value : (object)User.Telephone);
            command.Parameters.AddWithValue("@distance", path.Distance);
        }
    }
}
