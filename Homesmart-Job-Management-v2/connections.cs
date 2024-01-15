//#include "database_config.h"
using Homesmart_Job_Management_v2;
using MySql.Data.MySqlClient;
using System;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace Connections
{
    public class DatabaseConnection
    {
        private MySqlConnection connection;

        // Constructor
        public DatabaseConnection()
        {
            //dotnet add package MySql.Data --version 8.2.0
            Initialize();
        }

        // Initialize values
        private void Initialize()
        {
            string connectionString = $"SERVER={DbConfig.Server};PORT={DbConfig.Port};DATABASE={DbConfig.Database};USER={DbConfig.Uid};PASSWORD={DbConfig.Password};";

            connection = new MySqlConnection(connectionString);
        }

        // Open connection to the database
        public bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        Console.WriteLine("Cannot connect to server. Contact administrator");
                        break;

                    case 1045:
                        Console.WriteLine("Invalid username/password, please try again");
                        break;
                }
                return false;
            }
        }

        // Close connection
        public bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        //Check connection status
        public MySqlConnection GetConnection()
        {
            return connection;
        }
    }
}