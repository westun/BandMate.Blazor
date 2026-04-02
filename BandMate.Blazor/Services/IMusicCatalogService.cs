using BandMate.Blazor.DTO.MusicCatalog;
using BandMate.MusicCatalog.Models;

namespace BandMate.Blazor.Services
{
    public interface IMusicCatalogService
    {
        Task<IEnumerable<AlbumDTO>> GetAlbumsAsync(string artistId);
        Task<IEnumerable<ArtistDTO>> SearchArtistsAsync(string name);
    }
}