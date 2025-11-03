using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using YoutubeExplode;
using YoutubeExplode.Models;
using YoutubeExplode.Models.MediaStreams;

namespace YoutubeDownloader_WPFCore.Controls.PlayList.ViewModel;

public class PlayListControlViewModel : BindableBase
{
    private readonly IEventAggregator _eventAggregator;
    private readonly YoutubeClient _youtubeClient;

    private string _playlistTitle = string.Empty;
    private string _playlistAuthor = string.Empty;
    private bool _isLoading;
    private ObservableCollection<PlayListItemViewModel> _playlistItems = new();
    private string _selectedFormat = "Mp3";

    public PlayListControlViewModel(IEventAggregator eventAggregator, YoutubeClient youtubeClient)
    {
        _eventAggregator = eventAggregator ?? throw new ArgumentNullException(nameof(eventAggregator));
        _youtubeClient = youtubeClient ?? throw new ArgumentNullException(nameof(youtubeClient));

        DownloadPlaylistCommand = new DelegateCommand(async () => await DownloadEntirePlaylistAsync(), CanDownloadPlaylist);
    }

    public string PlaylistTitle
    {
        get => _playlistTitle;
        set => SetProperty(ref _playlistTitle, value);
    }

    public string PlaylistAuthor
    {
        get => _playlistAuthor;
        set => SetProperty(ref _playlistAuthor, value);
    }

    public bool IsLoading
    {
        get => _isLoading;
        set
        {
            if (SetProperty(ref _isLoading, value))
            {
                DownloadPlaylistCommand.RaiseCanExecuteChanged();
            }
        }
    }

    public ObservableCollection<PlayListItemViewModel> PlaylistItems
    {
        get => _playlistItems;
        set => SetProperty(ref _playlistItems, value);
    }

    public string SelectedFormat
    {
        get => _selectedFormat;
        set => SetProperty(ref _selectedFormat, value);
    }

    public DelegateCommand DownloadPlaylistCommand { get; }

    private bool CanDownloadPlaylist()
    {
        return !IsLoading && PlaylistItems.Any();
    }

    public async Task LoadPlaylistFromUrl(string playListUrl, YoutubeClient client)
    {
        if (string.IsNullOrEmpty(playListUrl))
            return;

        try
        {
            IsLoading = true;
            PlaylistItems.Clear();

            var playlistId = YoutubeClient.ParsePlaylistId(playListUrl);
            var playlist = await client.GetPlaylistAsync(playlistId);

            PlaylistTitle = playlist.Title;
            PlaylistAuthor = playlist.Author;
           
            var videos = playlist.Videos;

            int index = 1;
            foreach (var video in videos)
            {
                try
                {
                    var playlistItemViewModel = new PlayListItemViewModel(video, index, _eventAggregator, _youtubeClient);
                    PlaylistItems.Add(playlistItemViewModel);
                    index++;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error loading playlist item: {ex.Message}");
                }
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error loading playlist: {ex.Message}");
            // Could publish an error event here for UI notification
        }
        finally
        {
            IsLoading = false;
        }
    }

    private async Task DownloadEntirePlaylistAsync()
    {
        if (!PlaylistItems.Any())
            return;

        try
        {
            IsLoading = true;

            foreach (var item in PlaylistItems)
            {
                try
                {
                    await item.DownloadVideoAsync();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error downloading video {item.Title}: {ex.Message}");
                  
                  
                }
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error downloading playlist: {ex.Message}");
        }
        finally
        {
            IsLoading = false;
        }
    }
}

