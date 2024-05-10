using Art_gall.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Art_gall.DAO
{
    public interface IVirtualArtGallery
    {
        // Artwork Management
       // List<Artwork> GetArtworkList();
        bool AddArtwork(Artwork artwork);
       // bool UpdateArtwork(Artwork artwork);
    }

    public abstract class VirtualArtGalleryBase : IVirtualArtGallery
    {
        //public abstract List<Artwork> GetArtworkList();
        public abstract bool AddArtwork(Artwork artwork);
       // public abstract bool UpdateArtwork(Artwork artwork);
    }
}
