using Prism.Events;

namespace YoutubeDownloader_WPFCore.Controls.VideoPanel.ViewModel;

/// <summary>
/// Event published when a playable video stream URL has been resolved and
/// the Video panel should load/play it in the media element.
/// Payload: string absolute URL to a playable stream (muxed audio+video).
/// </summary>
internal sealed class VideoPlaybackRequestedEvent : PubSubEvent<string>
{
}
