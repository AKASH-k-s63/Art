using System;
using Microsoft.Data.SqlClient;
using System.IO;
using Newtonsoft.Json;


namespace Art_gall.Util
{
    public static class PropertyUtil
    {
        private static SqlConnection connection;

        public static string GetPropertyString()
        {
            string propertyFilePath = "D:\\Virtual_Art_Gal\\Art_gall\\Art_gall\\appsettings.json";

            string server = "ACC-002\\SQLEXPRESS04";
            string dbname = "VirtualArt";
            string username = "AKASH_NEW_USER";
            string password = "Root123";
            string trustedconnection = "true";

            try
            {
                if (File.Exists(propertyFilePath))
                {
                    string json = File.ReadAllText(propertyFilePath);
                    dynamic jsonData = JsonConvert.DeserializeObject(json);

                    server = jsonData["Server"];
                    dbname = jsonData["Database"];
                    username = jsonData["Username"];
                    password = jsonData["Password"];
                    trustedconnection = jsonData["TrustServerCertificate"];
                }
                else
                {
                    Console.WriteLine("JSON property file not found. Using default connection details.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading JSON property file: " + ex.Message);
            }

            // This is where you construct your connection string
            string connectionString = $"Server={server};Database={dbname};User Id={username};Password={password}; TrustServerCertificate ={trustedconnection}";
            Console.WriteLine(connectionString);
            return connectionString;
        }




    }
}
