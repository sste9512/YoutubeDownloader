using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using YoutubeExplode;
using YoutubeExplode.Models;
using YoutubeExplode.Models.MediaStreams;

namespace YoutubeDownloader_WPFCore.Controls.PlayList.ViewModel;

public class PlayListItemViewModel : BindableBase
{
    private readonly Video _video;
    private readonly IEventAggregator _eventAggregator;
    private readonly YoutubeClient _youtubeClient;

    private int _number;
    private string _title = string.Empty;
    private string _author = string.Empty;
    private string _thumbnailUrl = string.Empty;
    private string _duration = string.Empty;
    private bool _isDownloading;
    private double _downloadProgress;
    private bool _isDownloaded;

    public PlayListItemViewModel(Video video, int number, IEventAggregator eventAggregator, YoutubeClient youtubeClient)
    {
        _video = video ?? throw new ArgumentNullException(nameof(video));
        _eventAggregator = eventAggregator ?? throw new ArgumentNullException(nameof(eventAggregator));
        _youtubeClient = youtubeClient ?? throw new ArgumentNullException(nameof(youtubeClient));

        _number = number;
        _title = video.Title;
        _author = video.Author;
        _thumbnailUrl = video.Thumbnails.MediumResUrl;
        _duration = FormatDuration(video.Duration);

        DownloadVideoCommand = new DelegateCommand(async () => await DownloadVideoAsync(), CanDownloadVideo);
        PlayVideoCommand = new DelegateCommand(PlayVideo, CanPlayVideo);
        ShowDetailsCommand = new DelegateCommand(ShowDetails);
    }

    public Video Video => _video;

    public int Number
    {
        get => _number;
        set => SetProperty(ref _number, value);
    }

    public string Title
    {
        get => _title;
        set => SetProperty(ref _title, value);
    }

    public string Author
    {
        get => _author;
        set => SetProperty(ref _author, value);
    }

    public string ThumbnailUrl
    {
        get => _thumbnailUrl;
        set => SetProperty(ref _thumbnailUrl, value);
    }

    public string Duration
    {
        get => _duration;
        set => SetProperty(ref _duration, value);
    }

    public bool IsDownloading
    {
        get => _isDownloading;
        set
        {
            if (SetProperty(ref _isDownloading, value))
            {
                DownloadVideoCommand.RaiseCanExecuteChanged();
                PlayVideoCommand.RaiseCanExecuteChanged();
            }
        }
    }

    public double DownloadProgress
    {
        get => _downloadProgress;
        set => SetProperty(ref _downloadProgress, value);
    }

    public bool IsDownloaded
    {
        get => _isDownloaded;
        set
        {
            if (SetProperty(ref _isDownloaded, value))
            {
                PlayVideoCommand.RaiseCanExecuteChanged();
            }
        }
    }

    public DelegateCommand DownloadVideoCommand { get; }
    public DelegateCommand PlayVideoCommand { get; }
    public DelegateCommand ShowDetailsCommand { get; }

    private bool CanDownloadVideo()
    {
        return !IsDownloading && !IsDownloaded;
    }

    private bool CanPlayVideo()
    {
        return !IsDownloading;
    }

    public async Task DownloadVideoAsync()
    {
        if (IsDownloading || IsDownloaded)
            return;

        try
        {
            IsDownloading = true;
            DownloadProgress = 0;

            var videoId = _video.Id;
            var streamManifest = await _youtubeClient.GetVideoMediaStreamInfosAsync(videoId);

            // Get the best muxed stream (video + audio)
            var streamInfo = streamManifest.Muxed.WithHighestVideoQuality();

            if (streamInfo != null)
            {
                // Sanitize filename
                var sanitizedTitle = string.Join("_", Title.Split(Path.GetInvalidFileNameChars()));
                var outputFilePath = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.MyVideos),
                    $"{sanitizedTitle}.{streamInfo.Container.GetFileExtension()}"
                );

                // Download with progress tracking
                var progress = new Progress<double>(p =>
                {
                    DownloadProgress = p * 100;
                });

                await _youtubeClient.DownloadMediaStreamAsync(streamInfo, outputFilePath, progress);

                IsDownloaded = true;
                Debug.WriteLine($"Downloaded: {Title}");
            }
            else
            {
                Debug.WriteLine($"No suitable stream found for: {Title}");
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error downloading video {Title}: {ex.Message}");
            throw;
        }
        finally
        {
            IsDownloading = false;
        }
    }

    private void PlayVideo()
    {
        try
        {
            // Publish event to play this video
            Debug.WriteLine($"Playing video: {Title}");
            // Could publish a VideoPlayRequested event here
            // _eventAggregator.GetEvent<VideoPlayRequestedEvent>().Publish(_video);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error playing video: {ex.Message}");
        }
    }

    private void ShowDetails()
    {
        try
        {
            Debug.WriteLine($"Showing details for: {Title}");
            // Could publish an event to show video details in another panel
            // _eventAggregator.GetEvent<ShowVideoDetailsEvent>().Publish(_video);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error showing details: {ex.Message}");
        }
    }

    private string FormatDuration(TimeSpan duration)
    {
        try
        {
            if (duration.TotalHours >= 1)
            {
                return $"{duration.Hours:D2}:{duration.Minutes:D2}:{duration.Seconds:D2}";
            }
            return $"{duration.Minutes:D2}:{duration.Seconds:D2}";
        }
        catch (Exception)
        {
            return "00:00";
        }
    }
}

