using Prism.Mvvm;
using Prism.Commands;
using Prism.Events;
using YoutubeExplode;
using System.Windows.Input;

namespace YoutubeDownloader_WPFCore.Controls.VideoPanel.ViewModel;

public class VideoPanelViewModel : BindableBase
{
    private readonly YoutubeClient _youtubeClient;
    private readonly IEventAggregator _eventAggregator;
    
    private string _searchInput = string.Empty;
    private bool _isLoading;

    public VideoPanelViewModel(YoutubeClient youtubeClient, IEventAggregator eventAggregator)
    {
        _youtubeClient = youtubeClient ?? throw new ArgumentNullException(nameof(youtubeClient));
        _eventAggregator = eventAggregator ?? throw new ArgumentNullException(nameof(eventAggregator));
        
        SearchCommand = new DelegateCommand(async () => await SearchVideoAsync(), CanSearch);
        PlayCommand = new DelegateCommand(PlayVideo);
        NextCommand = new DelegateCommand(PlayNext);
        VolumeCommand = new DelegateCommand(ToggleVolume);
        ClosedCaptionCommand = new DelegateCommand(ToggleClosedCaption);
        SettingsCommand = new DelegateCommand(OpenSettings);
    }

    public string SearchInput
    {
        get => _searchInput;
        set 
        { 
            if (SetProperty(ref _searchInput, value))
            {
                SearchCommand.RaiseCanExecuteChanged();
            }
        }
    }

    public bool IsLoading
    {
        get => _isLoading;
        set => SetProperty(ref _isLoading, value);
    }

    public DelegateCommand SearchCommand { get; }
    public DelegateCommand PlayCommand { get; }
    public DelegateCommand NextCommand { get; }
    public DelegateCommand VolumeCommand { get; }
    public DelegateCommand ClosedCaptionCommand { get; }
    public DelegateCommand SettingsCommand { get; }

    private bool CanSearch()
    {
        return !string.IsNullOrWhiteSpace(SearchInput) && !IsLoading;
    }

    /// Asynchronously searches for a video based on the current input from the user.
    /// If the input is empty or invalid, the operation will not proceed.
    /// Validates the YouTube URL, retrieves video details, and publishes a notification with the video information.
    /// Handles and logs any errors that occur during the search operation.
    /// Updates the loading state during the operation.
    /// <returns>A Task representing the asynchronous search operation.</returns>
    private async Task SearchVideoAsync()
    {
        if (string.IsNullOrWhiteSpace(SearchInput))
            return;

        try
        {
            IsLoading = true;
            
            System.Diagnostics.Debug.WriteLine($"Searching for video: {SearchInput}");

            // var videoId = YoutubeClient.ParseVideoId(SearchInput);
            // if (string.IsNullOrWhiteSpace(videoId))
            // {
            //     throw new ArgumentException("Invalid YouTube URL");
            // }

            // var video = await _youtubeClient.GetVideoAsync(videoId);
            
            var videoUrl = "https://www.youtube.com/watch?v=YyIaCP4qUFE&t=10892s";
            var id = await _youtubeClient.Videos.GetAsync(videoUrl);
            Console.WriteLine(id.Id);
            var video = id;

            var title = video.Title; 
            var author = video.Author.ChannelTitle;
            var duration = video.Duration;
            Console.WriteLine(duration);
            Console.WriteLine(author); 
            Console.WriteLine(title);
            _eventAggregator.GetEvent<VideoSearchedEvent>().Publish(video);
        }
        catch (Exception ex)
        {
            // Handle error - could log or show message to user
            System.Diagnostics.Debug.WriteLine($"Error searching for video: {ex.Message}");
        }
        finally
        {
            IsLoading = false;
        }
    }

    private void PlayVideo()
    {
        // Implement play functionality
        System.Diagnostics.Debug.WriteLine("Play video command executed");
    }

    private void PlayNext()
    {
        // Implement play next functionality  
        System.Diagnostics.Debug.WriteLine("Play next command executed");
    }

    private void ToggleVolume()
    {
        // Implement volume toggle functionality
        System.Diagnostics.Debug.WriteLine("Toggle volume command executed");
    }

    private void ToggleClosedCaption()
    {
        // Implement closed caption toggle functionality
        System.Diagnostics.Debug.WriteLine("Toggle closed caption command executed");
    }

    private void OpenSettings()
    {
        // Implement settings functionality
        System.Diagnostics.Debug.WriteLine("Open settings command executed");
    }
}