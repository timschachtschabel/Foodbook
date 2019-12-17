using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using BeepWPFApp.Enum;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Cms;

namespace BeepWPFApp.Classes
{
    
    class Database
    {
        static string Server = "194.171.226.182";
        private static string DB = "bleep";
        private static string User = "joep";
        private static string Password = "yeet";


       static string connectionString = "SERVER=" + Server + ";" + "DATABASE = " + DB + ";" + "UID=" + User + ";" + "PASSWORD=" + Password + ";";

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
                // ex.message
                return false;
            }
        }

        public void CreateUser(string naam, string password, string email, List<string>Allergie)
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
            }
            catch (MySqlException e)
            {
                MessageBox.Show(e.ToString());
            }

        }
    }
}
