using BandMate.Client.Configuration;
using BandMate.Domain.Persistence;
using BandMate.Shared.Security;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.EntityFrameworkCore;

namespace BandMate.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            //var builder = WebAssemblyHostBuilder.CreateDefault(args);

            //builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            //builder.Services.AddScoped(sp =>
            //{
            //    var navigationManager = sp.GetRequiredService<NavigationManager>();
            //    return new HttpClient
            //    {
            //        BaseAddress = new Uri("https://localhost:7236/")//navigationManager.BaseUri)
            //    };
            //});

            //builder.Services.AddDbContextFactory<BandMateContext>(options =>
            //    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            //builder.Services.AddDalRepositories();
            //builder.Services.AddApiServices();

            //builder.Services.ConfigureBandStorage();

            //builder.Services.ConfigureMusicCatalog(builder);

            //builder.Services.AddScoped<IFileUploadValidator, FileUploadValidator>();

            //builder.Services.AddAutoMapper(cfg => { }, typeof(Program));

            //await builder.Build().RunAsync();
        }
    }
}
