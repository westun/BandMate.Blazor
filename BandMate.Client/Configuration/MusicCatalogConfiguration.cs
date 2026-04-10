using BandMate.MusicCatalog.Persistence;
using BandMate.Shared.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace BandMate.Client.Configuration
{
    public static class MusicCatalogConfiguration
    {
        public static IServiceCollection ConfigureMusicCatalog(this IServiceCollection services, WebAssemblyHostBuilder builder)
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
