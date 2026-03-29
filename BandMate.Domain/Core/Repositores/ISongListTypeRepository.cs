using BandMate.Domain.Core.Models;

namespace BandMate.Domain.Core.Repositores
{
    public interface ISongListTypeRepository
    {
        Task<IEnumerable<SongListType>> GetByBandIDAsync(int bandID);
        Task<SongListType> CreateAsync(SongListType songListType);
        Task<SongListType> UpdateAsync(SongListType songListType);
        Task DeleteAsync(int id);
    }
}