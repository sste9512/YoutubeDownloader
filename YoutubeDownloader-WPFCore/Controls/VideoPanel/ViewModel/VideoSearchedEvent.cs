using Prism.Events;
using YoutubeExplode.Models;

namespace YoutubeDownloader_WPFCore.Controls.VideoPanel.ViewModel;

internal class VideoSearchedEvent : PubSubEvent<Video>
{
}