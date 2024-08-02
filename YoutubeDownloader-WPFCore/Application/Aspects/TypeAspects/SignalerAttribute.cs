using Metalama.Extensions.DependencyInjection;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using YoutubeDownloader_WPFCore.Application.Core.Behavioural.SignalApi.Creational;
using YoutubeDownloader_WPFCore.Application.Core.Behavioural.SignalApi.Structural.Identification;
using YoutubeDownloader_WPFCore.Application.Core.Behavioural.SignalApi.Structural.Navigation;
using OneOf;

namespace YoutubeDownloader_WPFCore.Application.Aspects.TypeAspects;

internal class SignalerAttribute : TypeAspect
{
  
    [IntroduceDependency] private Graph _graph;
    [IntroduceDependency] private SignalPool _signalPool;


    public override void BuildAspect(IAspectBuilder<INamedType> builder)
    {
        foreach (var property in builder.Target.FieldsAndProperties.Where(p =>
                     !p.IsImplicitlyDeclared && p.IsAutoPropertyOrField == true))
        {
            builder.Advice.Override(property, nameof(this.OverrideProperty));
            /*builder.Advice.IntroduceUnaryOperator(property.DeclaringType, null, null, null, OperatorKind.LeftShift,
                OverrideStrategy.Default, null, null, null);*/
        }
    }
    
    

    [Template]
    private dynamic? OverrideProperty
    {
        get => meta.Proceed();

        set
        {
            /*_graph.InvokeEvent(meta.This.GetHashCode().ToString(), meta.This, new GraphEventArgs()
            {
                 ArgValue = value
            });*/
            meta.Proceed();
        }
    }

    [Introduce(Name = "Signal")]
    public async Task Signal<T>(string id, T value, CancellationToken cancellationToken)
    {
        var sig = _signalPool.FindById<T>(id);
        await sig.Signal(id, value, cancellationToken);
    }
    
    [Introduce(Name = "Signal")]
    public async Task Signal<T>(string id, T value)
    {
        var sig = _signalPool.FindById<T>(id);
        await sig.Signal(id, value, CancellationToken.None);
    }
    
    [Introduce(Name = "Send")]
    public IdRef<T> Send<T>(OneOf<string,  Guid, int, object> id)
    {
        var pendingId = string.Empty;
        id.Switch(x =>
        {
            pendingId = x;
        }, guid =>
        {
            pendingId = guid.ToString();
        }, i =>
        {
            pendingId = i.ToString();
        }, o =>
        {
            pendingId = o.GetHashCode().ToString();
        });
        
        return new IdRef<T>
        {
            Id = pendingId
        };
    }
}