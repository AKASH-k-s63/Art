using Art_gall.Util;
using Microsoft.Data.SqlClient;
using System;
using Art_gall.Model;
using Art_gall.DAO;
using System.IO;
using static Art_gall.Exceptions.ExceptionHandling;

namespace Art_gall.Main
{
    public class VirtualArtGallery : VirtualArtGalleryBase
    {

        // Connection string for connecting to the database
        private string connectionString;

        //constructor
        public VirtualArtGallery()
        {
            connectionString = PropertyUtil.GetPropertyString();
        }
        public override List<Artwork> GetArtworkList()
        {
            List<Artwork> artworkList = new List<Artwork>();

            try
            {
                using (SqlConnection connection = DBPropertyUtil.DBConnection.GetConnection())
                {
                    connection.Open();

                    string query = "SELECT * FROM Artwork";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Artwork artwork = new Artwork
                                {
                                    ArtworkID = reader.GetInt32(reader.GetOrdinal("ArtworkID")),
                                    Title = reader.GetString(reader.GetOrdinal("Title")),
                                    CreationDate = reader.GetDateTime(reader.GetOrdinal("CreationDate")),
                                    ImageURL = reader.GetString(reader.GetOrdinal("ImageURL")),
                                    Description = reader.GetString(reader.GetOrdinal("Description")),
                                    Medium = reader.GetString(reader.GetOrdinal("Medium"))
                                };

                                artworkList.Add(artwork);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ArtWorkNotFoundException(ex.Message);
            }
            Console.WriteLine(artworkList);
            foreach (Artwork artwork in artworkList)
            {
                Console.WriteLine($"ArtworkID: {artwork.ArtworkID}, Title: {artwork.Title}, CreationDate: {artwork.CreationDate}, ImageURL: {artwork.ImageURL}, Description: {artwork.Description}, Medium: {artwork.Medium}");
            }
            return artworkList;

        }

        public override bool AddArtwork(Artwork artwork)
        {
            if (artwork == null)
            {
                throw new ArgumentNullException(nameof(artwork));
            }

            if (artwork.ArtworkID != 0)
            {
                throw new ArgumentException("Artwork ID must be 0 for new artworks", nameof(artwork));
            }
            // Get input from the user
            GetArtworkDetailsFromUser(artwork);

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                string sql = "INSERT INTO Artwork (Title, Description, CreationDate, Medium, ImageURL) VALUES (@Title, @Description, @CreationDate, @Medium, @ImageURL); SELECT SCOPE_IDENTITY();";

                command.CommandText = sql;
                command.Parameters.AddWithValue("@Title", artwork.Title);
                command.Parameters.AddWithValue("@Description", artwork.Description);
                command.Parameters.AddWithValue("@CreationDate", artwork.CreationDate);
                command.Parameters.AddWithValue("@Medium", artwork.Medium);
                command.Parameters.AddWithValue("@ImageURL", artwork.ImageURL);

                connection.Open();

                // Execute the command and get the last inserted identity value
                object result = command.ExecuteScalar();
                int lastInsertedId = result != DBNull.Value ? Convert.ToInt32(result) : 0;

                if (lastInsertedId > 0)
                {
                    // Set the ArtworkID of the provided artwork object
                    artwork.ArtworkID = lastInsertedId;
                    Console.WriteLine("Artwork inserted successfully.");
                    return true;
                }
                else
                {
                    Console.WriteLine("Failed to insert artwork.");
                    return false;
                }
                
            }

            // Connection will be automatically closed when exiting the using block
        }

        //if willing create a new class and write this code and call inside the method
        private void GetArtworkDetailsFromUser(Artwork artwork)
        {
            Console.WriteLine("Enter artwork details:");
            Console.Write("Title: ");
            artwork.Title = Console.ReadLine();

            Console.Write("Description: ");
            artwork.Description = Console.ReadLine();

            Console.Write("Creation Date (YYYY-MM-DD): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime creationDate))
            {
                throw new ArgumentException("Invalid date format. Please enter date in YYYY-MM-DD format.");
            }
            artwork.CreationDate = creationDate;

            Console.Write("Medium: ");
            artwork.Medium = Console.ReadLine();

            Console.Write("Image URL: ");
            artwork.ImageURL = Console.ReadLine();
        }


        //remove//
        public override bool RemoveArtwork(int artworkID)
        {
            try
            {
                if (artworkID <= 0)
                {
                    throw new ArgumentException("Artwork ID must be greater than 0", nameof(artworkID));
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = connection.CreateCommand())
                {
                    string sql = "DELETE FROM Artwork WHERE ArtworkID = @ArtworkID";

                    command.CommandText = sql;
                    command.Parameters.AddWithValue("@ArtworkID", artworkID);

                    connection.Open();

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Artwork removed successfully.");
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("Failed to remove artwork. Artwork with specified ID not found.");
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ArtWorkNotFoundException(ex.Message);
            }

            // Connection will be automatically closed when exiting the using block
        }


    }
}
