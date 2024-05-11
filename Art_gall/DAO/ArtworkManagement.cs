using Art_gall.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Art_gall.DAO
{
    public class ArtworkManagement
    {
        //Add artwork input get
        public static void GetArtworkDetailsFromUser(Artwork artwork)
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

        //Update artwork input get
        public static void UpdateArtworkDetailsFromUser(Artwork artwork)
        {
            Console.WriteLine("Enter The artwork ID:");
            Console.Write("ArtworkID: ");
            //artwork.ArtworkID = Console.ReadLine();
            string input = Console.ReadLine();

            // Parse the input string to an integer
            if (int.TryParse(input, out int artworkID))
            {
                // Set the artwork ID
                artwork.ArtworkID = artworkID;
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid integer for ArtworkID.");
                // Optionally, you can handle the invalid input scenario here
            }

            Console.WriteLine("Enter updated artwork details:");
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

        //public static int GetArtworkIDToRemoveFromUser()
        //{
        //    int artworkIDToRemove;
        //    bool isValidInput = false;

        //    do
        //    {
        //        Console.Write("Enter the ID of the artwork to remove: ");
        //        string input = Console.ReadLine();

        //        if (int.TryParse(input, out artworkIDToRemove) && artworkIDToRemove > 0)
        //        {
        //            isValidInput = true;
        //        }
        //        else
        //        {
        //            Console.WriteLine("Invalid artwork ID. Please enter a valid integer greater than 0.");
        //        }
        //    } while (!isValidInput);

        //    return artworkIDToRemove;
        //}
    }


}
