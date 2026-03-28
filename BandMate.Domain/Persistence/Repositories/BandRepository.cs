using BandMate.Domain.Core.Models;
using BandMate.Domain.Core.Repositores;
using Microsoft.EntityFrameworkCore;

namespace BandMate.Domain.Persistence.Repositories
{
    public class BandRepository : IBandRepository
    {
        private readonly IDbContextFactory<BandMateContext> dbContextFactory;

        public BandRepository(IDbContextFactory<BandMateContext> dbContextFactory)
        {
            this.dbContextFactory = dbContextFactory;
        }

        public async Task<Band> GetAsync(int bandID)
        {
            using var dbContext = await dbContextFactory.CreateDbContextAsync();

            return await dbContext.Bands
                .FirstOrDefaultAsync(b => bandID == b.BandID);
        }
    }
}
