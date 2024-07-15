using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Metalama.Extensions.DependencyInjection;
using YoutubeDownloader_WPFCore.Application.Aspects.TypeAspects;
using YoutubeDownloader_WPFCore.Application.Controls.PlayList.ViewModel;
using YoutubeExplode;
using YoutubeExplode.Common;
using YoutubeExplode.Playlists;
using YoutubeExplode.Videos;
using YoutubeExplode.Videos.Streams;
using static System.Net.WebRequestMethods;

namespace YoutubeDownloader_WPFCore.Application.Controls.PlayList.View;

[ComponentBehaviors]
public partial class PlayListControl : UserControl
{

    [Dependency] private YoutubeClient _client;

    public IReadOnlyList<PlaylistVideo> Playlists { get; set; }


    public PlayListControl()
    {
        InitializeComponent();
    }


    public async void InitPlayListFromUrl()
    {
        var command = new UpdatePlaylistCommand()
        {
            PlayListId = "https://www.youtube.com/playlist?list=PLwrk1eoLMM9RTTBv97ugjIhhsUldJ4d-2"
        };
        command.Accept(this);

        Playlists = await Send(command);
    }

    private async void DownloadPlaylist_Click(object sender, RoutedEventArgs e)
    {
        var result = new List<Video>();
        foreach (var playlistVideo in Playlists)
        {
            var streamManifest = await _client.Videos.Streams.GetManifestAsync(playlistVideo.Url);
            // ...or highest bitrate audio-only stream
            var streamInfo = streamManifest.GetAudioOnlyStreams().GetWithHighestBitrate();

            // ...or highest quality MP4 video-only stream
            var streamInfos = streamManifest
                .GetVideoOnlyStreams()
                .Where(s => s.Container == Container.Mp4)
                .GetWithHighestVideoQuality();

            var video = await _client.Videos.Streams.GetAsync(streamInfos, CancellationToken.None);

            if (!Directory.Exists(@$"C:\Users\Steven\Desktop\Videos\{playlistVideo.Id}"))
            {
                Directory.CreateDirectory(@$"C:\Users\Steven\Desktop\Videos\{playlistVideo.Id}");
            }
            // Download the stream to a file
           await _client.Videos.Streams.DownloadAsync(streamInfos, @$"C:\Users\Steven\Desktop\Videos\{playlistVideo.Id}\{playlistVideo.Title}.{streamInfo.Container}");
            
            
            //result.Add(video);
        }
    }
}