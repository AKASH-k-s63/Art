using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Art_gall.Model
{
    public class Artist
    {
        public int ArtistID { get; set; }  //PrimaryKey
        public string Name { get; set; }
        public string Biography { get; set; }
        public DateOnly BirthDate { get; set; }
        public string Nationality { get; set; }
        public string Website { get; set; }
        public string ContactInformation { get; set; }
    }
}
