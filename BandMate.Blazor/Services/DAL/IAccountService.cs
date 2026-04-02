using BandMate.Blazor.DTO;

namespace BandMate.Blazor.Services.DAL
{
    public interface IAccountService
    {
        Task<AccountDTO> CreateAsync(AccountDTO accountDTO);
    }
}