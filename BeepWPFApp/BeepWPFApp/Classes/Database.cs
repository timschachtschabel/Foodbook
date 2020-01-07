
using System.Collections.Generic;
using System.Net.Sockets;
using System.Windows;
using BeepWPFApp.Classes;
using MySql.Data.MySqlClient;

namespace BeepWPFApp
{
    
    public class Database
    {
        static string Server = "localhost";
        private static string DB = "producten";
        private static string UserName = "root";
        private static string Password = "";


        static string connectionString = "SERVER=" + Server + ";" + "DATABASE = " + DB + ";" + "UID=" + UserName + ";" + "PASSWORD=" + Password + ";";

        MySqlConnection connection = new MySqlConnection(connectionString);

        public Database()
        {

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

                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        GlobalSettings.AllergieString = result.GetString("Allergies");
                        GlobalSettings.Naam = result.GetString("Name");
                        GlobalSettings.Email = result.GetString("Email");
                        CloseConnection();
                        return true;
                    }
                    
                }
                else
                {
                    CloseConnection();
                    return false;

                }
                CloseConnection();

                return false;

            }
            catch (MySqlException e)
            {
                MessageBox.Show(e.ToString());
                CloseConnection();
                return false;
            }
        }

        public bool ProductExist(string barcode)
        {
            OpenConnection();

            string cmd = $"SELECT * FROM `bleep`.`bl_product` WHERE `Barcode` = '{barcode}' LIMIT 0,1000";
            MySqlCommand Command = new MySqlCommand(cmd, connection);

            MySqlDataReader result = Command.ExecuteReader();
            if (result.HasRows)
            {
                CloseConnection();
                return true;
            }

            CloseConnection();
            return false;
        }

        public string DbGetProductName(string barcode)
        {
            OpenConnection();
            string cmd = $"SELECT * FROM `bleep`.`bl_product` WHERE `Barcode` = '{barcode}'";
            MySqlCommand Command = new MySqlCommand(cmd, connection);

            MySqlDataReader result = Command.ExecuteReader();

            while (result.Read())
            {
                string naam = result.GetString("Name");
                CloseConnection();
                return naam;
            }

            CloseConnection();
            
            return "notfound";
        }

        public void CacheProduct(string barcode, string naam,string prijs, string ingredient, string allergie)
        {
            try
            {
                
                //Verbind
                OpenConnection();
                string cmd =
                    $"INSERT INTO producten (Naam, Prijs, Barcode, Ingredienten, Allergie) Values ('{naam}','{prijs}','{barcode}','{ingredient}','{allergie}');";

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
