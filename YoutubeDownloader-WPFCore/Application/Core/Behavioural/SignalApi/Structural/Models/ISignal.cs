namespace YoutubeDownloader_WPFCore.Application.Core.Behavioural.SignalApi.Structural.Models;

public interface ISignal<T> : IDisposable
{
    Task Signal(string id, T value, CancellationToken cancellationToken);
    void OnSignal(Action<T> handler);
}