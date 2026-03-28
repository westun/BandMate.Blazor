using BandMate.Blazor.DTO;

namespace BandMate.Blazor.Services
{
    public interface ISongListTypeService
    {
        Task<IEnumerable<SongListTypeDTO>> GetBandIDAsync(int bandID);
    }
}