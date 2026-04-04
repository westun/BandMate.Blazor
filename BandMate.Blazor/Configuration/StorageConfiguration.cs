using BandMate.Blazor.Services;

namespace BandMate.Blazor.Configuration
{
    public static class StorageConfiguration
    {
        public static IServiceCollection ConfigureBandStorage(this IServiceCollection services)
        {
            services.AddScoped<IBlobStorageService, BlobStorageService>();
            services.AddScoped<IBandResourceManager, BandResourceManager>();

            return services;
        }
    }
}
