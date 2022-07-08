using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using DTO.DTO;
namespace DataLayer.DAO
{
    public class ControlerTable
    {
        public static String SQL_SELECT = "SELECT * FROM \"Controler\"";
        public static String SQL_SELECT_ID = "SELECT * FROM \"Controler\" WHERE cID = @id";
        public static String SQL_INSERT = "INSERT INTO \"Controler\" VALUES(@id,@eID,@number_of_fines)";
        public static String SQL_UPDATE = "UPDATE \"Controler\" SET number_of_fines= @number_of_fines WHERE cID = @id";
        public static String SQL_DELETE_ID = "DELETE FROM \"Controler\" WHERE cID = @id";
        //public static String SQL_DAILY = "dbo.Daily(@dID)";
        public static int Insert(Controler controler, Database pDB = null)
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
            PrepareCommand(command, controler);
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
        public static int Update(Controler controler, Database pDB = null)
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
            PrepareCommand(command, controler);
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
        
        private static Collection<Vehicle> VRead(SqlDataReader reader)
        {
            Collection<Vehicle> vehicles = new Collection<Vehicle>();
            while (reader.Read())
            {
                int i = -1;
                Vehicle vehicle = new Vehicle();
                vehicle.Id = reader.GetInt32(++i);
                vehicle.Name = reader.GetString(++i);
                vehicle.License_plate = reader.GetString(++i);
                vehicles.Add(vehicle);
            }
            return vehicles;
        }
        public static int Daily(int dID, Database pDB = null)
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
            string query = "SELECT dbo.Daily(@dID)";
            SqlCommand command = db.CreateCommand(query);
            SqlParameter para = new SqlParameter();
            para.ParameterName = "@dID";
            para.SqlDbType = SqlDbType.Int;
            para.Value = dID;
            command.Parameters.Add(para);
            //SqlDataReader dr = command.ExecuteReader();



            /*SqlCommand command = db.CreateCommand(SQL_DAILY);
            command.Parameters.AddWithValue("@dID", dID);
            command.CommandType = System.Data.CommandType.Use;*/
            int ret = (int)command.ExecuteScalar();

            if (pDB == null)
            {
                db.Close();
            }

            return ret;
        }
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
        public static Collection<Controler> Select(Database pDB = null)
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

            Collection<Controler> controlers = Read(reader);
            reader.Close();
            if (pDB == null)
            {
                db.Close();
            }
            return controlers;
        }
        public static Controler SelectID(int id, Database pDB = null)
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
            Controler controler = null;
            Collection<Controler> controlers = Read(reader);
            reader.Close();
            if (controlers.Count == 1)
            {
                controler = controlers[0];
            }
            if (pDB == null)
            {
                db.Close();
            }
            return controler;
        }
        /*
         * 4.1.2.5 
        */
        private static Collection<Controler> Read(SqlDataReader reader)
        {
            Collection<Controler> controlers = new Collection<Controler>();
            while (reader.Read())
            {
                int i = -1;
                Controler controler = new Controler();
                controler.cID = reader.GetInt32(++i);
                controler.eID = reader.GetInt32(++i);
                controler.Number_of_fines= reader.GetInt32(++i);
                controlers.Add(controler);
            }
            return controlers;
        }
        private static void PrepareCommand(SqlCommand command, Controler controler)
        {
            command.Parameters.AddWithValue("@id", controler.cID);
            command.Parameters.AddWithValue("@eID", controler.eID);
            command.Parameters.AddWithValue("@number_of_fines", controler.Number_of_fines);
        }
    }
}
