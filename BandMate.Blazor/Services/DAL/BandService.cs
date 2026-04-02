using AutoMapper;
using BandMate.Blazor.DTO;
using BandMate.Domain.Core.Models;
using BandMate.Domain.Core.Repositores;

namespace BandMate.Blazor.Services.DAL
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

        public async Task<IEnumerable<BandDTO>> GetAll(int accountID)
        {
            var bands = await this.bandRepository.GetAllAsync(accountID);

            return this.mapper.Map<IEnumerable<BandDTO>>(bands);
        }

        public async Task<BandDTO> GetDefaultBand(int accountID)
        {
            var band = await this.bandRepository.GetDefaultBandAsync(accountID);

            return this.mapper.Map<BandDTO>(band);
        }

        public async Task<BandDTO> CreateAsync(BandDTO band, int accountID)
        {
            var bandMapped = this.mapper.Map<Band>(band);

            bandMapped.BandAccounts = new List<BandAccount>() { new BandAccount { AccountID = accountID, BandID = band.BandID } }; 

            var bandFromDatabase = await this.bandRepository.CreateAsync(bandMapped);

            return this.mapper.Map<BandDTO>(bandFromDatabase);
        }
    }
}
