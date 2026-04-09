using AutoMapper;
using BandMate.MusicCatalog.Persistence;
using BandMate.Shared.DTO.MusicCatalog;

namespace BandMate.Shared.Services
{
    public class MusicCatalogService : IMusicCatalogService
    {
        private readonly IArtistRepository artistRepository;
        private readonly IMapper mapper;

        public MusicCatalogService(
            IArtistRepository artistRepository,
            IMapper mapper)
        {
            this.artistRepository = artistRepository ?? throw new ArgumentNullException();
            this.mapper = mapper;
        }

        public async Task<IEnumerable<ArtistDTO>> SearchArtistsAsync(string name)
        {
            var artist = await this.artistRepository.SearchArtistsAsync(name);

            return this.mapper.Map<IEnumerable<ArtistDTO>>(artist);
        }

        public async Task<IEnumerable<AlbumDTO>> GetAlbumsAsync(string artistId)
        {
            var albums = await this.artistRepository.GetAlbums(artistId);

            return this.mapper.Map<IEnumerable<AlbumDTO>>(albums);
        }

        public async Task<AlbumDTO> GetAlbumAsync(string albumId)
        {
            var album = await this.artistRepository.GetAlbum(albumId);

            return this.mapper.Map<AlbumDTO>(album);
        }

        public async Task<TrackDTO> GetTrackAsync(string trackId)
        {
            var track = await this.artistRepository.GetTrack(trackId);

            return this.mapper.Map<TrackDTO>(track);
        }
    }
}
