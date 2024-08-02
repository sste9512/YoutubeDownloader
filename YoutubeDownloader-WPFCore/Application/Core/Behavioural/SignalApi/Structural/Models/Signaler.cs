using System.Reactive.Subjects;
using System.Threading.Channels;
using Metalama.Extensions.DependencyInjection;

// ReSharper disable RedundantAssignment

namespace YoutubeDownloader_WPFCore.Application.Core.Behavioural.SignalApi.Structural.Models;

// TODO: Create Signal Builder that chains off of implicit operators

public class Signaler<T> : ISignal<T>
{
    public string Id { get; set; }
    public string OwnerId { get; init; }

    [Dependency] private readonly Creational.ChannelFactory _channelFactory;

    private readonly Subject<T> _subject;
    private Channel<T> _channel;
    private List<WeakReference<Action<T>?>> _actions;

    public Signaler()
    {
        _subject = new Subject<T>();
        _actions = new List<WeakReference<Action<T>?>>();
    }


    public void Dispose()
    {
        _channel.Writer.Complete();
        _subject.Dispose();
    }

    /// <summary>
    ///   Method for emitting values and events from appropriate channels
    /// </summary>
    /// <param name="id"></param>
    /// <param name="value"></param>
    /// <param name="cancellationToken"></param>
    public async Task Signal(string id, T value, CancellationToken cancellationToken)
    {
        if (_channel is null)
        {
            Id = id;
            _channel = _channelFactory.GetOrCreate<T>(id);
        }

        await _channel.Writer.WriteAsync(value, cancellationToken);
        // Events are wired here at the moment a channel is written to, ensuring we always get the most recent one off of the queue
        _subject.OnNext(await _channel.Reader.ReadAsync(cancellationToken));
    }

    /// <summary>
    /// Method for listening to subscribed actions
    /// </summary>
    /// <param name="handler"></param>
    public void OnSignal(Action<T> handler)
    {
        var weak = new WeakReference<Action<T>?>(handler);
        _actions.Add(weak);
        if (weak.TryGetTarget(out Action<T>? action))
        {
            _subject.Subscribe(handler);
        }
    }

  

    public static Signaler<T> operator >> (Signaler<T> signaler, Action<T> action)
    {
        signaler.OnSignal(action);
        return signaler;
    }
}