using BandMate.Domain.Core.Repositores;
using BandMate.Domain.Persistence.Repositories;

namespace BandMate.Blazor.Configuration
{
    public static class DalRepositoryConfiguration
    {
        public static IServiceCollection AddDalRepositories(this IServiceCollection services)
        {
            services.AddTransient<ISongRepository, SongRepository>();

            return services;
        }
    }
}
