using BandMate.Blazor.Services;

namespace BandMate.Blazor.Configuration
{
    public static class DalServiceConfiguration
    {
        public static IServiceCollection AddDalServices(this IServiceCollection services)
        {
            services.AddTransient<ISongService, SongService>();
            
            return services;
        }
    }
}
