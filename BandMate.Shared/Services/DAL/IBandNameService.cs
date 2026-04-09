using BandMate.Shared.DTO;

namespace BandMate.Shared.Services.DAL
{
    public interface IBandNameService
    {
        Task<IEnumerable<BandNameDTO>> GetAllByBandID(int bandID);
        Task<BandNameDTO> CreateAsync(BandNameDTO bandNameDTO);
    }
}