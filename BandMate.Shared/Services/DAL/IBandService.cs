using BandMate.Shared.DTO;

namespace BandMate.Shared.Services.DAL
{
    public interface IBandService
    {
        Task<BandDTO> GetBand(int bandID);
        Task<IEnumerable<BandDTO>> GetAll(int accountID);
        Task<BandDTO> GetDefaultBand(int accountID);
        Task<BandDTO> CreateAsync(BandDTO band, int accountID);
    }
}