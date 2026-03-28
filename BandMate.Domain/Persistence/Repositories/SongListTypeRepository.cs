using BandMate.Domain.Core.Models;
using BandMate.Domain.Core.Repositores;
using Microsoft.EntityFrameworkCore;

namespace BandMate.Domain.Persistence.Repositories
{
    public class SongListTypeRepository : ISongListTypeRepository
    {
        private readonly IDbContextFactory<BandMateContext> dbContextFactory;

        public SongListTypeRepository(IDbContextFactory<BandMateContext> dbContextFactory)
        {
            this.dbContextFactory = dbContextFactory;
        }

        public async Task<IEnumerable<SongListType>> GetByBandIDAsync(int bandID)
        {
            using var dbContext = await dbContextFactory.CreateDbContextAsync();

            return await dbContext.SongListTypes
                .Where(s => s.Band.BandID == bandID)
                .OrderBy(s => s.SortOrder)
                .ToListAsync();
        }
    }
}
