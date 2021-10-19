using System;
using System.Text;
using System.Data.SqlClient;

namespace ChoreApp
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                DataAccess sqlAccess = new DataAccess();
                sqlAccess.CreateDB();
                sqlAccess.CreateChoreTable();
                UserDataInput.ImputData(sqlAccess);
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
