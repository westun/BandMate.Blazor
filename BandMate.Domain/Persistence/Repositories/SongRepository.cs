using BandMate.Domain.Core.Models;
using BandMate.Domain.Core.Repositores;
using BandMate.Domain.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BandMate.Domain.Persistence.Repositories
{
    public class SongRepository : ISongRepository
    {
        private readonly IDbContextFactory<BandMateContext> dbContextFactory;

        public SongRepository(IDbContextFactory<BandMateContext> dbContextFactory)
        {
            this.dbContextFactory = dbContextFactory;
        }

        public async Task<Song> GetAsync(int songID)
        {
            using var dbContext = await dbContextFactory.CreateDbContextAsync();

            return await dbContext.Songs
                .FirstOrDefaultAsync(s => songID == s.SongID);
        }

        public async Task<IEnumerable<Song>> GetByBandIDAsync(int bandID)
        {
            using var dbContext = await dbContextFactory.CreateDbContextAsync();

            return await dbContext.Songs
                .Where(s => bandID == s.BandID)
                .ToListAsync();
        }
    }
}
