using System.Threading;
using Autofac;

namespace YoutubeDownloader.Domain.DependencyInjection
{
    public class TaskModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(x => new CancellationTokenSource());
            base.Load(builder);
        }
    }
}