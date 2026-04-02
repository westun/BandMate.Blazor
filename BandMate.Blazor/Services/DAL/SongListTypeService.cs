using AutoMapper;
using BandMate.Blazor.DTO;
using BandMate.Domain.Core.Models;
using BandMate.Domain.Core.Repositores;

namespace BandMate.Blazor.Services.DAL
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

        public async Task<SongListTypeDTO> CreateAsync(SongListTypeDTO songListTypeDTO)
        {
            var songListType = this.mapper.Map<SongListType>(songListTypeDTO);

            var songListTypeFromDatabase = await this.songListTypeRepository.CreateAsync(songListType);

            return this.mapper.Map<SongListTypeDTO>(songListTypeFromDatabase);
        }

        public async Task<SongListTypeDTO> UpdateAsync(SongListTypeDTO songListTypeDTO)
        {
            var songListType = this.mapper.Map<SongListType>(songListTypeDTO);

            var songListTypeFromDatabase = await this.songListTypeRepository.UpdateAsync(songListType);

            return this.mapper.Map<SongListTypeDTO>(songListTypeFromDatabase);
        }

        public async Task DeleteAsync(int id)
        {
            await this.songListTypeRepository.DeleteAsync(id);
        }
    }
}
