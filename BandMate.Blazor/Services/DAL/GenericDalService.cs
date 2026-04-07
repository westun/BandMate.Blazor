using BandMate.Domain.Core.Repositores;

namespace BandMate.Blazor.Services.DAL
{
    using AutoMapper;

    public class GenericDalService<TEntity, TDto> : IGenericDalService<TEntity, TDto>
        where TEntity : class
        where TDto : class
    {
        private readonly IGenericDalRepository<TEntity> _repository;
        private readonly IMapper _mapper;

        public GenericDalService(IGenericDalRepository<TEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<TDto?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return _mapper.Map<TDto>(entity); // AutoMapper 16.x conversion
        }

        public async Task<IEnumerable<TDto>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<TDto>>(entities);
        }

        public async Task CreateAsync(TDto dto)
        {
            var entity = _mapper.Map<TEntity>(dto);
            await _repository.AddAsync(entity);
        }

        public async Task UpdateAsync(TDto dto)
        {
            var entity = _mapper.Map<TEntity>(dto);
            await _repository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null)
            {
                await _repository.RemoveAsync(entity);
            }
        }
    }
}