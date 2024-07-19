using System.Reactive.Subjects;
using System.Threading.Channels;

namespace YoutubeDownloader_WPFCore.Application.Core;

public interface ISignal<T>
{
    Task Signal(T value, CancellationToken cancellationToken);
    void OnSignal(Action<T> handler);
}


public class Signaler<T> : ISignal<T> 
{
    private readonly Subject<T> _subject;
    private readonly Channel<T> _channel;
    private List<WeakReference<Action<T>?>> _actions;

    public Signaler(ChannelFactory channelFactory)
    {
        _subject = new Subject<T>();
        _channel = channelFactory.MakeOrCreate<T>();
        _actions = new List<WeakReference<Action<T>?>>();
    }
    
    public Signaler()
    {
        
    }
    
    public void Dispose()
    {
         _channel.Writer.Complete();
    }

    public async Task Signal(T value, CancellationToken cancellationToken)
    {
        await _channel.Writer.WriteAsync(value, cancellationToken);
        _subject.OnNext(await _channel.Reader.ReadAsync(cancellationToken));
    }

    public void OnSignal(Action<T> handler)
    {
        var weak = new WeakReference<Action<T>?>(handler);
        _actions.Add(weak);
        if (weak.TryGetTarget(out Action<T>? action))
        {
            _subject.Subscribe(action);
        }
    }
}