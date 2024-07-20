using Metalama.Extensions.DependencyInjection;
using Metalama.Framework.Fabrics;

namespace YoutubeDownloader_WPFCore.Application.Aspects.Amenders;

public class LogFabric : ProjectFabric
{
    public override void AmendProject(IProjectAmender amender)
    {
        amender.ConfigureDependencyInjection(dependencyInjection =>
            dependencyInjection.RegisterFramework<Metalama.Extensions.DependencyInjection.Implementation.LoggerDependencyInjectionFramework>());
        
    }
}