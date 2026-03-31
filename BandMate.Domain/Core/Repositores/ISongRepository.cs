using BandMate.Domain.Core.Models;

namespace BandMate.Domain.Core.Repositores
{
    public interface ISongRepository
    {
        Task<Song> GetAsync(int songID);
        Task<IEnumerable<Song>> GetByBandIDAsync(int bandID);
        Task DeleteAsync(int id);
        Task<Song> CreateAsync(Song song);
    }
}
