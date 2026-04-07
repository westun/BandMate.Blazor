namespace BandMate.Blazor.Services.DAL
{
    public interface IGenericDalService<TEntity, TDto>
    where TEntity : class
    where TDto : class
    {
        Task<TDto?> GetByIdAsync(int id);
        Task<IEnumerable<TDto>> GetAllAsync();
        Task CreateAsync(TDto dto);
        Task UpdateAsync(TDto dto);
        Task DeleteAsync(int id);
    }
}
