using System.Configuration;
using System.IO;
using System.Windows;
using MediatR;
using Metalama.Extensions.DependencyInjection;
using Metalama.Framework.Aspects;
using Microsoft.Extensions.Logging;
using YoutubeDownloader_WPFCore.Application.Controls.PlayList.View;
using YoutubeDownloader_WPFCore.Application.Core.Behavioural.CQRS.Pipelines;
using YoutubeExplode;
using YoutubeExplode.Playlists;
using YoutubeExplode.Videos.Streams;

namespace YoutubeDownloader_WPFCore.Application.Controls.PlayList.Commands;

public enum VideoTypeOptions
{
    Video,
    VideoAndAudio,
    Audio
}

public class DownloadPlaylistCommand : ViewRequest<string, PlayListControl>
{
    public IReadOnlyList<PlaylistVideo> PlaylistVideos { get; init; }
    public VideoTypeOptions VideoTypeOptions { get; init; }
    public Playlist PlayList { get; init; }
}

public class DownloadPlaylistCommandHandler : IRequestHandler<DownloadPlaylistCommand, string>
{


    [Dependency] private YoutubeClient _client;

    [Dependency()] private ILogger _logger;

    IEnumerable<PlayListItemControl> _listItemControl;


    public async Task<string> Handle(DownloadPlaylistCommand request, CancellationToken cancellationToken)
    {
        request.View.ProgressBar.Visibility = Visibility.Visible;
        try
        {
            var result = new Task[request.PlaylistVideos.Count];

            for (var i = 0; i < request.PlaylistVideos.Count; i++)
            {
                result[i] = ExecuteDownload(request.PlayList.Title, request.PlaylistVideos[i], cancellationToken);
            }

            await Task.WhenAll(result);
            request.View.ProgressBar.Visibility = Visibility.Hidden;
        }
        catch (Exception e)
        {
            await request.View.SnackBar.ShowWithTimeout(e.Message, () =>
            {
                request.View.ProgressBar.Visibility = Visibility.Hidden;
                _logger.LogError(e.Message);
            });
        }


        return string.Empty;
    }

    private async Task ExecuteDownload(string playListTitle, PlaylistVideo playlistVideo,
        CancellationToken cancellationToken)
    {
        var streamManifest = await _client.Videos.Streams.GetManifestAsync(playlistVideo.Url, cancellationToken);

        var streamInfo = streamManifest
            .GetAudioOnlyStreams()
            .GetWithHighestBitrate();

        DirectoryInfo directoryInfo;


        if (!Directory.Exists(@$"C:\Users\Steven\Desktop\Videos\{playListTitle}"))
        {
            directoryInfo = Directory.CreateDirectory(@$"C:\Users\Steven\Desktop\Videos\{playListTitle}");
            _logger.LogInformation("Creating Directory : " + directoryInfo.Name);
        }

        // Download the stream to a file
        _logger.LogInformation($"Saving {playlistVideo.Title} to File");
        await _client.Videos.Streams.DownloadAsync(streamInfo,
            @$"C:\Users\Steven\Desktop\Videos\{playListTitle}\{playlistVideo.Title}.{streamInfo.Container}",
            cancellationToken: cancellationToken);

        /*var item = _listItemControl.First(x => x.VideoTitle.Content == playlistVideo.Title);*/
        //item.ProgressBar.Visibility = Visibility.Hidden;
    }
}