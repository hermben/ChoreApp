using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ChoreApp
{
    class UserDataInput
    {
        public static void ImputData(DataAccess sqlAccess)
        {
                bool input = true;
                int dataInput = 0;
                while (input)
                {
                    Console.Clear();
                    Console.WriteLine("Please Add ,update, delete or print Chores");
                    Console.WriteLine("1. Add Chores.");
                    Console.WriteLine("2. Update Chores.");
                    Console.WriteLine("3. Delete Chores.");
                    Console.WriteLine("4. Show  Chores.");
                    Console.WriteLine("5. Exit.");
                    Int32.TryParse(Console.ReadLine(), out dataInput);//returns true or false

                    switch (dataInput)
                    {
                        //Add chore
                        case 1:                        
                            UserAddChore(sqlAccess);
                            Enter();
                             break;

                        //Update Chore
                        case 2:
                            UserUpdateChore(sqlAccess);
                            Enter();
                            break;

                        //Delete Chore
                        case 3:
                            UserDeleteChore(sqlAccess);
                            Enter();
                            break;

                        //Print Chores
                        case 4:
                            PrintChores(sqlAccess);
                            Enter();
                            break;

                        //Exit
                        case 5:
                            input = false;
                            break;
                    }

                }
            
        }

        private static void UserAddChore(DataAccess sqlAccess)
        {
            Console.Clear();
            Console.Write("Add chore name:");
            string nameInput = Console.ReadLine();
            Console.Write("Add chore details: ");
            string detailsInput = Console.ReadLine();
            int rowsToBeAffected = sqlAccess.AddChore(nameInput, detailsInput);
            Console.WriteLine(rowsToBeAffected + " row(s) Added");
        }

        private static void UserUpdateChore(DataAccess sqlAccess)
        {
            Console.Clear();
            PrintChores(sqlAccess);
            string choreToUpdateName;
            Console.Write("Enter the Chore name to update:");
            choreToUpdateName = Console.ReadLine();
            Console.Write("Enter the chore new details : ");
            string detailsInput = Console.ReadLine();
            int rowsToBeAffected = sqlAccess.UpdateChore(detailsInput, choreToUpdateName);
            Console.WriteLine(rowsToBeAffected + " row(s) Updated");
        }

        private static void UserDeleteChore(DataAccess sqlAccess)
        {
            Console.Clear();
            PrintChores(sqlAccess);
            string choreToDeleteName;
            Console.Write("Enter the name of chore to delete:");
            choreToDeleteName = Console.ReadLine();
            int rowsToBeAffected = sqlAccess.DeleteChore(choreToDeleteName);
            Console.WriteLine(rowsToBeAffected + " row(s) deleted");
        }

        private static void PrintChores(DataAccess sqlAccess)
        {
            var chores = new List<string>();
            chores = sqlAccess.GetChores();

            foreach (var item in chores)
            {
                Console.WriteLine(item);
            }
        }

        private static void Enter()
        {
            Console.Write("Press any key to continue...");
            Console.ReadKey(true);
        }

    }
}
