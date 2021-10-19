using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ChoreApp
{
    class DataAccess
    {

        private string connectionString;


        public DataAccess()
        {
            connectionString = ConnectionString();
        }


        public string ConnectionString()
        {
            // Build connection string
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = "localhost";   // update me
            builder.UserID = "root";              // update me
            builder.Password = "Test1234";      // update me
            builder.InitialCatalog = "master";
            return builder.ConnectionString;
        }

        public void CreateDB()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Create a sample database
                Console.Write("Dropping and creating database 'ChoreDB' ... ");
                String sql = "DROP DATABASE IF EXISTS [ChoreDB]; CREATE DATABASE [ChoreDB]";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {

                    command.ExecuteNonQuery();
                    Console.WriteLine("executed");
                }

                connection.Close();

            }
        }

        public void CreateChoreTable()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                Console.WriteLine("create ChoreTable");
                var sb = new StringBuilder();
                sb.Append("Use ChoreDB;");
                sb.Append("CREATE TABLE Chores ( ");
                sb.Append("     choreId INT IDENTITY(1,1) NOT NULL PRIMARY KEY, ");
                sb.Append("     choreName NVARCHAR(MAX), ");
                sb.Append("     choreDetails NVARCHAR(MAX) ");
                sb.Append("); ");
                string sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {

                    command.ExecuteNonQuery();
                    Console.WriteLine("executed");

                }

                connection.Close();
            }

        }

        public int AddChore(string choreName, string choreDetails)

        {
            var result = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                Console.WriteLine("add Chores");
                var sb = new StringBuilder();
                sb.Append("Use ChoreDB;");
                sb.Append("INSERT Chores (choreName, choreDetails) ");
                sb.Append("VALUES (@choreName, @choreDetails);");
                string sql = sb.ToString();


                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@choreName", choreName);
                    command.Parameters.AddWithValue("@choreDetails", choreDetails);
                    result = command.ExecuteNonQuery();

                }

                connection.Close();

            }
            return result;

        }

        public int UpdateChore(string choreDetails, string choreToUpdate)
        {
            var result = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                Console.WriteLine($"updating chores");

                var sb = new StringBuilder();
                sb.Append("Use ChoreDB;");
                sb.Append("UPDATE Chores SET choreDetails = @choreDetails WHERE choreName = @choreName");
                string sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@choreDetails", choreDetails);
                    command.Parameters.AddWithValue("@choreName", choreToUpdate);
                    result =  command.ExecuteNonQuery();
                }

                connection.Close();

            }
            return result;

        }
        public int DeleteChore(string choreToDelete)
        {
            var result = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                Console.WriteLine($"Delete chores");
                var sb = new StringBuilder();
                sb.Append("Use ChoreDB;");
                sb.Append("DELETE FROM Chores WHERE choreName = @choreName;");
                string sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@choreName", choreToDelete);
                    result =  command.ExecuteNonQuery();
                }

                connection.Close();

            }
            return result;

        }

        public List<string> GetChores()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var sb = new StringBuilder();
                sb.Append("Use ChoreDB;");
                sb.Append("SELECT choreId,choreName,choreDetails FROM Chores;");

                string sql = sb.ToString();
                var choreToGet = new List<string>();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            choreToGet.Add($"{reader.GetInt32(0)} {reader.GetString(1)} {reader.GetString(2)}");
                        }
                    }
                }

                connection.Close();

                return choreToGet;
            }
        }

    }
 
}
