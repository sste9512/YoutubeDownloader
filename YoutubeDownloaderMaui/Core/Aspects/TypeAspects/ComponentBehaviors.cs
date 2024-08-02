using MediatR;
using Metalama.Extensions.DependencyInjection;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;

namespace YoutubeDownloaderMaui.Core.Aspects.TypeAspects;

public sealed class ComponentBehaviors : TypeAspect
{
   
    [IntroduceDependency(IsLazy = true)] private readonly ISender _sender;

    [IntroduceDependency(IsLazy = true)] private readonly IPublisher _publisher;

    [IntroduceDependency(IsLazy = true)] private readonly CancellationTokenSource _cancellationTokenSource;

    [IntroduceDependency] private readonly IServiceProvider _serviceProvider;

    /*[Introduce] public Guid Id { get; } = Guid.NewGuid();*/
    [Introduce] public int ParentId { get; set; }


    public override void BuildAspect(IAspectBuilder<INamedType> builder)
    {
    }


    [Introduce(Name = "Launch")]
    public async Task Launch(Action action)
    {
        /*await Task.Factory.StartNew(x => { action.Invoke(); }, _cancellationTokenSource.Token,
            _cancellationTokenSource.Token);*/
    }


    [Introduce(Name = "Send")]
    public async Task<T> Send<T>(IRequest<T> request)
    {
        return await _sender.Send(request, _cancellationTokenSource.Token);
    }

    [Introduce(Name = "CreateStream")]
    public IAsyncEnumerable<TResponse> CreateStream<TResponse>(IStreamRequest<TResponse> request,
        CancellationToken cancellationToken)
    {
        return _sender.CreateStream(request, cancellationToken);
    }


    [Introduce(Name = "Navigate")]
    public void Navigate<T>() where T : Window
    {
        /*var form = Inject<T>();
        ParentId = form.GetHashCode();
        form.Show();*/
    }

    [Introduce(Name = "LaunchWithResult")]
    public async Task<T> LaunchWithResult<T>(Func<T> func)
    {
        return await Task.Factory.StartNew(x => func.Invoke(), _cancellationTokenSource.Token);
    }

    [Introduce(Name = "RaiseEvent")]
    public async Task RaiseEvent(object @event)
    {
        await _publisher.Publish(@event, _cancellationTokenSource.Token);
    }

    [Introduce(Name = "CancelAll")]
    public async Task CancelAll()
    {
        await _cancellationTokenSource.CancelAsync();
    }

    [Introduce(Name = "Inject")]
    public T Inject<T>() where T : class
    {
        return _serviceProvider.GetRequiredService<T>();
    }


    [Introduce(Name = "Publish")]
    public async Task Publish<TNotification>(TNotification notification) where TNotification : INotification
    {
        await _publisher.Publish(notification, _cancellationTokenSource.Token);
    }
}