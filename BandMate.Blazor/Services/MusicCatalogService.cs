using AutoMapper;
using BandMate.Blazor.DTO.MusicCatalog;
using BandMate.MusicCatalog.Models;
using BandMate.MusicCatalog.Persistence;

namespace BandMate.Blazor.Services
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
    }
}
