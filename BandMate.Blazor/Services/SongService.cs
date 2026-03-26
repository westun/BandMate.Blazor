using AutoMapper;
using BandMate.Blazor.DTO;
using BandMate.Domain.Core.Repositores;

namespace BandMate.Blazor.Services
{
    public class SongService : ISongService
    {
        private readonly ISongRepository songRepository;
        private readonly IMapper mapper;

        public SongService(
            ISongRepository songRepository,
            IMapper mapper)
        {
            this.songRepository = songRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<SongDTO>> GetSongsByBandIDAsync(int bandID)
        {
            var songs = await this.songRepository.GetByBandIDAsync(bandID);

            return this.mapper.Map<IEnumerable<SongDTO>>(songs);
        }

    }
}
