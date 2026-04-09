using BandMate.MusicCatalog.Persistence;
using BandMate.Shared.Services;

namespace BandMate.Blazor.Configuration
{
    public static class MusicCatalogConfiguration
    {
        public static IServiceCollection ConfigureMusicCatalog(this IServiceCollection services, WebApplicationBuilder builder)
        {
            services.AddTransient<IArtistRepository, SpotifyArtistRepository>();
            services.AddTransient<SpotifySettings>(serviceProvider =>
            {
                return new SpotifySettings
                {
                    ClientId = builder.Configuration.GetValue<string>("Spotify:ClientId"),
                    ClientSecret = builder.Configuration.GetValue<string>("Spotify:ClientSecret"),
                };
            });

            services.AddTransient<IMusicCatalogService, MusicCatalogService>();

            return services;
        }
    }
}
