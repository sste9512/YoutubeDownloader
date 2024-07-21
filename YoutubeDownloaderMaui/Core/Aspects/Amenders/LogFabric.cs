using Metalama.Extensions.DependencyInjection;
using Metalama.Framework.Fabrics;

namespace YoutubeDownloaderMaui.Core.Aspects.Amenders;

public class LogFabric : ProjectFabric
{
    public override void AmendProject(IProjectAmender amender)
    {
        amender.Outbound.ConfigureDependencyInjection(dependencyInjection =>
            dependencyInjection.RegisterFramework<Metalama.Extensions.DependencyInjection.Implementation.LoggerDependencyInjectionFramework>());
        
    }
}