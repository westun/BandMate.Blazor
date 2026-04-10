using BandMate.Domain.Core.Repositores;
using BandMate.Domain.Persistence.Repositories;

namespace BandMate.Api.Configuration
{
    public static class DalRepositoryConfiguration
    {
        public static IServiceCollection AddDalRepositories(this IServiceCollection services)
        {
            services.AddTransient<ISongRepository, SongRepository>();
            services.AddTransient<ISongListTypeRepository, SongListTypeRepository>();
            services.AddTransient<IBandRepository, BandRepository>();
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<IBandAccountRepository, BandAccountRepository>();
            services.AddTransient<IBandNameRepository, BandNameRepository>();

            services.AddScoped(typeof(IGenericDalRepository<>), typeof(GenericDalRepository<>));

            return services;
        }
    }
}
