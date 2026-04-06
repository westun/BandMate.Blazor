using BandMate.Blazor.DTO.MusicCatalog;

namespace BandMate.Blazor.Services
{
    public interface IMusicCatalogService
    {
        Task<IEnumerable<AlbumDTO>> GetAlbumsAsync(string artistId);
        Task<IEnumerable<ArtistDTO>> SearchArtistsAsync(string name);
        Task<AlbumDTO> GetAlbumAsync(string albumId);
        Task<TrackDTO> GetTrackAsync(string trackId);
    }
}