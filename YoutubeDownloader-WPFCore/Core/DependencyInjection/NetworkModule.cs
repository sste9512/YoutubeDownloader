using System.Net.Http;
using System.Text.Json;
using Autofac;
using YoutubeExplode;

namespace YoutubeDownloader_WPFCore.Core.DependencyInjection;

public class NetworkModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        var client = new HttpClient(new YoutubeDelegate());
        builder.Register(x => new YoutubeClient(client));
        base.Load(builder);
    }
}

public class YoutubeDelegate : DelegatingHandler
{
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        Console.Out.WriteLine(JsonSerializer.Serialize(request));
        return base.SendAsync(request, cancellationToken);
    }
}