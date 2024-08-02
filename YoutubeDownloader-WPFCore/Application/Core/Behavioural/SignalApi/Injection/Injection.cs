using Microsoft.Extensions.DependencyInjection;
using YoutubeDownloader_WPFCore.Application.Core.Behavioural.SignalApi.Creational;

namespace YoutubeDownloader_WPFCore.Application.Core.Behavioural.SignalApi.Injection;

public static class Injection
{
    public static IServiceCollection AddSignals(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<Creational.ChannelFactory>();
        serviceCollection.AddMemoryCache();
        serviceCollection.AddSingleton<SignalPool>();

        return serviceCollection;
    }
}