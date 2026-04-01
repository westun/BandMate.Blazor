using BandMate.Blazor.DTO;

namespace BandMate.Blazor.Services
{
    public interface ISongService
    {
        Task<IEnumerable<SongDTO>> GetSongsByBandIDAsync(int bandID);
        Task DeleteAsync(int id);
        Task<SongDTO> CreateAsync(SongDTO songDTO);

        Task<SongDTO> UpdateAsync(SongDTO songDTO);
    }
}