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

        public async Task<IEnumerable<Band>> GetAllAsync(int accountID)
        {
            using var dbContext = await dbContextFactory.CreateDbContextAsync();

            return await dbContext.BandAccounts.Where(ba => ba.AccountID == accountID)
                .Include(ba => ba.Band)
                .Select(ba => ba.Band)
                .ToListAsync();
        }

        public async Task<Band> GetDefaultBandAsync(int accountID)
        {
            using var dbContext = await dbContextFactory.CreateDbContextAsync();

            return await dbContext.Bands
                .FirstOrDefaultAsync(b => b.BandAccounts.Any(ba => ba.AccountID == accountID && ba.Default));
        }

        public async Task<Band> CreateAsync(Band band)
        {
            using var dbContext = await dbContextFactory.CreateDbContextAsync();

            dbContext.Bands.Add(band);
            await dbContext.SaveChangesAsync();

            return band;
        }
    }
}
