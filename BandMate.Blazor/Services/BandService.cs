using AutoMapper;
using BandMate.Blazor.DTO;
using BandMate.Domain.Core.Repositores;

namespace BandMate.Blazor.Services
{
    public class BandService : IBandService
    {
        private readonly IBandRepository bandRepository;
        private readonly IMapper mapper;

        public BandService(
            IBandRepository bandRepository,
            IMapper mapper)
        {
            this.bandRepository = bandRepository;
            this.mapper = mapper;
        }

        public async Task<BandDTO> GetBand(int bandID)
        {
            var band = await this.bandRepository.GetAsync(bandID);

            return this.mapper.Map<BandDTO>(band);
        }

        public async Task<BandDTO> GetDefaultBand(int accountID)
        {
            var band = await this.bandRepository.GetDefaultBand(accountID);

            return this.mapper.Map<BandDTO>(band);
        }
    }
}
