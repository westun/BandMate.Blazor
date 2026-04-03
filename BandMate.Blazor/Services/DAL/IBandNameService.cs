using BandMate.Blazor.DTO;

namespace BandMate.Blazor.Services.DAL
{
    public interface IBandNameService
    {
        Task<IEnumerable<BandNameDTO>> GetAllByBandID(int bandID);
        Task<BandNameDTO> CreateAsync(BandNameDTO bandNameDTO);
    }
}