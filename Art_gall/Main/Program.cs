using Art_gall.DAO.Service;
using Art_gall.Model;
using Art_gall.Util;
using Microsoft.Data.SqlClient;
using System;
using static Art_gall.Util.DBPropertyUtil;

namespace Art_gall.Main
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
            CrimeAnalysisServiceImpl virtualArtGallery = new CrimeAnalysisServiceImpl();

            

            //End
            UserloginServices userdetails = new UserloginServices();
            // Display main menu options
            Console.WriteLine("Main Menu:");
            Console.WriteLine("1. Login");
            Console.WriteLine("2. Register");
            Console.WriteLine("3. Logout");
            // Add more menu options as needed...

            // Get user input
            int choice = int.Parse(Console.ReadLine());

            // Perform actions based on user choice
            switch (choice)
            {
                case 1:
                    Console.WriteLine("Please Enter Login Credentials");
                    Console.WriteLine("Enter your username: ");
                    string username = Console.ReadLine();

                    Console.WriteLine("Enter your password: ");
                    string password = Console.ReadLine();

                    User LoginUser = userdetails.LoginbyUser(username, password);
                    if (LoginUser != null) // Check if Artworkbyid is not null
                    {// Assuming you have a method to remove artwork from the gallery
                        Console.WriteLine($"User LOGIN successfully with Id {LoginUser}.");
                    }
                    else
                    {
                        Console.WriteLine("Failed to Login With USer detail provided.");
                    }
                    break;
                case 2:
                    Console.WriteLine("Please Enter New Registration Credentials");
                    User Adduser = new User();
                    bool isadduser = userdetails.RegisterUser(Adduser);
                    if (isadduser)
                    {
                        Console.WriteLine("User added successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Failed to add User.");
                    }
                    break;
                case 3:
                    Console.WriteLine("Logout Procedure Starts");
                    User logout = userdetails.Logout();
                   
                    break;
            }

         
            
            Console.WriteLine("---------------GEt ALL USERS List Start-----------------------");
            List<User> Userslist = userdetails.GetAllUsers();
            Console.WriteLine("---------------GEt ALL USERS List End-----------------------");
            

            //Artwork instance
            Artwork artwork = new Artwork();
            Artwork updateartwork = new Artwork();

            // Call the GetArtworkList() method
            //total list
            Console.WriteLine("---------------GEt total Artwork List Start-----------------------");
            List<Artwork> artworkList = virtualArtGallery.GetArtworkList();
            Console.WriteLine("----------------------GEt total Artwork List END-------------------");
            // find by artworkId
            Console.WriteLine("-----------------------Find Artwork By ID Start--------------------");
            Console.Write("Enter the ID of the artwork to Find: ");
            if (!int.TryParse(Console.ReadLine(), out int artworkID) || artworkID <= 0)
            {
                Console.WriteLine("Invalid artwork ID. Please enter a valid integer greater than 0.");
                return; // Exit the program if the ID is invalid
            }
            Artwork Artworkbyid = virtualArtGallery.GetArtworkById(artworkID);
            if (Artworkbyid != null) // Check if Artworkbyid is not null
            {// Assuming you have a method to remove artwork from the gallery
                Console.WriteLine($"Artwork Viewed successfully with Id {artworkID}.");
            }
            else
            {
                Console.WriteLine("Failed to find artwork with the specified ID.");
            }
            Console.WriteLine("-----------------------Find Artwork By ID END------------------------");
            //End

            Console.WriteLine("-----------------------Add Artwork Start------------------------");
            ////Add Art work //
            bool isadded = virtualArtGallery.AddArtwork(artwork);
            if (isadded)
            {
                Console.WriteLine("Artwork added successfully.");
            }
            else
            {
                Console.WriteLine("Failed to add artwork.");
            }
            Console.WriteLine("-----------------------Add ArtWork END------------------------");
            //End

            Console.WriteLine("-----------------------Update Artwork Start------------------------");
            bool isupdated = virtualArtGallery.UpdateArtwork(updateartwork);
            if (isupdated)
            {
                Console.WriteLine("Artwork Updated successfully.");
            }
            else
            {
                Console.WriteLine("Failed to Update artwork.");
            }
            Console.WriteLine("-----------------------Update Artwork END------------------------");

            Console.WriteLine("-----------------------Remove Artwork By ID Start------------------------");
            // remove Art work
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
            Console.WriteLine("-----------------------Remove Artwork By ID END------------------------");

            //End
            //search reult
            Console.WriteLine("--------Search Art Results Started ---------------------");
            // Perform artwork search
            Console.WriteLine("Enter the keyword to search for:");
            string keyword = Console.ReadLine();

            // Perform artwork search
            List<Artwork> searchResults = virtualArtGallery.SearchArtworks(keyword);
            Console.WriteLine("--------Search Art Results END ---------------------");

            Console.WriteLine("--------Add TO Fav Started ---------------------");
            Console.WriteLine("Enter the UserID:");
            int userId = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter the ArtworkID:");
            int artworkId = Convert.ToInt32(Console.ReadLine());

            // Add artwork to user's favorites
            bool success = virtualArtGallery.AddArtworkToFavorite(userId, artworkId);

            // Display result
            if (success)
            {
                Console.WriteLine("Artwork added to favorites successfully.");
            }
            else
            {
                Console.WriteLine("Failed to add artwork to favorites.");
            }
            Console.WriteLine("--------Add TO Fav END ---------------------");


            Console.WriteLine("--------Remove Add TO Fav Started ---------------------");
            // Remove artwork from user's favorites
            Console.WriteLine("Enter the UserID:");
            int userIdremove = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter the ArtworkID:");
            int artworkIdremove = Convert.ToInt32(Console.ReadLine());
            bool removeSuccess = virtualArtGallery.RemoveArtworkFromFavorite(userIdremove, artworkIdremove);
            if (removeSuccess)
            {
                Console.WriteLine("Artwork removed from favorites successfully.");
            }
            else
            {
                Console.WriteLine("Failed to remove artwork from favorites.");
            }
            Console.WriteLine("--------Remove add TO Fav END ---------------------");

            Console.WriteLine("--------Get User Fav Work Started ---------------------");
            // Get user's favorite artworks
            Console.WriteLine("Enter the UserID:");
            int favuserId = Convert.ToInt32(Console.ReadLine());

            List<Artwork> favoriteArtworks = virtualArtGallery.GetUserFavoriteArtworks(favuserId);
            if (favoriteArtworks.Count > 0)
            {
                Console.WriteLine("User's Favorite Artworks:");
                foreach (Artwork artwork1 in favoriteArtworks)
                {
                    Console.WriteLine($"ArtworkID: {artwork1.ArtworkID}, Title: {artwork1.Title}, Description: {artwork1.Description}");
                }
            }
            else
            {
                Console.WriteLine("User has no favorite artworks.");
            }

            Console.WriteLine("--------Get User Fav Work End ---------------------");



            Console.WriteLine("Enter the GalleryId:");
            int GalleryId = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter the ArtworkID:");
            int ArtworkId = Convert.ToInt32(Console.ReadLine());

            // Add artwork to user's favorites
            bool Addtogall = virtualArtGallery.AddArtworktoGallery(ArtworkId, GalleryId);

            // Display result
            if (Addtogall)
            {
                Console.WriteLine("Artwork added to Gallery successfully.");
            }
            else
            {
                Console.WriteLine("Failed to add artwork to Gallery.");
            }
            Console.WriteLine("--------Add TO Artwork Gallery END ---------------------");
        }


    }
}
