using BandMate.Domain.Core.Models;

namespace BandMate.Domain.Core.Repositores
{
    public interface ISongListTypeRepository
    {
        Task<IEnumerable<SongListType>> GetByBandIDAsync(int bandID);
    }
}