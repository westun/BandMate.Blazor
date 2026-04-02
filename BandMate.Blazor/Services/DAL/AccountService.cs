using AutoMapper;
using BandMate.Blazor.DTO;
using BandMate.Domain.Core.Models;
using BandMate.Domain.Core.Repositores;

namespace BandMate.Blazor.Services.DAL
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository accountRepository;
        private readonly IMapper mapper;

        public AccountService(
            IAccountRepository accountRepository,
            IMapper mapper)
        {
            this.accountRepository = accountRepository;
            this.mapper = mapper;
        }

        public async Task<AccountDTO> CreateAsync(AccountDTO accountDTO)
        {
            var account = this.mapper.Map<Account>(accountDTO);

            var accountFromDatabase = await this.accountRepository.CreateAsync(account);

            return this.mapper.Map<AccountDTO>(accountFromDatabase);
        }
    }
}
