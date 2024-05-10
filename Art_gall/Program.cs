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


            // Create an instance of VirtualArtGallery
            VirtualArtGallery virtualArtGallery = new VirtualArtGallery();
            Artwork artwork = new Artwork();

            // Set artwork properties as needed
            artwork.Title = "Example Title";
            artwork.Description = "Example Description";
            artwork.CreationDate = DateTime.Now;
            artwork.Medium = "Example Medium";
            artwork.ImageURL = "Example Image URL";


            // Call the GetArtworkList() method
            List<Artwork> artworkList = virtualArtGallery.GetArtworkList();

            bool isadded = virtualArtGallery.AddArtwork(artwork);
            // Check the result
            if (isadded)
            {
                Console.WriteLine("Artwork added successfully.");
            }
            else
            {
                Console.WriteLine("Failed to add artwork.");
            }

            //remove Art work
            Console.Write("Enter the ID of the artwork to remove: ");
            if (!int.TryParse(Console.ReadLine(), out int artworkIDToRemove) || artworkIDToRemove <= 0)
            {
                Console.WriteLine("Invalid artwork ID. Please enter a valid integer greater than 0.");
                return; // Exit the program if the ID is invalid
            }

            // Call the RemoveArtwork method
            bool isArtworkRemoved = virtualArtGallery.RemoveArtwork(artworkIDToRemove);

            // Check the result
            if (isArtworkRemoved)
            {
                Console.WriteLine("Artwork removed successfully.");
            }
            else
            {
                Console.WriteLine("Failed to remove artwork.");
            }
            //bool updated = gallery.UpdateArtwork(artwork2);
            //Console.WriteLine($"Updated artwork 1: {updated}");

        }
      
    }
}
