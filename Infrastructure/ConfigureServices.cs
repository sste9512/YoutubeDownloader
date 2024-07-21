using System.Reflection;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Infrastructure.Files;
using CleanArchitecture.Infrastructure.Identity;
using CleanArchitecture.Infrastructure.Persistence;
using CleanArchitecture.Infrastructure.Persistence.Data.Services;
using CleanArchitecture.Infrastructure.Persistence.Interceptors;
using CleanArchitecture.Infrastructure.Persistence.Network;
using CleanArchitecture.Infrastructure.Persistence.Stores;
using CleanArchitecture.Infrastructure.Services;
using Config.Net;
using LiteDB;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Client;
using YoutubeDownloader_WPFCore.Application;
using YoutubeDownloader_WPFCore.Application.Configuration;
using YoutubeExplode;

namespace CleanArchitecture.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {

        services.AddLogging();

        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddScoped<AuditableEntitySaveChangesInterceptor>();

        /*if (configuration.GetValue<bool>("UseInMemoryDatabase"))
        {*/
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlite(connectionString,
                builder => builder.MigrationsAssembly(Assembly.GetAssembly(typeof(ApplicationDbContext))?.FullName)));

        //options.UseInMemoryDatabase("CleanArchitectureDb"));
        //}
        /*else
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
        }*/

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<ApplicationDbContextInitialiser>();

        services.AddTransient<IEmailSender<ApplicationUser>, MyEmailSender>();


        /*services
            .AddDefaultIdentity<ApplicationUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();*/
        //.AddDefaultTokenProviders();

        /*services.AddIdentityServer()
            .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();*/

        services.AddTransient<IDateTime, DateTimeService>();
        services.AddTransient<IIdentityService, IdentityService>();
        services.AddTransient<ICsvFileBuilder, CsvFileBuilder>();

        /*services.AddAuthentication()
            .AddIdentityServerJwt();*/

        /*services.AddAuthorization(options =>
            options.AddPolicy("CanPurge", policy => policy.RequireRole("Administrator")));*/


        services.AddAuthentication()
            .AddBearerToken(IdentityConstants.BearerScheme);

        services.AddAuthorizationBuilder();

        services
            .AddIdentityCore<ApplicationUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddApiEndpoints();
/*#else
        services
            .AddDefaultIdentity<ApplicationUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();
#endif*/

        services.AddSingleton(TimeProvider.System);
        services.AddTransient<IIdentityService, IdentityService>();



        // Add Http Configurations Here
        services.AddHttpClient(nameof(YoutubeClient))
            .AddHttpMessageHandler<YoutubeMessageHandler>();
        services.AddKeyedScoped<HttpClient>(nameof(YoutubeClient),
            (services, factory) => new HttpClient(services.GetRequiredService<YoutubeMessageHandler>()));
        services.AddScoped<YoutubeClient>(x =>
        {
            var factory = x.GetRequiredService<IHttpClientFactory>();
            return new YoutubeClient(factory.CreateClient(nameof(YoutubeClient)));
        });

        services.AddScoped<IFileSystemConfiguration>(x => new ConfigurationBuilder<IFileSystemConfiguration>()
            .UseJsonFile("/filesystemsettings.json")
            .Build());
       
        services.AddScoped<LiteDatabase>(x => new LiteDatabase(@"Persistence.db"));
        services.AddScoped<IDocumentStore, DocumentStore>();
        // Move this to Application Services
        services.AddScoped<CancellationTokenSource>(x => new CancellationTokenSource());
        services.AddScoped<GoogleDriveService>();
        services.AddScoped<ChannelFactory>();
        services.AddMemoryCache();
        services.AddScoped<YoutubeMessageHandler>();
        services.AddScoped<ImageStore>();


        /*ervices.AddAuthorization(options =>
            options.AddPolicy(Policies.CanPurge, policy => policy.RequireRole(Roles.Administrator)));*/
        return services;
    }
}

public class MyEmailSender : IEmailSender<ApplicationUser>
{
    public Task SendConfirmationLinkAsync(ApplicationUser user, string email, string confirmationLink)
    {
        throw new NotImplementedException();
    }

    public Task SendPasswordResetLinkAsync(ApplicationUser user, string email, string resetLink)
    {
        throw new NotImplementedException();
    }

    public Task SendPasswordResetCodeAsync(ApplicationUser user, string email, string resetCode)
    {
        throw new NotImplementedException();
    }
}