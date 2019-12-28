using System.Collections.Generic;
using System.Net.Sockets;
using System.Windows;
using MySql.Data.MySqlClient;
using WebaPi1.Classes;

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

                        break;
                    case 1045:
                        // Invalid username / password


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
            catch (MySqlException)
            {

                // ex.message
                return false;
            }
        }

        public bool CreateProduct(string barcode, string naam)
        {
            try
            {

                //Verbind
                OpenConnection();
                string cmd =
                    $"INSERT INTO producten (Naam, Barcode) Values ('{naam}', '{barcode}');";

                //write Queru
                MySqlCommand command = new MySqlCommand(cmd, connection);
                command.ExecuteNonQuery();

                //opruimen 
                CloseConnection();
                return true;

            }
            catch (MySqlException)
            {
                return false;
            }
        }

        public Product dbGetproductInfo(string barcode)
        {
            Product resultProduct = new Product();
            OpenConnection();
            string cmd = $"SELECT * FROM producten WHERE `Barcode` = '{barcode}'";
            MySqlCommand Command = new MySqlCommand(cmd, connection);

            MySqlDataReader result = Command.ExecuteReader();

            while (result.Read())
            {
                resultProduct.Naam = result.GetString("Naam");
                resultProduct.barcode = result.GetString("Barcode");
                resultProduct.Prijs = result.GetString("Prijs");
                resultProduct.Allergie = result.GetString("Allergie");
                resultProduct.Ingredient = result.GetString("Ingredienten");

            }

            CloseConnection();

            if (resultProduct.Naam == null)
            {
                return null;
            }

            return resultProduct;
        }

 
    }
}