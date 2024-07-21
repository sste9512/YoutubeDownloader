using System.Threading.Channels;
using Microsoft.Extensions.Caching.Memory;

namespace YoutubeDownloader_WPFCore.Application;

public sealed class ChannelFactory(IMemoryCache memoryCache)
{
    public Channel<T> MakeOrCreate<T>() 
    {
        if (memoryCache.TryGetValue("Channel" + typeof(T), out Channel<T> channel))
        {
            return channel;
        }

        var entry = memoryCache.CreateEntry("Channel" + typeof(T));
        var freshChannel = Channel.CreateBounded<T>(new BoundedChannelOptions(100)
        {
            AllowSynchronousContinuations = false,
            SingleReader = false,
            SingleWriter = false,
            FullMode = BoundedChannelFullMode.DropOldest
        });
        entry.Value = freshChannel;
        return freshChannel;
    }
}