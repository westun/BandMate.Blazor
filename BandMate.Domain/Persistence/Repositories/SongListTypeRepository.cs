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

        public async Task<SongListType> CreateAsync(SongListType songListType)
        {
            using var dbContext = await dbContextFactory.CreateDbContextAsync();

            dbContext.SongListTypes.Add(songListType);
            await dbContext.SaveChangesAsync();

            return songListType;
        }

        public async Task<SongListType> UpdateAsync(SongListType songListType)
        {
            using var dbContext = await dbContextFactory.CreateDbContextAsync();

            var songType = dbContext.SongListTypes.FirstOrDefault(s => s.SongListTypeID == songListType.SongListTypeID);
            if (songType == null)
            {
                throw new Exception("SongListType not found");
            }

            songType.SortOrder = songListType.SortOrder;
            songType.ListTypeName = songListType.ListTypeName;
            songType.BandID = songListType.BandID;

            await dbContext.SaveChangesAsync();

            return songType;
        }
    }
}
