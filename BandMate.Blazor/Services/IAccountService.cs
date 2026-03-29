using BandMate.Blazor.DTO;

namespace BandMate.Blazor.Services
{
    public interface IAccountService
    {
        Task<AccountDTO> CreateAsync(AccountDTO accountDTO);
    }
}