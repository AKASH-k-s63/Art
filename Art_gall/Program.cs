using Art_gall.DAO;
using Art_gall.Main;
using Art_gall.Model;
using Art_gall.Util;
using Microsoft.Data.SqlClient;
using System;
using static Art_gall.Util.DBPropertyUtil;

namespace Art_gall
{
    class Program
    {
        static void Main(string[] args)
        {
         
            // Get SqlConnection object
            using (SqlConnection connection = DBConnection.GetConnection())
            {
                try
                {
                    // Open the connection
                    connection.Open();

                    // Connection established, you can execute SQL queries here
                    Console.WriteLine("Connected to database.");

                    // Close the connection
                    //connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }

            IVirtualArtGallery gallery = new VirtualArtGallery();
            Artwork artwork1 = new Artwork
            {
                ArtworkID = 0,
                Title = "Artist 1",
                Description = "Description 1",
                Medium = "test",
                CreationDate = new DateTime(2024, 5, 1),
                ImageURL ="",
               
            };
            bool added = gallery.AddArtwork(artwork1);
            Console.WriteLine($"Added artwork 1: {added}");

            //bool updated = gallery.UpdateArtwork(artwork2);
            //Console.WriteLine($"Updated artwork 1: {updated}");

        }
      
    }
}
