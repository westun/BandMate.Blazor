using BandMate.Blazor.DTO;

namespace BandMate.Blazor.Services
{
    public interface IBandService
    {
        Task<BandDTO> GetBand(int bandID);
        Task<BandDTO> GetDefaultBand(int accountID);
    }
}