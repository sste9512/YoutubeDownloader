using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Microsoft.Extensions.Logging;

namespace YoutubeDownloaderMaui.Core.Aspects.TypeAspects;

public class LogMethodsAspect : TypeAspect
{

    [Meta.IntroduceDependency] private readonly ILogger _logger;
    
    public override void BuildAspect(IAspectBuilder<INamedType> builder)
    {
        
        base.BuildAspect(builder);
    }
}