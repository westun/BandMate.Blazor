using BandMate.Domain.Core.Models;
using BandMate.Domain.Core.Repositores;
using Microsoft.EntityFrameworkCore;

namespace BandMate.Domain.Persistence.Repositories
{
    public class BandAccountRepository : IBandAccountRepository
    {
        private readonly IDbContextFactory<BandMateContext> dbContextFactory;

        public BandAccountRepository(IDbContextFactory<BandMateContext> dbContextFactory)
        {
            this.dbContextFactory = dbContextFactory;
        }

        public async Task SaveDefaultBandAsync(int bandID, int accountID)
        {
            using var dbContext = await dbContextFactory.CreateDbContextAsync();

            var bands = await dbContext.BandAccounts.Where(ba => ba.AccountID == accountID).ToListAsync();

            foreach (var bandAccount in bands)
            {
                bandAccount.Default = bandAccount.BandID == bandID;
            }

            await dbContext.SaveChangesAsync();
        }
    }
}
