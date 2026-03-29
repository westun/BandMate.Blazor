using BandMate.Domain.Core.Models;

namespace BandMate.Domain.Core.Repositores
{
    public interface IAccountRepository
    {
        Task<Account> CreateAsync(Account account);
    }
}