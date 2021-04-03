using Autofac;
using YoutubeExplode;

namespace YoutubeDownloader.DependencyInjection
{
    public class NetworkModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(x => new YoutubeClient());
            base.Load(builder);
        }
    }
}