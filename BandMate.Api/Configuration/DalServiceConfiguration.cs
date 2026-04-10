using BandMate.Shared.Services.DAL;

namespace BandMate.Api.Configuration
{
    public static class DalServiceConfiguration
    {
        public static IServiceCollection AddDalServices(this IServiceCollection services)
        {
            services.AddTransient<ISongService, SongService>();
            services.AddTransient<ISongListTypeService, SongListTypeService>();
            services.AddTransient<IBandService, BandService>();
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IBandAccountService, BandAccountService>();
            services.AddTransient<IBandNameService, BandNameService>();

            services.AddScoped(typeof(IGenericDalService<,>), typeof(GenericDalService<,>));

            return services;
        }
    }
}
