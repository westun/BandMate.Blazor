using BandMate.Authentication.Data;
using BandMate.Blazor.Authentication;
using BandMate.Blazor.Components;
using BandMate.Blazor.Components.Account;
using BandMate.Blazor.Configuration;
using BandMate.Domain.Persistence;
using BandMate.Shared.Security;
using BandMate.Shared.Services;
using BandMate.Shared.Services.Api;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BandMate.Blazor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents()
                .AddInteractiveWebAssemblyComponents();

            builder.Services.AddCascadingAuthenticationState();
            builder.Services.AddScoped<IdentityRedirectManager>();
            builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

            builder.Services.AddAuthentication(options =>
                {
                    options.DefaultScheme = IdentityConstants.ApplicationScheme;
                    options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
                })
                .AddIdentityCookies();

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            if (builder.Environment.IsDevelopment())
            {
                builder.Services.AddDatabaseDeveloperPageExceptionFilter();
            }

            builder.Services.AddIdentityCore<ApplicationUser>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = false;
                    options.Stores.SchemaVersion = IdentitySchemaVersions.Version3;
                })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddSignInManager()
                .AddDefaultTokenProviders()
                .AddClaimsPrincipalFactory<AdditionalUserClaimsPrincipalFactory>();

            builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

            builder.Services.AddDbContextFactory<BandMateContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddDalRepositories();
            builder.Services.AddDalServices();

            builder.Services.AddAutoMapper(cfg => { }, typeof(Program));

            builder.Services.AddBlazorBootstrap();

            builder.Services.ConfigureMusicCatalog(builder);

            builder.Services.AddScoped<IUserService, UserService>();

            builder.Services.ConfigureBandStorage();

            builder.Services.AddScoped<NavStateService>();

            builder.Services.AddScoped<IFileUploadValidator, FileUploadValidator>();

            builder.Services.AddScoped<IAuthApiService, AuthApiService>();

            //builder.Services.AddHttpClient("BandMateApiHttpClient", client =>
            //{
            //    client.BaseAddress = new Uri("https://localhost:7236/");//builder.HostEnvironment.BaseAddress);
            //})
            //.ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
            //{
            //    // This allows the cookie to be sent with the request
            //    UseDefaultCredentials = true
            //});

            //builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("BandMateApiHttpClient"));

            builder.Services.AddScoped(sp =>
            {
                var navigationManager = sp.GetRequiredService<NavigationManager>();
                return new HttpClient
                {
                    BaseAddress = new Uri("https://localhost:7236/")//navigationManager.BaseUri)
                };
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error", createScopeForErrors: true);
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
            app.UseHttpsRedirection();

            app.UseAntiforgery();

            app.MapStaticAssets();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode()
                .AddInteractiveWebAssemblyRenderMode()
                .AddAdditionalAssemblies(typeof(BandMate.Client._Imports).Assembly);

            // Add additional endpoints required by the Identity /Account Razor components.
            app.MapAdditionalIdentityEndpoints();

            app.Run();
        }
    }
}
