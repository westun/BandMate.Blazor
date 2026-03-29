using BandMate.Blazor.Authentication;
using BandMate.Blazor.Components;
using BandMate.Blazor.Components.Account;
using BandMate.Blazor.Configuration;
using BandMate.Blazor.Data;
using BandMate.Blazor.Middleware;
using BandMate.Domain.Persistence;
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
                .AddInteractiveServerComponents();

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
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddIdentityCore<ApplicationUser>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = true;
                    options.Stores.SchemaVersion = IdentitySchemaVersions.Version3;
                })
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

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
            app.UseHttpsRedirection();

            app.UseAntiforgery();

            app.MapStaticAssets();
            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            // Add additional endpoints required by the Identity /Account Razor components.
            app.MapAdditionalIdentityEndpoints();

            // 2. Only run the EmailCheckMiddleware on actual page requests
            app.UseWhen(context =>
                // Only run if it's a GET request (standard page load)
                context.Request.Method == HttpMethods.Get &&
                // Ignore internal Blazor SignalR and framework paths
                !context.Request.Path.StartsWithSegments("/_blazor") &&
                !context.Request.Path.StartsWithSegments("/_framework") &&
                !context.Request.Path.StartsWithSegments("/account") &&
                // Ignore common static file extensions
                !Path.HasExtension(context.Request.Path.Value),
                appBuilder =>
                {
                    appBuilder.UseMiddleware<BandIdInRouteCheckMiddleware>();
                });

            app.Run();
        }
    }
}
