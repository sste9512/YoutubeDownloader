using Prism.Events;
using YoutubeExplode.Videos;

namespace YoutubeDownloader_WPFCore.Controls.VideoPanel.ViewModel;

internal class VideoSearchedEvent : PubSubEvent<Video>
{
}