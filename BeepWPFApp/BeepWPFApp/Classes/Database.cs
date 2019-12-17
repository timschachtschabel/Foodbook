using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using BeepWPFApp.Enum;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Cms;

namespace BeepWPFApp.Classes
{
    
    class Database
    {
        static string Server = "194.171.226.182";
        private static string DB = "bleep";
        private static string UserName = "joep";
        private static string Password = "yeet";


       static string connectionString = "SERVER=" + Server + ";" + "DATABASE = " + DB + ";" + "UID=" + UserName + ";" + "PASSWORD=" + Password + ";";

        MySqlConnection connection = new MySqlConnection(connectionString);
        public Database()
        {
//            Server = "localhost";
//            DB = "csharp";
//            User = "user";
//            Password = "";
        }
        public bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException e)
            {
                switch (e.Number)
                {
                    
                    case 0:
                        // Cannot connect to server
                        MessageBox.Show(e.ToString());
                        break;
                    case 1045:
                        // Invalid username / password
                        MessageBox.Show(e.ToString());

                        break;
                }

                return false;
            }
        }

        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException e)
            {
                MessageBox.Show(e.ToString());
                // ex.message
                return false;
            }
        }

        public bool CreateUser(string naam, string password, string email, List<string>Allergie)
        {
            try
            {
                //maakt van List string
                string allergie = "";
                foreach (var item in Allergie)
                {
                    allergie = allergie + item + ",";
                }

                //Verbind
                OpenConnection();
                string cmd =
                    $"INSERT INTO bleep.bl_user (Name, Password, Email, Allergies) Values ('{naam}','{password}','{email}','{allergie}');";

                //write Queru
                MySqlCommand command = new MySqlCommand(cmd, connection);
                command.ExecuteNonQuery();

                //opruimen 
                CloseConnection();
                return true;
            }
            catch (MySqlException e)
            {
                MessageBox.Show(e.ToString());
                return false;
            }

        }

        public bool CheckUser(string naam, string password)
        {
            try
            {
                //Verbind
                OpenConnection();
                string cmd = $"SELECT * from bl_user WHERE Name = '{naam}' and password = '{password}'";

                //write Query
                MySqlCommand command = new MySqlCommand(cmd, connection);
                MySqlDataReader result = command.ExecuteReader();

                //Voeg data toe
                while (result.Read())
                {
                    User.AllergieString = result.GetString("Allergies");
                    User.Naam = result.GetString("Name");
                    User.Email = result.GetString("Email");
                    User.CreationTime = result.GetString("Date_created");
                }

                CloseConnection();
                return true;
            }
            catch (MySqlException e)
            {
                MessageBox.Show(e.ToString());
                return false;
            }
        }
    }
}
