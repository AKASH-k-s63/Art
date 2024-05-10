using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Art_gall.Model
{
    public class User
    {
        public int UserID { get; set; }  //Primary Key
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateOnly DateofBirth { get; set; }
        public byte[] ProfilePicture { get; set; }
        public string FavouriteArtWorks { get; set; }
    }
}
