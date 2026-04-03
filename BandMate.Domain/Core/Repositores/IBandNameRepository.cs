using BandMate.Domain.Core.Models;

namespace BandMate.Domain.Core.Repositores
{
    public interface IBandNameRepository
    {
        Task<IEnumerable<BandName>> GetAllByBandID(int bandID);
    }
}