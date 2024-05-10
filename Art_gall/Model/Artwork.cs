using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Art_gall.Model
{

    public class Artwork
    {
        public int ArtworkID { get; set; } //primarykey
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public string Medium { get; set; }
        public string ImageURL { get; set; }  //ImageURL (or any reference to the digital representation)
    }

    //internal class Artwork
    //{
    //}
}
