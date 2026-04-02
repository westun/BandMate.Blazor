using AutoMapper;
using BandMate.Domain.Core.Repositores;

namespace BandMate.Blazor.Services.DAL
{
    public class BandAccountService : IBandAccountService
    {
        private readonly IBandAccountRepository bandAccountRepository;
        private readonly IMapper mapper;

        public BandAccountService(
            IBandAccountRepository bandAccountRepository,
            IMapper mapper)
        {
            this.bandAccountRepository = bandAccountRepository;
            this.mapper = mapper;
        }

        public async Task SaveDefaultBandAsync(int bandID, int accountID)
        {
            await this.bandAccountRepository.SaveDefaultBandAsync(bandID, accountID);
        }
    }
}
