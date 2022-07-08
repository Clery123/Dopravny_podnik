using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DataLayer
{
    public class Database
    {
        /// <summary>
        /// Represents a MS SQL Database
        /// </summary
        private SqlConnection Connection { get; set; }
        private SqlTransaction SqlTransaction { get; set; }
        public string Language { get; set; }
        public Database()
        {
            Connection = new SqlConnection();
            Language = "en";
        }
        /// <summary>
        /// Connect
        /// </summary>
        public bool Connect(string conString)
        {
            if (Connection.State != System.Data.ConnectionState.Open)
            {
                Connection.ConnectionString = conString;
                Connection.Open();
            }
            return true;
        }
        public bool Connect()
        {
            bool ret = true;
            if (Connection.State != System.Data.ConnectionState.Open)
            {
                Console.WriteLine("aaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
                // connection string is stored in file App.config or Web.config
                string connectionString = ConfigurationManager.ConnectionStrings["ConnectionStringMsSql"].ConnectionString;
                ret = Connect(ConfigurationManager.ConnectionStrings["ConnectionStringMsSql"].ConnectionString);
            }
            Console.WriteLine(".");
            return ret;
        }
        /// <summary>
        /// Close
        /// </summary>
        public void Close()
        {
            Connection.Close();
        }

        /// <summary>
        /// Begin a transaction.
        /// </summary>
        public void BeginTransaction()
        {
            SqlTransaction = Connection.BeginTransaction(IsolationLevel.Serializable);
        }

        /// <summary>
        /// End a transaction.
        /// </summary>
        public void EndTransaction()
        {
            SqlTransaction.Commit();
            Close();
        }

        /// <summary>
        /// If a transaction is failed call it.
        /// </summary>
        public void Rollback()
        {
            SqlTransaction.Rollback();
        }

        /// <summary>
        /// Insert a record encapulated in the command.
        /// </summary>
        public int ExecuteNonQuery(SqlCommand command)
        {
            int rowNumber = 0;
            // try
            // {
            rowNumber = command.ExecuteNonQuery();
            Console.Write("ccc");
            // }
            //catch (Exception e)
            // {
            Console.Write("---..--");
            //  }
            return rowNumber;
        }

        /// <summary>
        /// Create command
        /// </summary>
        public SqlCommand CreateCommand(string strCommand)
        {
            SqlCommand command = new SqlCommand(strCommand, Connection);
            Console.WriteLine("a");
            if (SqlTransaction != null)
            {
                command.Transaction = SqlTransaction;

            }
            return command;
        }

        /// <summary>
        /// Select encapulated in the command.
        /// </summary>
        public SqlDataReader Select(SqlCommand command)
        {
            SqlDataReader sqlReader = command.ExecuteReader();
            return sqlReader;
        }
    }
}
