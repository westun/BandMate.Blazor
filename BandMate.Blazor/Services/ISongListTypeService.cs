using BandMate.Blazor.DTO;

namespace BandMate.Blazor.Services
{
    public interface ISongListTypeService
    {
        Task<IEnumerable<SongListTypeDTO>> GetBandIDAsync(int bandID);
        Task<SongListTypeDTO> CreateAsync(SongListTypeDTO songListTypeDTO);
        Task<SongListTypeDTO> UpdateAsync(SongListTypeDTO songListTypeDTO);
        Task DeleteAsync(int id);
    }
}