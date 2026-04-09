using BandMate.Shared.DTO;

namespace BandMate.Shared.Services.DAL
{
    public interface IAccountService
    {
        Task<AccountDTO> CreateAsync(AccountDTO accountDTO);
        Task<IEnumerable<AccountDTO>> GetAllByBandID(int bandID);
    }
}