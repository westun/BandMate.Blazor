using BandMate.Blazor.DTO;
using BandMate.Domain.Core.Models;

namespace BandMate.Blazor.Services.DAL
{
    public interface IBandService
    {
        Task<BandDTO> GetBand(int bandID);
        Task<IEnumerable<BandDTO>> GetAll(int accountID);
        Task<BandDTO> GetDefaultBand(int accountID);
        Task<BandDTO> CreateAsync(BandDTO band, int accountID);
    }
}