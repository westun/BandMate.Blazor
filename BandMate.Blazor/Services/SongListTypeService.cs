using AutoMapper;
using BandMate.Blazor.DTO;
using BandMate.Domain.Core.Repositores;

namespace BandMate.Blazor.Services
{
    public class SongListTypeService : ISongListTypeService
    {
        private readonly ISongListTypeRepository songListTypeRepository;
        private readonly IMapper mapper;

        public SongListTypeService(
            ISongListTypeRepository songListTypeRepository,
            IMapper mapper)
        {
            this.songListTypeRepository = songListTypeRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<SongListTypeDTO>> GetBandIDAsync(int bandID)
        {
            var songListTypes = await this.songListTypeRepository.GetByBandIDAsync(bandID);

            return this.mapper.Map<IEnumerable<SongListTypeDTO>>(songListTypes);
        }

    }
}
