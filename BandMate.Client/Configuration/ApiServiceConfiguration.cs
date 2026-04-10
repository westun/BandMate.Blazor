using BandMate.Shared.Services.Api;
using BandMate.Shared.Services.DAL;

namespace BandMate.Client.Configuration
{
    public static class ApiServiceConfiguration
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
            services.AddTransient<ISongService, SongApiService>();

            return services;
        }
    }
}
