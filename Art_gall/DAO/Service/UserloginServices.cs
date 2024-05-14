using Art_gall.Model;
using Art_gall.Util;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Art_gall.Exceptions.ExceptionHandling;


namespace Art_gall.DAO.Service
{
    public class UserloginServices : IUserActivities
    {
        private static User currentUser = null;
        private string connectionString;
        public UserloginServices()
        {
            connectionString = PropertyUtil.GetPropertyString();
        }
        public List<User> GetAllUsers()
        {
            List<User> Getusers = new List<User>();
            try
            {
                using (SqlConnection connection = DBPropertyUtil.DBConnection.GetConnection())
                {
                    connection.Open();

                    string query = "SELECT * FROM Users";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                User Userslist = new User
                                {
                                    UserID = reader.GetInt32(reader.GetOrdinal("UserID")),
                                    Username = reader.GetString(reader.GetOrdinal("Username")),
                                    FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                                    LastName = reader.GetString(reader.GetOrdinal("LastName")),
                                    Password = reader.GetString(reader.GetOrdinal("Password")),
                                    Email = reader.GetString(reader.GetOrdinal("Email")),
                                    DateOfBirth = reader.GetDateTime(reader.GetOrdinal("DateofBirth"))
                                };

                                Getusers.Add(Userslist);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new UserNotFoundException(ex.Message);
            }

            foreach (User users in Getusers)
            {
                Console.WriteLine($"USerID: {users.UserID},UserName: {users.Username},Password: {users.Password}, FirstName: {users.FirstName}, LastName: {users.LastName}, EMAIL: {users.Email}, DOB: {users.DateOfBirth}, Profile PIC: {users.ProfilePicture}");
            }
            return Getusers;

        }





        public bool RegisterUser(User user)
        {
            try
            {
                using (SqlConnection Connection = new SqlConnection(connectionString))
                {
                    Connection.Open();
                    string query = "Insert into Users(Username, Password, Email, FirstName, LastName, DateOfBirth, ProfilePicture) " +
                        "Values (@Username, @Password, @Email, @FirstName, @LastName, @DateOfBirth, @ProfilePicture)";
                    using (SqlCommand cmd = new SqlCommand(query, Connection))
                    {

                        ArtworkManagement.AddUserData(user);
                        cmd.CommandText = query;
                        cmd.Parameters.AddWithValue("@Username", user.Username);
                        cmd.Parameters.AddWithValue("@Password", user.Password);
                        cmd.Parameters.AddWithValue("@Email", user.Email);
                        cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
                        cmd.Parameters.AddWithValue("@LastName", user.LastName);
                        cmd.Parameters.AddWithValue("@DateofBirth", user.DateOfBirth);
                        cmd.Parameters.AddWithValue("@ProfilePicture", user.ProfilePicture);

                        int Useradded = cmd.ExecuteNonQuery();
                        // return rowsAffected > 0;
                        if (Useradded > 0)
                        {
                            Console.WriteLine("User Added successfully.");
                            return true;
                        }
                        else
                        {
                            Console.WriteLine("Failed to Add User .");
                            return false;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }

        public User LoginbyUser(string username, string password)
        {
            try
            {
                using (SqlConnection Connection = new SqlConnection(connectionString))
                {
                    Connection.Open();
                    string query = "SELECT * FROM Users WHERE Username = @Username AND Password = @Password";
                    using (SqlCommand cmd = new SqlCommand(query, Connection))
                    {
                        cmd.Parameters.AddWithValue("@Username", username);
                        cmd.Parameters.AddWithValue("@Password", password);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                User loginUser = new User
                                {
                                    //UserID = reader.GetInt32(reader.GetOrdinal("UserID")),
                                    Username = reader.GetString(reader.GetOrdinal("Username")),
                                    Password = reader.GetString(reader.GetOrdinal("Password")),
                                    /* Email = reader.GetString(reader.GetOrdinal("Email")),
                                     DateOfBirth = reader.GetDateTime(reader.GetOrdinal("DateofBirth"))*/
                                };
                                Console.WriteLine($"USerID: {loginUser.UserID},UserName: {loginUser.Username},Password: {loginUser.Password}, FirstName: {loginUser.FirstName}, LastName: {loginUser.LastName}, EMAIL: {loginUser.Email}, DOB: {loginUser.DateOfBirth}, Profile PIC: {loginUser.ProfilePicture}");
                                currentUser = loginUser; //Act as session storage
                                return loginUser;
                            }
                            else
                            {
                                return null;
                            }

                        }
                    }

                } 
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
           
        }


        public User Logout()
        {
            
            currentUser = null;
            Console.WriteLine("Logged out successfully.");
            Environment.Exit(0);
            Console.ReadKey();
            return currentUser;
          

        }

    }
}
