using Metalama.Extensions.DependencyInjection;
using YoutubeDownloader_WPFCore.Application.Core.Behavioural.SignalApi.Creational;
using YoutubeDownloader_WPFCore.Application.Core.Behavioural.SignalApi.Structural.Models;

namespace YoutubeDownloader_WPFCore.Application.Core.Behavioural.SignalApi.Structural.Identification;

/// <summary>
///  TODO : Create Stepwise Builder out of this
/// </summary>
public class IdRef<T>
{
    [Dependency]  private readonly SignalPool _signalPool;

    private Signaler<T1> FindById<T1>(string id)
    {
        return _signalPool.FindById<T1>(id);
    }

    public string Id { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="idRef"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static bool operator <<(IdRef<T> idRef, T value)
    {
        var sig = idRef.FindById<T>(idRef.Id);
        sig.Signal(sig.Id, value, CancellationToken.None).ConfigureAwait(false);
        return true;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="idRef"></param>
    /// <param name="action"></param>
    /// <returns></returns>
    public static Signaler<T> operator >> (IdRef<T> idRef, Action<T> action)
    {
        var weak = new WeakAction<T>(action);
        var sig = idRef.FindById<T>(idRef.Id);
        sig.OnSignal(weak);
        return sig;
    }

    public static explicit operator IdRef<T>(string s)
    {
        return new IdRef<T>()
        {
            Id = s
        };
    }

    public static explicit operator IdRef<T>(int s)
    {
        return new IdRef<T>()
        {
            Id = s.ToString()
        };
    }
}