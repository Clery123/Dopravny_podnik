using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using DTO.DTO;

namespace DataLayer.DAO
{
    public class EmployeeTable
    {
        public static String SQL_SELECT = "SELECT * FROM \"Employee\"";
        public static String SQL_SELECT_ID = "SELECT * FROM \"Employee\" WHERE eID = @eID";
        public static String SQL_INSERTDriver = "INSERT INTO \"Employee\" (eid, did,first_name,last_name,phone,birth_date,salary,start_date)VALUES(@eID,@dID,@first_name,@last_name,@phone,@birth_date,@salary,@start_date)";
        public static String SQL_INSERTDispatcher = "INSERT INTO \"Employee\" (eid, disID,first_name,last_name,phone,birth_date,salary,start_date)VALUES(@eID,@disID,@first_name,@last_name,@phone,@birth_date,@salary,@start_date)";
        public static String SQL_INSERTControler = "INSERT INTO \"Employee\" (eid, cID,first_name,last_name,phone,birth_date,salary,start_date)VALUES(@eID,@cID,@first_name,@last_name,@phone,@birth_date,@salary,@start_date)";

        public static String SQL_UPDATE = "UPDATE \"Employee\" SET first_name = @first_name, last_name = @last_name, phone = @phone, birth_date = @birth_date, salary = @salary, start_date = @start_date WHERE eID = @id";
        public static String SQL_DELETE_ID = "DELETE FROM \"Employee\" WHERE eID = @id";
        public static String SQL_BONUS = "Bonus";
        //public static String SQL_DAILY = "dbo.Daily(@dID)";
        public static int InsertD(Employee employee, Database pDB = null)
        {
            Database db;
            if (pDB == null)
            {
                db = new Database();
                db.Connect();
            }
            else
                db = (Database)pDB;
            SqlCommand command = db.CreateCommand(SQL_INSERTDriver);
            PrepareCommand(command, employee);
            int ret = db.ExecuteNonQuery(command);
            if (pDB == null)
            {
                db.Close();
            }
            return ret;
        }
        public static void LogE(string title, string action)
        {
            JsonData.LogIt(title, action);
        }
        public static int InsertDis(Employee employee, Database pDB = null)
        {
            Database db;
            if (pDB == null)
            {
                db = new Database();
                db.Connect();
            }
            else
                db = (Database)pDB;
            SqlCommand command = db.CreateCommand(SQL_INSERTDispatcher);
            PrepareCommand(command, employee);
            int ret = db.ExecuteNonQuery(command);
            if (pDB == null)
            {
                db.Close();
            }
            return ret;
        }
        public static int InsertC(Employee employee, Database pDB = null)
        {
            Database db;
            if (pDB == null)
            {
                db = new Database();
                db.Connect();
            }
            else
                db = (Database)pDB;
            SqlCommand command = db.CreateCommand(SQL_INSERTControler);
            PrepareCommand(command, employee);
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
        public static int Update(Employee employee,int id, Database pDB = null)
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
            command.Parameters.AddWithValue("@id", id);
            PrepareCommand(command, employee);
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
        public static void Bonus(Database pDB = null)
        {
            Database db;
            if (pDB == null)
            {
                db = new Database();
                db.Connect();
            }
            else
                db = (Database)pDB;
            SqlCommand command = db.CreateCommand(SQL_BONUS);
            command.ExecuteNonQuery();
            if (pDB == null)
            {
                db.Close();
            }
            
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
        public static Collection<Employee> Select(Database pDB = null)
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

            Collection<Employee> employees = Read(reader);
            reader.Close();
            if (pDB == null)
            {
                db.Close();
            }
            return employees;
        }

        public static Employee SelectID(int id, Database pDB = null)
        {
            Employee emplo = null;
            Database db;
            if (pDB == null)
            {
                db = new Database();
                db.Connect();
            }
            else
                db = (Database)pDB;
            SqlCommand command = db.CreateCommand(SQL_SELECT_ID);
            command.Parameters.AddWithValue("@eID", id);
            SqlDataReader reader = db.Select(command);

            Collection<Employee> emp = Read(reader);
            if(emp.Count == 1)
            {
                emplo = emp[0];
            }
            reader.Close();

            if (pDB == null)
            {
                db.Close();
            }
            return emplo;
        }
        /*
         * 4.1.2.5 
        */
        private static Collection<Employee> Read(SqlDataReader reader)
        {
            Collection<Employee> employees = new Collection<Employee>();
            while (reader.Read())
            {
                int i = -1;
                Employee employee = new Employee();
                employee.eID = reader.GetInt32(++i);
                if (!reader.IsDBNull(i+1))
                    employee.dID = reader.GetInt32(++i);
                else 
                    i++;
                if (!reader.IsDBNull(i+1))
                    employee.disID = reader.GetInt32(++i);
                else
                    i++;
                if (!reader.IsDBNull(i+1))
                    employee.cID = reader.GetInt32(++i);
                else i++;
                employee.First_name = reader.GetString(++i);
                employee.Last_name = reader.GetString(++i);
                employee.Phone= reader.GetString(++i);
                employee.Birth_date = reader.GetDateTime(++i);
                employee.Salary = reader.GetInt32(++i);
                employee.Start_date = reader.GetDateTime(++i);
                employees.Add(employee);
            }
            return employees;
        }
        private static void PrepareCommand(SqlCommand command, Employee employee)
        {
            command.Parameters.AddWithValue("@eID", employee.eID);
            command.Parameters.AddWithValue("@dID", employee.dID);
            command.Parameters.AddWithValue("@disID", employee.disID);
            command.Parameters.AddWithValue("@cID", employee.cID);
            command.Parameters.AddWithValue("@first_name", employee.First_name);
            command.Parameters.AddWithValue("@last_name", employee.Last_name);
            command.Parameters.AddWithValue("@phone", employee.Phone);
            command.Parameters.AddWithValue("@birth_date", employee.Birth_date);
            command.Parameters.AddWithValue("@salary", employee.Salary);
            command.Parameters.AddWithValue("@start_date", employee.Start_date);
        }

        

    }
}
