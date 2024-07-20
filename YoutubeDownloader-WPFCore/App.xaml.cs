using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text.Json;
using System.Threading.Channels;
using System.Windows;
using System.Windows.Controls;
using Config.Net;
using LiteDB;
using MassTransit;
using MassTransit.SagaStateMachine;
using MassTransit.SignalR;
using MediatR;
using Metalama.Extensions.DependencyInjection.ServiceLocator;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.CircuitBreaker;
using Polly.Timeout;
using YoutubeDownloader_WPFCore.Application;
using YoutubeDownloader_WPFCore.Application.Aspects.TypeAspects;
using YoutubeDownloader_WPFCore.Application.Configuration;
using YoutubeDownloader_WPFCore.Application.Controls.PlayList.Commands;
using YoutubeDownloader_WPFCore.Application.Controls.PlayList.View;
using YoutubeDownloader_WPFCore.Application.Core.Behavioural.CQRS.Pipelines;
using YoutubeDownloader_WPFCore.Application.Interfaces;
using YoutubeDownloader_WPFCore.Infrastructure;
using YoutubeDownloader_WPFCore.Infrastructure.Data.Services;
using YoutubeDownloader_WPFCore.Infrastructure.Stores;
using YoutubeExplode;
using MemoryCache = Microsoft.Extensions.Caching.Memory.MemoryCache;

namespace YoutubeDownloader_WPFCore;

public class ControlStore
{
    public IMemoryCache _memoryCache;

    public ControlStore(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public void Register<T>(T instance)
    {
        var key = typeof(T) + @"\Views";
        if (_memoryCache.TryGetValue(key, out List<T> instances))
        {
            instances.Add(instance);
            _memoryCache.Set(key, instances);
        }

        _memoryCache.Set(key, new List<T>()
        {
            instance
        });
    }

    public IEnumerable<T> FindByType<T>()
    {
        var key = typeof(T) + @"\Views";
        if(_memoryCache.TryGetValue(key, out List<T> instances))
        {
            return instances;
        }
        return Enumerable.Empty<T>();
    }


    public void Remove<T>(T instance)
    {
         var key = typeof(T) + @"\Views";
         if (_memoryCache.TryGetValue(key, out List<T> instances))
         {
             if (instances.Contains(instance))
             {
                 instances.Remove(instance);
             }
             _memoryCache.Set(key, instances);
         }
    }
}

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
                services.AddHttpClient("youtubeclient")
                    .AddHttpMessageHandler<YoutubeMessageHandler>();
                services.AddKeyedScoped<HttpClient>("youtubeclient",
                    (services, factory) => new HttpClient(services.GetRequiredService<YoutubeMessageHandler>()));

                services.AddLogging();
                services.AddScoped<YoutubeClient>(x =>
                {
                    var factory = x.GetRequiredService<IHttpClientFactory>();
                    return new YoutubeClient(factory.CreateClient("youtubeclient"));
                });

                services.AddScoped<IFileSystemConfiguration>(x => new ConfigurationBuilder<IFileSystemConfiguration>()
                    .UseJsonFile("/filesystemsettings.json")
                    .Build());
                services.AddSingleton<Dictionary<int, object>>(x => new Dictionary<int, object>());
                services.AddScoped<LiteDatabase>(x => new LiteDatabase(@"Persistence.db"));
                services.AddScoped<CancellationTokenSource>(x => new CancellationTokenSource());
                services.AddScoped<IDocumentStore, DocumentStore>();
                services.AddScoped<GoogleDriveService>();
                services.AddScoped<IUser, User>();
                services.AddScoped<ChannelFactory>();
                services.AddMemoryCache();
                services.AddSignalRCore();
                services.AddScoped<YoutubeMessageHandler>();
                services.AddScoped<ImageStore>();


                services.AddResiliencePipeline("youtube-download-strategy", pipelineBuilder =>
                {
                    pipelineBuilder.AddRetry(new()
                        {
                            MaxRetryAttempts = 3,
                            Delay = TimeSpan.FromSeconds(3),
                            ShouldHandle = args => args.Outcome switch
                            {
                                { Exception: HttpRequestException } => PredicateResult.True(),
                                { Exception: TimeoutRejectedException } => PredicateResult
                                    .True(), // You can handle multiple exceptions
                                { Result: HttpResponseMessage response } when !response.IsSuccessStatusCode =>
                                    PredicateResult.True(),
                                _ => PredicateResult.False()
                            }
                        })
                        .AddTimeout(TimeSpan.FromSeconds(10));
                });


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