using Prism.Events;

namespace YoutubeDownloader_WPFCore.Core.Structural.Events;

public sealed class VideoSearchErrorEvent : PubSubEvent<Exception>
{
    
}

