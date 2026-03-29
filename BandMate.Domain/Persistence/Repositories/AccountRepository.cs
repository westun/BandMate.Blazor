using BandMate.Domain.Core.Models;
using BandMate.Domain.Core.Repositores;
using Microsoft.EntityFrameworkCore;

namespace BandMate.Domain.Persistence.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IDbContextFactory<BandMateContext> dbContextFactory;

        public AccountRepository(IDbContextFactory<BandMateContext> dbContextFactory)
        {
            this.dbContextFactory = dbContextFactory;
        }

        public async Task<Account> CreateAsync(Account account)
        {
            using var dbContext = await dbContextFactory.CreateDbContextAsync();

            //TODO: make fields nullable in the database and remove this check
            if (account.B2CObjectIdentifier == null)
            {
                account.B2CObjectIdentifier = string.Empty;
            }

            if (account.FirstName == null)
            {
                account.FirstName = string.Empty;
            }

            if (account.LastName == null)
            {
                account.LastName = string.Empty;
            }

            dbContext.Accounts.Add(account);
            await dbContext.SaveChangesAsync();

            return account;
        }
    }
}
