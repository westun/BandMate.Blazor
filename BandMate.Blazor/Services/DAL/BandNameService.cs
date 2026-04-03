using AutoMapper;
using BandMate.Blazor.DTO;
using BandMate.Domain.Core.Repositores;

namespace BandMate.Blazor.Services.DAL
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
    }
}
