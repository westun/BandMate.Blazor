using BandMate.Domain.Core.Models;

namespace BandMate.Domain.Core.Repositores
{
    public interface IBandRepository
    {
        Task<Band> GetAsync(int bandID);
    }
}