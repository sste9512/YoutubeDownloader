using System.Threading.Channels;

namespace YoutubeDownloader_WPFCore.Application.Core;

public sealed class DefaultBackgroundTaskQueue(Channel<Func<CancellationToken, ValueTask>> queue) : IBackgroundTaskQueue
{
    
    
    
    public async ValueTask QueueBackgroundWorkItemAsync(
        Func<CancellationToken, ValueTask> workItem)
    {
        ArgumentNullException.ThrowIfNull(workItem);

        await queue.Writer.WriteAsync(workItem);
    }

    public async ValueTask<Func<CancellationToken, ValueTask>> DequeueAsync( 
        CancellationToken cancellationToken)
    {
        Func<CancellationToken, ValueTask>? workItem =
            await queue.Reader.ReadAsync(cancellationToken);

        return workItem;
    }
}