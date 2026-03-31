using BandMate.Domain.Core.Models;
using BandMate.Domain.Core.Repositores;
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

        public async Task DeleteAsync(int id)
        {
            using var dbContext = await dbContextFactory.CreateDbContextAsync();
    
            var song = await dbContext.Songs
                .FirstOrDefaultAsync(s => id == s.SongID);
    
            if (song != null)
            {
                dbContext.Songs.Remove(song);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task<Song> CreateAsync(Song song)
        {
            using var dbContext = await dbContextFactory.CreateDbContextAsync();

            dbContext.Songs.Add(song);
            await dbContext.SaveChangesAsync();

            return song;
        }
    }
}
