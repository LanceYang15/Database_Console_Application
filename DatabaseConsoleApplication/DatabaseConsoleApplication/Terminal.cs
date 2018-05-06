using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DatabaseConsoleApplication
{
    class Terminal
    {
        bool running;
        bool update;
        int gamePrice;
        int gameID;
        int intInput;
        string databaseName;
        string query;
        string newDatabase;
        string deleteTable;
        string deleteDatabase;
        string gameName;
        string gameConsole;
        string gameCategory;



        //DataTable databases;
        UserInterface UI;
        SqlConnection databaseConnection;

        public Terminal()
        {
            //Default name is "Master"
            databaseName = "TestDatabase2";
            running = true;
            UI = new UserInterface();
            databaseConnection = new SqlConnection("Server=.; Database=" + databaseName + "; Integrated Security=true");
            run();
        }

        public void run()
        {
            while (running)
            {
                Console.Clear();
                MenuDisplay();
            }

            Console.WriteLine("console turning off, goodbye!");

        }

        public void MenuDisplay()
        {
            Console.WriteLine("[1] create database");
            Console.WriteLine("[2] select database");
            Console.WriteLine("[3] enter query to create table");
            Console.WriteLine("[4] show tables");
            Console.WriteLine("[5] delete table");
            Console.WriteLine("[6] delete database");
            Console.WriteLine("[7] add game data");
            Console.WriteLine("[8] show table data");
            Console.WriteLine("[9] update table data");
            Console.WriteLine("[10] delete table data");
            Console.WriteLine("[0] EXIT");
            MenuSelection();
        }

        public void MenuSelection()
        {
            switch (UI.GetStringInput())
            {
                case "1":
                    CreateDatabase();
                    break;

                case "2":
                    ChangeDatabase();
                    break;

                case "3":
                    EnterQuery();
                    break;

                case "4":
                    ShowTable();
                    break;

                case "5":
                    DeleteTable();
                    break;

                case "6":
                    DeleteDatabase();
                    break;

                case "7":
                    AddNewGame();
                    break;

                case "8":
                    ShowTableData();
                    break;

                case "9":
                    UpdateTableValue();
                    break;

                case "10":
                    DeleteTableData();
                    break;

                case "0":
                    running = false;
                    break;

                default:
                    Console.WriteLine("no clue sorry");
                    Console.ReadKey();
                    break;
            }
        }


        public void DeleteTableData()
        {
            Console.Clear();
            Console.WriteLine("Please enter the Game ID in which game you would like to remove.");
            gameID = UI.GetIntInput();

            try
            {
                databaseConnection.Open();
                SqlCommand delete = new SqlCommand("DELETE FROM Game WHERE GameID = @1", databaseConnection);
                using (delete)
                {
                    delete.Parameters.AddWithValue("@1", gameID);

                    delete.ExecuteNonQuery();
                }
                databaseConnection.Close();
                Console.WriteLine("Data Removed");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: DeleteTableData");
            }
            Console.ReadKey();
        }


        public void UpdateTableValue()
        {
            Console.Clear();

            try
            {
                ObtainGameData();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: UpdatingTableValue");
            }

            //Console.WriteLine("Exiting UpdateTableValue");
            Console.ReadKey();
        }


        public void ObtainGameData()
        {
            update = true;
            GetGameID();

            while (update)
            {
                ShowGameData();
                UpdateMenu();
            }
        }


        public void UpdateMenu()
        {
            Console.WriteLine("========================================");
            Console.WriteLine("What would you like to update?");
            Console.WriteLine("[1] Name");
            Console.WriteLine("[2] Price");
            Console.WriteLine("[3] Console");
            Console.WriteLine("[4] Category");
            Console.WriteLine("[5] Exit");
            Console.WriteLine("Please enter a number");
            UpdateSelectionAction(intInput = UI.GetIntInput());
        }


        public void UpdateSelectionAction(int userInput)
        {
            switch (userInput)
            {
                case 1:
                    Console.WriteLine("Please enter a new Name");
                    gameName = UI.GetStringInput();
                    UpdateName();
                    break;

                case 2:
                    Console.WriteLine("Please enter a new Price");
                    gamePrice = UI.GetIntInput();
                    UpdatePrice();
                    break;

                case 3:
                    Console.WriteLine("Please enter a new Console");
                    gameConsole = UI.GetStringInput();
                    UpdateGameConsole();
                    break;

                case 4:
                    Console.WriteLine("Please enter a new Category");
                    gameCategory = UI.GetStringInput();
                    UpdateGameCategory();
                    break;

                case 5:
                    Console.WriteLine("exiting update menu");
                    Console.ReadKey();
                    update = false;
                    break;
            }
        }


        public void UpdateGameCategory()
        {
            try
            {
                databaseConnection.Open();
                SqlCommand updateName = new SqlCommand("UPDATE Game SET Category = @2 WHERE GameID = @1", databaseConnection);
                using (updateName)
                {
                    updateName.Parameters.AddWithValue("@1", gameID);
                    updateName.Parameters.AddWithValue("@2", gameCategory);

                    updateName.ExecuteNonQuery();
                }
                databaseConnection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: UpdateName");
            }
        }


        public void UpdateGameConsole()
        {
            try
            {
                databaseConnection.Open();
                SqlCommand updateName = new SqlCommand("UPDATE Game SET Console = @2 WHERE GameID = @1", databaseConnection);
                using (updateName)
                {
                    updateName.Parameters.AddWithValue("@1", gameID);
                    updateName.Parameters.AddWithValue("@2", gameConsole);

                    updateName.ExecuteNonQuery();
                }
                databaseConnection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: UpdateName");
            }
        }


        public void UpdatePrice()
        {
            try
            {
                databaseConnection.Open();
                SqlCommand updateName = new SqlCommand("UPDATE Game SET Price = @2 WHERE GameID = @1", databaseConnection);
                using (updateName)
                {
                    updateName.Parameters.AddWithValue("@1", gameID);
                    updateName.Parameters.AddWithValue("@2", gamePrice);

                    updateName.ExecuteNonQuery();
                }
                databaseConnection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: UpdateName");
            }
        }


        public void UpdateName()
        {
            try
            {
                databaseConnection.Open();
                SqlCommand updateName = new SqlCommand("UPDATE Game SET Name = @2 WHERE GameID = @1", databaseConnection);
                using (updateName)
                {
                    updateName.Parameters.AddWithValue("@1", gameID);
                    updateName.Parameters.AddWithValue("@2", gameName);

                    updateName.ExecuteNonQuery();
                }
                databaseConnection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: UpdateName");
            }
        }


        public void ShowGameData()
        {
            Console.Clear();
            try
            {
                databaseConnection.Open();
                SqlCommand getGame = new SqlCommand("SELECT * FROM Game WHERE GameID = @1", databaseConnection);
                using (getGame)
                {
                    getGame.Parameters.AddWithValue("@1", gameID);

                    SqlDataReader reader = getGame.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine("GameID: " + $"{reader["GameID"]}");
                            Console.WriteLine("Name: " + $"{reader["Name"]}");
                            Console.WriteLine("Price: " + $"{reader["Price"]}");
                            Console.WriteLine("Console: " + $"{reader["Console"]}");
                            Console.WriteLine("Category: " + $"{reader["Category"]}");
                        }
                    }
                }
                databaseConnection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: ShowGameData");
            }


        }


        public void GetGameID()
        {
            Console.WriteLine("What is the GameID you wish to update?");
            gameID = UI.GetIntInput();
        }


        public void ShowTableData()
        {
            Console.Clear();

            try
            {
                databaseConnection.Open();
                SqlCommand showData = new SqlCommand("SELECT * FROM Game", databaseConnection);
                using (showData)
                {
                    SqlDataReader reader = showData.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine("GameID: " + $"{reader["GameID"]}");
                            Console.WriteLine("Name: " + $"{reader["Name"]}");
                            Console.WriteLine("Price: " + $"{reader["Price"]}");
                            Console.WriteLine("Console: " + $"{reader["Console"]}");
                            Console.WriteLine("Category: " + $"{reader["Category"]}");
                            Console.WriteLine("\n===================\n");
                        }
                    }
                }
                databaseConnection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: ShowTableData");
            }

            Console.ReadKey();
        }


        public void AddNewGame()
        {
            Console.Clear();
            Console.WriteLine("Please enter the name of the game.");
            gameName = UI.GetStringInput();
            Console.Clear();
            Console.WriteLine("Please enter the price of the game.");
            gamePrice = UI.GetIntInput();
            Console.Clear();
            Console.WriteLine("What game console is used to play this game?");
            gameConsole = UI.GetStringInput();
            Console.Clear();
            Console.WriteLine("Please enter a game category");
            gameCategory = UI.GetStringInput();

            try
            {
                databaseConnection.Open();
                SqlCommand addToTable = new SqlCommand("INSERT INTO Game(Name, Price, Console, Category) VALUES(@1, @2, @3, @4)", databaseConnection);
                using (addToTable)
                {
                    addToTable.Parameters.AddWithValue("@1", gameName);
                    addToTable.Parameters.AddWithValue("@2", gamePrice);
                    addToTable.Parameters.AddWithValue("@3", gameConsole);
                    addToTable.Parameters.AddWithValue("@4", gameCategory);

                    addToTable.ExecuteNonQuery();
                    Console.WriteLine("Added to table");
                }
                databaseConnection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: adding to table");
            }

            Console.ReadKey();
        }


        public void DeleteDatabase()
        {
            Console.Clear();
            Console.WriteLine("Enter the name of the table to delete.");
            deleteDatabase = UI.GetStringInput();

            try
            {
                databaseConnection.Open();
                SqlCommand delete = new SqlCommand("DROP DATABASE " + deleteDatabase, databaseConnection);
                using (delete)
                {
                    delete.ExecuteNonQuery();
                }

                databaseConnection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("unable to delete database.");
            }

            Console.ReadKey();
        }


        public void DeleteTable()
        {
            Console.Clear();
            Console.WriteLine("Enter the name of the table to delete.");
            deleteTable = UI.GetStringInput();

            try
            {
                databaseConnection.Open();
                SqlCommand delete = new SqlCommand("DROP TABLE " + deleteTable, databaseConnection);
                using (delete)
                {
                    delete.ExecuteNonQuery();
                }

                databaseConnection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("unable to delete table.");
            }

            Console.ReadKey();
        }


        public void ShowTable()
        {
            Console.Clear();

            try
            {
                databaseConnection.Open();
                SqlCommand showTable = new SqlCommand("USE " + databaseName + " SELECT * FROM sys.Tables;", databaseConnection);
                using (showTable)
                {
                    SqlDataReader reader = showTable.ExecuteReader();
                    while (reader.Read())
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            Console.WriteLine(reader.GetValue(i));
                        }
                        Console.WriteLine("=============[ END ]=============");
                    }
                }
                databaseConnection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: Obtaining Tables");
            }

            Console.ReadKey();
        }


        public void EnterQuery()
        {
            Console.WriteLine("Enter your query to create a table with its parameters");
            query = UI.GetStringInput();

            try
            {
                databaseConnection.Open();
                SqlCommand command = new SqlCommand("" + query, databaseConnection);
                using (command)
                {
                    command.ExecuteNonQuery();
                }

                databaseConnection.Close();
            }
            catch
            {
                Console.WriteLine("Error with query");
            }
            Console.ReadKey();
        }


        public void ChangeDatabase()
        {
            Console.WriteLine("Please enter the name of the database you would like to switch to.");
            databaseName = UI.GetStringInput();

            databaseConnection = new SqlConnection("Server=.; Database=" + databaseName + "; Integrated Security=true");
        }


        public void CreateDatabase()
        {
            Console.WriteLine("Enter a name for the database");
            newDatabase = UI.GetStringInput();
            try
            {
                databaseConnection.Open();
                SqlCommand create = new SqlCommand("CREATE DATABASE " + newDatabase, databaseConnection);
                using (create)
                {
                    create.ExecuteNonQuery();
                }
                Console.WriteLine("Database Created");
                databaseConnection.Close();
                Console.ReadKey();
            }
            catch (Exception e)
            {
                Console.WriteLine("Unable to create database");
            }
        }

    }
}
