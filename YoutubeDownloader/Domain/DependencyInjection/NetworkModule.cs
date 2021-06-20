using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using Newtonsoft.Json;
using YoutubeExplode;

namespace YoutubeDownloader.Domain.DependencyInjection
{
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
            Console.Out.WriteLine(JsonConvert.SerializeObject(request));
            return base.SendAsync(request, cancellationToken);
        }
    }
}