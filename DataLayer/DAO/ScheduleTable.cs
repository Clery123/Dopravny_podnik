using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Text;
using DTO.DTO;
namespace DataLayer.DAO
{
    public class ScheduleTable
    {
        public static String SQL_SELECT = "SELECT * FROM \"Schedule\"";
        public static String SQL_SELECT_ID = "SELECT * FROM \"Schedule\" WHERE sID = @id";
        public static String SQL_INSERT = "INSERT INTO \"Schedule\" VALUES(@id,@vID,@dID,@cID,@pID,@date,@number)";
        public static String SQL_UPDATE = "UPDATE \"Schedule\" SET vID = @vID, dID = @dID, cID = @cID, pID = @pID, date = @date, number = @number WHERE sID = @id";
        public static String SQL_DELETE_ID = "DELETE FROM \"Schedule\" WHERE sID = @id";
        public static String SQL_DEL = "DelS";
        public static int Insert(Schedule schedule, Database pDB = null)
        {
            ScheduleTable.LogS("Schedule", "Insert");
            Database db;
            if (pDB == null)
            {
                db = new Database();
                db.Connect();
            }
            else
                db = (Database)pDB;
            SqlCommand command = db.CreateCommand(SQL_INSERT);
            PrepareCommand(command, schedule);
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
        public static void LogS(string title, string action)
        {
            JsonData.LogIt(title, action);
        }
        public static int Update(Schedule schedule, Database pDB = null)
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
            PrepareCommand(command, schedule);
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
            command.Parameters.AddWithValue("@sID", id);
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
        public static Collection<Schedule> Select(Database pDB = null)
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

            Collection<Schedule> schedules = Read(reader);
            reader.Close();
            if (pDB == null)
            {
                db.Close();
            }
            return schedules;
        }
        public static Schedule SelectID(int id, Database pDB = null)
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
            Schedule schedule = null;
            Collection<Schedule> schedules = Read(reader);
            reader.Close();
            if (schedules.Count == 1)
            {
                schedule = schedules[0];
            }
            if (pDB == null)
            {
                db.Close();
            }
            return schedule;
        }
        /*
         * 4.1.2.5 
        */
        private static Collection<Schedule> Read(SqlDataReader reader)
        {
            Collection<Schedule> schedules = new Collection<Schedule>();
            while (reader.Read())
            {
                int i = -1;
                Schedule schedule = new Schedule();
                schedule.sID = reader.GetInt32(++i);
                schedule.vID = reader.GetInt32(++i);
                schedule.dID = reader.GetInt32(++i);
                schedule.cID = reader.GetInt32(++i);
                schedule.pID = reader.GetInt32(++i);
                schedule.Date = reader.GetDateTime(++i);
                schedule.Number = reader.GetInt32(++i);
                schedules.Add(schedule);
            }
            return schedules;
        }
        private static void PrepareCommand(SqlCommand command, Schedule schedule)
        {
            command.Parameters.AddWithValue("@id", schedule.sID);
            command.Parameters.AddWithValue("@vID", schedule.vID);
            command.Parameters.AddWithValue("@dID", schedule.dID);
            command.Parameters.AddWithValue("@cID", schedule.cID);
            //command.Parameters.AddWithValue("@telephone", User.Telephone == null ? DBNull.Value : (object)User.Telephone);
            command.Parameters.AddWithValue("@pID", schedule.pID);
            command.Parameters.AddWithValue("@date", schedule.Date);
            command.Parameters.AddWithValue("@number", schedule.Number);
        }
    }
}
