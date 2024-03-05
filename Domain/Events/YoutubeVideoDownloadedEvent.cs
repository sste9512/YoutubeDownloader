namespace CleanArchitecture.Domain.Events;

public class YoutubeVideoDownloadedEvent : BaseEvent
{
    public YoutubeVideo YoutubeVideo { get; }

    public YoutubeVideoDownloadedEvent(YoutubeVideo youtubeVideo)
    {
        YoutubeVideo = youtubeVideo;
    }
}