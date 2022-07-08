using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using DTO.DTO;
namespace DataLayer.DAO
{
    public class DriverTable
    {
        public static String SQL_SELECT = "SELECT * FROM \"Driver\"";
        public static String SQL_SELECT_ID = "SELECT * FROM \"Driver\" WHERE dID = @id";
        public static String SQL_INSERT = "INSERT INTO \"Driver\" VALUES(@id,@eID,@hours_worked)";
        public static String SQL_UPDATE = "UPDATE \"Driver\" SET hours_worked= @hours_worked WHERE dID = @id";
        public static String SQL_DELETE_ID = "DELETE FROM \"Driver\" WHERE dID = @id";
        public static String SQL_DAILY = "SELECT dbo.Daily(@dID)";
        public static String SQL_DEL = "Del";
        public static int Insert(Driver driver, Database pDB = null)
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
            PrepareCommand(command, driver);
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
        public static int Update(Driver driver, Database pDB = null)
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
            PrepareCommand(command, driver);
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
        public static Collection<Vehicle> Added_vehicles(int dID, Database pDB = null)
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
            string query = "SELECT DISTINCT Vehicle.vID, name, license_plate FROM \"Vehicle\" JOIN Schedule ON Schedule.vID = Vehicle.vID JOIN Driver ON Driver.dID = Schedule.dID WHERE Driver.dID = @dID";
            SqlCommand command = db.CreateCommand(query);
            SqlParameter para = new SqlParameter();
            para.ParameterName = "@dID";
            para.SqlDbType = SqlDbType.Int;
            para.Value = dID;
            command.Parameters.Add(para);
            SqlDataReader reader = db.Select(command);
            Collection<Vehicle> vehicles = VRead(reader);
            reader.Close();
            if (pDB == null)
                db.Close();
            return vehicles;
        }
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
            SqlCommand command = db.CreateCommand(SQL_DAILY);
            SqlParameter para = new SqlParameter();
            para.ParameterName = "@dID";
            para.SqlDbType = SqlDbType.Int;
            para.Value = dID;
            command.Parameters.Add(para);
            int ret;
            try
            {
               ret = (int)command.ExecuteScalar();
            }
            catch(Exception e)
            {
                ret = 0;
            }
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

        public static String Del(int id, Database pDB = null)
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

            SqlCommand command = db.CreateCommand(SQL_DEL);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            //command.Parameters.Add("@dID", SqlDbType.Int);
            command.Parameters.AddWithValue("@dID", id);
            var che = command.Parameters.Add("@return", System.Data.SqlDbType.VarChar, 50);
            che.Direction = System.Data.ParameterDirection.Output;
            command.ExecuteNonQuery();
            String output = che.Value.ToString();
            if (pDB == null)
            {
                db.Close();
            }
            return output;
        }
        /*
         * f_4.1.2.4
        */
        public static Collection<Driver> Select(Database pDB = null)
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

            Collection<Driver> drivers = Read(reader);
            reader.Close();
            if (pDB == null)
            {
                db.Close();
            }
            return drivers;
        }
        public static Driver SelectID(int id,Database pDB = null)
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
            Driver driver = null;
            Collection<Driver> drivers = Read(reader);
            reader.Close();
            if(drivers.Count == 1)
            {
                driver = drivers[0];
            }
            if (pDB == null)
            {
                db.Close();
            }
            return driver;
        }
        /*
         * 4.1.2.5 
        */
        private static Collection<Driver> Read(SqlDataReader reader)
        {
            Collection<Driver> drivers = new Collection<Driver>();
            while (reader.Read())
            {
                int i = -1;
                Driver driver = new Driver();
                driver.dID = reader.GetInt32(++i);
                driver.eID = reader.GetInt32(++i);
                driver.Hours_worked = reader.GetInt32(++i);
                drivers.Add(driver);
            }
            return drivers;
        }
        private static void PrepareCommand(SqlCommand command, Driver driver)
        {
            command.Parameters.AddWithValue("@id", driver.dID);
            command.Parameters.AddWithValue("@eID", driver.eID);
            command.Parameters.AddWithValue("@hours_worked", driver.Hours_worked);
        }
    }
}
