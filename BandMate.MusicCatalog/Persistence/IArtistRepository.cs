using BandMate.MusicCatalog.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BandMate.MusicCatalog.Persistence
{
    public interface IArtistRepository
    {
         Task<IEnumerable<Artist>> SearchArtistsAsync(string name);
         Task<IEnumerable<Album>> GetAlbums(string artistId);
    }
}
