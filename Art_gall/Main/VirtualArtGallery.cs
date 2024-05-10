using Art_gall.Util;
using Microsoft.Data.SqlClient;
using System;
using Art_gall.Model;
using Art_gall.DAO;

namespace Art_gall.Main
{
    public class VirtualArtGallery : VirtualArtGalleryBase
    {

        // Connection string for connecting to the database
        private string connectionString;

        public VirtualArtGallery()
        {
            connectionString = PropertyUtil.GetPropertyString();
        }
        //public override List<Artwork> GetArtworkList()
        //{
            
        //}

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

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                string sql = "INSERT INTO Artworks (Title, Description, CreationDate, Medium, ImageURL) VALUES (@Title, @Description, @CreationDate, @Medium, @ImageURL); SELECT SCOPE_IDENTITY();";

                command.CommandText = sql;
                command.Parameters.AddWithValue("@Title", artwork.Title);
                command.Parameters.AddWithValue("@Description", artwork.Description);
                command.Parameters.AddWithValue("@CreationDate", artwork.CreationDate);
                command.Parameters.AddWithValue("@Medium", artwork.Medium);
                command.Parameters.AddWithValue("@ImageURL", artwork.ImageURL);

                connection.Open();

                int lastInsertedId = Convert.ToInt32(command.ExecuteScalar());
                artwork.ArtworkID = lastInsertedId;

                return lastInsertedId > 0;
            }

            // Connection will be automatically closed when exiting the using block
        }
    }
}
