using YoutubeExplode.Videos;

namespace CleanArchitecture.Application.Common.Interfaces;

public interface IYoutubeClient
{
    Task<Video> DownloadVideoAsync(string url, CancellationToken cancellationToken = default);

    Task<Stream> AccessStreamAsync(string url, StreamOptions streamOptions = default,
        CancellationToken cancellationToken = default);
}

public readonly struct StreamOptions
{
    public MediaSpecification MediaSpecification { get; init; }
}

public enum MediaSpecification
{
    VideoOnly,
    SoundOnly
}