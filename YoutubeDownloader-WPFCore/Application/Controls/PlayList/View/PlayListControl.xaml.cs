using Metalama.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Windows;
using System.Windows.Controls;
using YoutubeDownloader_WPFCore.Application.Aspects.TypeAspects;
using YoutubeDownloader_WPFCore.Application.Controls.PlayList.Commands;
using YoutubeExplode;
using YoutubeExplode.Playlists;

namespace YoutubeDownloader_WPFCore.Application.Controls.PlayList.View;

[ComponentBehaviors]
public partial class PlayListControl : UserControl
{
    
    [Dependency] private YoutubeClient _client;

    [Dependency] private ILogger _logger;
    
    private Playlist playList { get; set; }

    private IReadOnlyList<PlaylistVideo> Playlists { get; set; }


    public PlayListControl()
    {
        InitializeComponent();
    }

   


    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void DownloadPlaylist_Click(object sender, RoutedEventArgs e)
    {
        var command = new DownloadPlaylistCommand
        {
            PlaylistVideos = Playlists,
            VideoTypeOptions = VideoTypeOptions.Audio,
            PlayList = playList
        };
        command.Accept(this);

        await Send(command);
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        PlayListBox.Items.Clear();
        if (SearchInput.Text == null) return;
        playList = await _client.Playlists.GetAsync(SearchInput.Text.Trim());

        PlaylistAuthor.Content = playList.Author;
        PlaylistTitle.Content = playList.Title;

        playlistThumbnailImage.Source = playList.Thumbnails[playList.Thumbnails.Count - 1].Url.BitmapFromUrl();

        var command = new UpdatePlaylistCommand
        {
            PlayListId = SearchInput.Text.Trim()
        };
        command.Accept(this);

        Playlists = await Send(command);
    }
}