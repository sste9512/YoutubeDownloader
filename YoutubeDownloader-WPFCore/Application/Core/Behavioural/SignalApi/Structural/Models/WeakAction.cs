namespace YoutubeDownloader_WPFCore.Application.Core.Behavioural.SignalApi.Structural.Models;

public class WeakAction<T>
{
    public string CorrelationalId { get; set; }

    private WeakReference<Action<T>> _weakReference;

    public WeakAction(Action<T> referrent)
    {
        _weakReference = new WeakReference<Action<T>>(referrent);
    }

    private Action<T> Operation()
    {
        return _weakReference.TryGetTarget(out Action<T> target) ? target : null;
    }

    private WeakAction<T> Create(Action<T> action)
    {
        return new WeakAction<T>(action);
    }

    /// <summary>
    ///  Auto Create Weak Reference from Action
    /// </summary>
    /// <param name="action"></param>
    /// <returns></returns>
    public static implicit operator WeakAction<T>(Action<T> action)
    {
        return new WeakAction<T>(action);
    }

    /// <summary>
    ///  Auto Extrapolate Action from weak reference
    /// </summary>
    /// <returns></returns>
    public static implicit operator Action<T>(WeakAction<T> weakAction)
    {
        return weakAction.Operation();
    }
}