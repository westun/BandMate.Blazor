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

            var songDTOs = this.mapper.Map<IEnumerable<SongDTO>>(songs);

            songDTOs = songDTOs.OrderBy(s => s.Artist).ThenBy(s => s.Title);

            songDTOs.Select(s =>
            {
                s.CreatedDateForDisplay = s.CreatedDate.ToString("d");
                return s;
            }).ToList();

            return songDTOs;
        }

    }
}
