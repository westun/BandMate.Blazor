using BandMate.Shared.DTO.MusicCatalog;

namespace BandMate.Shared.Services
{
    public interface IMusicCatalogService
    {
        Task<IEnumerable<AlbumDTO>> GetAlbumsAsync(string artistId);
        Task<IEnumerable<ArtistDTO>> SearchArtistsAsync(string name);
        Task<AlbumDTO> GetAlbumAsync(string albumId);
        Task<TrackDTO> GetTrackAsync(string trackId);
    }
}