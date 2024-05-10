using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Art_gall.Model
{
    public class Gallery
    {
        public int GalleryID { get; set; }  //Primary Key
        public string Name { get; set; }
        public string Description { get; set; }
        public string Curator { get; set; }
        public DateTime OpeningHours { get; set; }
    }
}
