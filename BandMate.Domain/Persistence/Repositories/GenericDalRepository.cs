using BandMate.Domain.Core.Repositores;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BandMate.Domain.Persistence.Repositories
{
    public class GenericDalRepository<T> : IGenericDalRepository<T> where T : class
    {
        protected readonly BandMateContext _context;
        protected readonly DbSet<T> _dbSet;

        public GenericDalRepository(BandMateContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate).ToList();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            // EF Core 10.0 tracks changes automatically if the entity is attached.
            // For detached entities, use Update() or Entry().CurrentValues.SetValues()
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(T entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
