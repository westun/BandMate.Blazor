using AutoMapper;
using BandMate.Domain.Core.Models;
using BandMate.Domain.Core.Repositores;
using BandMate.Shared.DTO;

namespace BandMate.Shared.Services.DAL
{
    public class BandNameService : IBandNameService
    {
        private readonly IBandNameRepository bandNameRepository;
        private readonly IMapper mapper;

        public BandNameService(
            IBandNameRepository bandNameRepository,
            IMapper mapper)
        {
            this.bandNameRepository = bandNameRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<BandNameDTO>> GetAllByBandID(int bandID)
        {
            var bandNames = await this.bandNameRepository.GetAllByBandID(bandID);

            return this.mapper.Map<IEnumerable<BandNameDTO>>(bandNames);
        }

        public async Task<BandNameDTO> CreateAsync(BandNameDTO bandNameDTO)
        {
            var bandName = this.mapper.Map<BandName>(bandNameDTO);

            var bandNameFromDatabase = await this.bandNameRepository.CreateAsync(bandName);

            return this.mapper.Map<BandNameDTO>(bandNameFromDatabase);
        }
    }
}
