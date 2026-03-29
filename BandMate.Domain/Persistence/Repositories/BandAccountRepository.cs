using BandMate.Domain.Core.Models;
using BandMate.Domain.Core.Repositores;
using Microsoft.EntityFrameworkCore;

namespace BandMate.Domain.Persistence.Repositories
{
    public class BandAccountRepository
    {
        private readonly IDbContextFactory<BandMateContext> dbContextFactory;

        public BandAccountRepository(IDbContextFactory<BandMateContext> dbContextFactory)
        {
            this.dbContextFactory = dbContextFactory;
        }
    }
}
