using BandMate.Blazor.DTO;

namespace BandMate.Blazor.Services.DAL
{
    public interface ISongService
    {
        Task<SongDTO> GetByIdAsync(int songID);
        Task<IEnumerable<SongDTO>> GetSongsByBandIDAsync(int bandID);
        Task DeleteAsync(int id);
        Task<SongDTO> CreateAsync(SongDTO songDTO);
        Task<SongDTO> UpdateAsync(SongDTO songDTO);
    }
}