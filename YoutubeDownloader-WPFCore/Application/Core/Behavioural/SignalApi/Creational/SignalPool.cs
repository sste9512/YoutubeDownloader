using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using YoutubeDownloader_WPFCore.Application.Core.Behavioural.SignalApi.Structural.Models;

namespace YoutubeDownloader_WPFCore.Application.Core.Behavioural.SignalApi.Creational;

/// <summary>
/// Tracking factory for signals
/// </summary>
public class SignalPool
{
    /// <summary>
    ///  Actual Pool for storing signals
    ///  Pool
    /// </summary>
    private readonly ConcurrentDictionary<string, object> _pool = new();
    

    /// <summary>
    /// Central call to query signal information, news a new signal if the id could not be matched
    /// </summary>
    /// <param name="id"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public Signaler<T> FindById<T>(string id)
    {
        var key = CreateKey<T>(id);
        if (!_pool.TryGetValue(key, out var obj)) return Make<T>(id);
        var signaler = Unsafe.As<Signaler<T>>(obj);
        return signaler;
    }


    /// <summary>
    /// Creational for casting proper types 
    /// </summary>
    /// <param name="id"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    private Signaler<T> Make<T>(string id)
    {
        var key = CreateKey<T>(id);
        var signaler = new Signaler<T>
        {
            Id = key
        };
        _pool.TryAdd(key, signaler);
        return signaler;
    }

    private string CreateKey<T>(string id)
    {
        return typeof(T) + id;
    }

 
}
    