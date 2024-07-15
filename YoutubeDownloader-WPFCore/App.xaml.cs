
using System.Reflection;
using System.Text.Json;
using System.Threading.Channels;
using System.Windows;

using LiteDB;
using MediatR;
using Metalama.Extensions.DependencyInjection.ServiceLocator;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using YoutubeDownloader_WPFCore.Application.Core.Behavioural.CQRS.Pipelines;
using YoutubeDownloader_WPFCore.Application.Interfaces;
using YoutubeDownloader_WPFCore.Infrastructure;
using YoutubeDownloader_WPFCore.Infrastructure.Data.Services;
using YoutubeDownloader_WPFCore.Infrastructure.Stores;
using YoutubeExplode;

namespace YoutubeDownloader_WPFCore;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App 
{
    protected override void OnStartup(StartupEventArgs e)
    {
        var builder = Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                config.AddJsonFile("appsettings.json", true, true);
                config.AddEnvironmentVariables();
            })
            .ConfigureLogging(log =>
            {
                log.AddJsonConsole(options =>
                {
                    options.IncludeScopes = true;
                    options.TimestampFormat = "HH:mm:ss ";
                    options.JsonWriterOptions = new JsonWriterOptions
                    {
                        Indented = true,
                    };
                });
            })
            .ConfigureServices((hostContext, services) =>
            {
                services.AddLogging();
                services.AddScoped<YoutubeClient>();
                services.AddSingleton<Channel<Exception>>(x =>
                {
                    BoundedChannelOptions options = new(100)
                    {
                        FullMode = BoundedChannelFullMode.Wait
                    };
                    return Channel.CreateBounded<Exception>(options);
                });
                services.AddScoped<LiteDatabase>(x => new LiteDatabase(@"Persistence.db"));
                services.AddScoped<CancellationTokenSource>(x => new CancellationTokenSource());
                services.AddScoped<IDocumentStore, DocumentStore>();
                services.AddScoped<GoogleDriveService>();
                services.AddScoped<IUser, User>();
                services.AddMediatR(cfg =>
                {
                    cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                    cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
                    cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));
                    cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehaviour<,>));
                    cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));
                    cfg.AddBehavior(typeof(IStreamPipelineBehavior<,>), typeof(GenericStreamPipelineBehavior<,>));
                });
                //services.AddMemoryCache();
                services.AddSingleton<Channel<Func<CancellationToken, ValueTask>>>(x =>
                {
                    BoundedChannelOptions options = new(100)
                    {
                        FullMode = BoundedChannelFullMode.Wait
                    };
                    return Channel.CreateBounded<Func<CancellationToken, ValueTask>>(options);
                });
                
            });

        var application = builder.Build();

        ServiceProviderProvider.ServiceProvider = () => application.Services;
        
        
        base.OnStartup(e);
    }
}