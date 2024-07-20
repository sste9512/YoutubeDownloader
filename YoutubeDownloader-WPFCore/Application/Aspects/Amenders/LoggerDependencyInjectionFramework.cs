using Metalama.Extensions.DependencyInjection;
using Metalama.Extensions.DependencyInjection.Implementation;
using Metalama.Framework.Code;
using Metalama.Framework.Diagnostics;
using Microsoft.Extensions.Logging;

namespace YoutubeDownloader_WPFCore.Application.Aspects.Amenders;

public sealed class LoggerDependencyInjectionFramework : DefaultDependencyInjectionFramework
{
    public override bool CanHandleDependency(DependencyProperties properties, in ScopedDiagnosticSink diagnostics)
    {
        return properties.DependencyType.Is(typeof(ILogger));
    }

    protected override DefaultDependencyInjectionStrategy GetStrategy(DependencyProperties properties)
    {
        return new DefaultDependencyInjectionStrategy(properties);
    }
}

class InjectionStrategy : DefaultDependencyInjectionStrategy
{
    public InjectionStrategy(DependencyProperties properties) : base(properties)
    {
    }

    protected override IPullStrategy GetPullStrategy(IFieldOrProperty introducedFieldOrProperty)
    {
        return new LoggerPullStrategy(this.Properties, introducedFieldOrProperty);
    }
}

// Our customized pull strategy. Decides how to assign the field or property from the constructor.
sealed class LoggerPullStrategy : DefaultPullStrategy
{
    public LoggerPullStrategy(DependencyProperties properties, IFieldOrProperty introducedFieldOrProperty) : base(
        properties,
        introducedFieldOrProperty)
    {
        
    }
    
    

    // Returns the type of the required or created constructor parameter. We return ILogger<T> where T is the declaring type
    // (The default behavior would return just ILogger).
    protected override IType ParameterType
        => ((INamedType)TypeFactory.GetType(typeof(ILogger<>))).WithTypeArguments(this.IntroducedFieldOrProperty
            .DeclaringType);
}