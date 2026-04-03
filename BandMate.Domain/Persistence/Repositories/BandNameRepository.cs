using BandMate.Domain.Core.Models;
using BandMate.Domain.Core.Repositores;
using Microsoft.EntityFrameworkCore;

namespace BandMate.Domain.Persistence.Repositories
{
    public class BandNameRepository : IBandNameRepository
    {
        private readonly IDbContextFactory<BandMateContext> dbContextFactory;

        public BandNameRepository(IDbContextFactory<BandMateContext> dbContextFactory)
        {
            this.dbContextFactory = dbContextFactory;
        }

        public async Task<IEnumerable<BandName>> GetAllByBandID(int bandID)
        {
            var dbContext = await dbContextFactory.CreateDbContextAsync();

            return dbContext.BandNames.Where(bn => bn.BandID == bandID).ToList();
        }

        public async Task<BandName> CreateAsync(BandName bandName)
        {
            using var dbContext = await dbContextFactory.CreateDbContextAsync();

            dbContext.BandNames.Add(bandName);

            await dbContext.SaveChangesAsync();

            return bandName;
        }
    }
}
