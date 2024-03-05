using Autofac;

namespace YoutubeDownloader_WPFCore.Core.DependencyInjection;

public class TaskModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.Register(x => new CancellationTokenSource());
        base.Load(builder);
    }
}