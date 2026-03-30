using BandMate.Domain.Core.Models;

namespace BandMate.Domain.Core.Repositores
{
    public interface IBandRepository
    {
        Task<Band> GetAsync(int bandID);
        Task<Band> GetDefaultBandAsync(int accountID);
        Task<IEnumerable<Band>> GetAllAsync(int accountID);
        Task<Band> CreateAsync(Band band);
    }
}