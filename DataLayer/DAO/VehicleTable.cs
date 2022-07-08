using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Text;
using DTO.DTO;
namespace DataLayer.DAO
{
    public class VehicleTable
    {
        public static String SQL_SELECT = "SELECT * FROM \"Vehicle\"";
        public static String SQL_SELECT_ID = "SELECT * FROM \"Vehicle\" WHERE vID = @id";
        public static String SQL_INSERT = "INSERT INTO \"Vehicle\" VALUES(@ID,@catacity,@name,@type,@condition,@license_plate,@year_of_manufacture)";
        public static String SQL_UPDATE = "UPDATE \"Vehicle\" SET catacity = @catacity, name = @name, type = @type, condition = @condition, license_plate = @license_plate, year_of_manufacture = @year_of_manufacture WHERE vID = @vID";
        public static String SQL_DELETE_ID = "DELETE FROM \"Vehicle\" WHERE vID = @id";
        public static String SQL_Vehicle_check = "Vehicle_chck";
        public static String SQL_DEL = "DelV";
        /*
         * f_4.1.2.1
        */
        public static void LogV(string title, string action)
        {
            JsonData.LogIt(title, action);
        }
        public static int Insert(Vehicle vehicle, Database pDB = null)
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
            PrepareCommand(command, vehicle);
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
        public static int Update(Vehicle veh, int id, Database pDB = null)
        {
            Database db;
            if (pDB == null)
            {
                db = new Database();
                db.Connect();
            }
            else
                db = (Database)pDB;
            SqlCommand command = db.CreateCommand(SQL_UPDATE);
            command.Parameters.AddWithValue("@vID", id);
            PrepareCommand(command, veh);
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
            command.Parameters.AddWithValue("@vID", id);
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
        public static Collection<Vehicle> Select(Database pDB = null)
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

            Collection<Vehicle> vehicles = Read(reader);
            reader.Close();
            if(pDB == null)
            {
                db.Close();
            }
            return vehicles;
        }
        public static Vehicle SelectID(int id, Database pDB = null)
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
            Vehicle vehicle = null;
            Collection<Vehicle> vehicles= Read(reader);
            reader.Close();
            if (vehicles.Count == 1)
            {
                vehicle= vehicles[0];
            }
            if (pDB == null)
            {
                db.Close();
            }
            return vehicle;
        }
        /*
         * 4.1.2.5 
        */
        private static Collection<Vehicle> Read(SqlDataReader reader)
        {
            Collection<Vehicle> vehicles = new Collection<Vehicle>();
            while (reader.Read())
            {
                int i = -1;
                Vehicle vehicle = new Vehicle();
                vehicle.Id = reader.GetInt32(++i);
                vehicle.Capacity = reader.GetInt32(++i);
                vehicle.Name = reader.GetString(++i);
                vehicle.Type = reader.GetString(++i);
                vehicle.Condition = reader.GetString(++i);
                vehicle.License_plate = reader.GetString(++i);
                vehicle.Year_of_manufacture = reader.GetInt32(++i);
                vehicles.Add(vehicle);
            }
            return vehicles;
        }
        /*
         * 4.1.2.5
        */
        public static String VehicleCheck(Database pDB = null)
        {
            Database db;
            if(pDB == null)
            {
                db = new Database();
                db.Connect();
            }
            else
            {
                db = (Database)pDB;
            }
          
            SqlCommand command = db.CreateCommand(SQL_Vehicle_check);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            var che = command.Parameters.Add("@return",System.Data.SqlDbType.VarChar,50);
            che.Direction = System.Data.ParameterDirection.Output;
            command.ExecuteNonQuery();
            String output = che.Value.ToString();
            if(pDB == null)
            { 
                db.Close(); 
            }
            return output;
        }
        private static void PrepareCommand(SqlCommand command, Vehicle vehicle)
        {
            command.Parameters.AddWithValue("@ID", vehicle.Id);
            command.Parameters.AddWithValue("@catacity", vehicle.Capacity);
            command.Parameters.AddWithValue("@name", vehicle.Name);
            command.Parameters.AddWithValue("@type", vehicle.Type);
            command.Parameters.AddWithValue("@condition", vehicle.Condition);
            command.Parameters.AddWithValue("@license_plate", vehicle.License_plate);
            command.Parameters.AddWithValue("@year_of_manufacture", vehicle.Year_of_manufacture);
            
        }
    }
}
