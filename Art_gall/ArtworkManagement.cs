﻿using Art_gall.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Art_gall
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

        public static void AddUserData(User user)
        {
            Console.WriteLine("Enter the Required Data for adding users:");
            Console.WriteLine("Enter User Name:");
            user.Username = Console.ReadLine();

            Console.WriteLine("Enter Password:");
            user.Password = Console.ReadLine();
            /*var Encryptpass = Encryptpwd(Passwor);
            user.Password = Encryptpass;
            Console.WriteLine($"Encrypted pwd:{Encryptpass}");*/

            Console.WriteLine("Enter Email Address:");
            user.Email = Console.ReadLine();

            Console.WriteLine("Enter the First Name:");
            user.FirstName = Console.ReadLine();

            Console.WriteLine("Enter the Last Name:");
            user.LastName = Console.ReadLine();

            Console.WriteLine("Enter DOB (YYYY-MM-DD):");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime BirthDate))
            {
                throw new ArgumentException("Invalid date format. Please enter date in YYYY-MM-DD format.");
            }
            user.DateOfBirth = BirthDate;

            Console.WriteLine("Upload User Profile");
            user.ProfilePicture = Console.ReadLine();
        }

        public static string Encryptpwd(string password)
        {
            try
            {
                using (SHA256 sha256 = SHA256.Create())
                {
                    // Compute hash from the password bytes
                    byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                    // Convert byte array to a string
                    StringBuilder builder = new StringBuilder();
                    for (int i = 0; i < hashBytes.Length; i++)
                    {
                        var res = builder.Append(hashBytes[i].ToString("x2")); // Convert each byte to a hexadecimal string
                    }
                    return builder.ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }


}
