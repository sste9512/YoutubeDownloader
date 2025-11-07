using Prism.Mvvm;
using Prism.Commands;
using Prism.Events;
using YoutubeExplode;
using System.Windows.Media.Imaging;
using SurrealDb.Embedded.RocksDb;
using YoutubeDownloader_WPFCore.Controls.VideoPanel.ViewModel;
using YoutubeDownloader_WPFCore.Core.Values;
using YoutubeExplode.Videos;
using YoutubeExplode.Videos.Streams;
using YoutubeDownloader_WPFCore.Controls.VideoPanel.ViewModel;

namespace YoutubeDownloader_WPFCore.Controls.VideoInfoPanel.ViewModel;

public sealed class VideoInfoPanelViewModel : BindableBase, IDisposable, IAsyncDisposable
{
    private readonly YoutubeClient _youtubeClient;
    private readonly IEventAggregator _eventAggregator;
    private readonly SurrealDbRocksDbClient _surrealDbRocksDbClient;

    private string _videoTitle = string.Empty;
    private BitmapImage? _videoThumbnail;
    private string _likesCount = string.Empty;
    private string _dislikesCount = string.Empty;
    private string _viewCount = string.Empty;
    private string _videoAuthor = string.Empty;
    private string _videoPublishedDate = string.Empty;
    private string _videoDescription = string.Empty;
    private string _videoDuration = string.Empty;
    private string _videoUrl = string.Empty;
    private string _videoExtension = string.Empty;
    private string _videoAvailable = string.Empty;
    private string _soundAvailable = string.Empty;
    private string _mixedAvailable = string.Empty;
    private bool _isLoading;
    private string? _playbackUrl;
    private string? _currentVideoId;
    
    private SubscriptionToken? _subscriptionToken;

    public VideoInfoPanelViewModel(YoutubeClient youtubeClient, IEventAggregator eventAggregator, SurrealDbRocksDbClient surrealDbRocksDbClient)
    {
        _youtubeClient = youtubeClient ?? throw new ArgumentNullException(nameof(youtubeClient));
        _eventAggregator = eventAggregator ?? throw new ArgumentNullException(nameof(eventAggregator));
        _surrealDbRocksDbClient = surrealDbRocksDbClient;

        DownloadVideoCommand = new DelegateCommand(async () => await DownloadVideoAsync(), CanDownloadVideo);
        DownloadAndAddToPlaylistCommand =
            new DelegateCommand(async () => await DownloadAndAddToPlaylistAsync(), CanDownloadVideo);


        _subscriptionToken = _eventAggregator.GetEvent<VideoSearchedEvent>().Subscribe(async x =>
        {
            await Action(x);
        });
    }

    private async ValueTask Action(Video x)
    {
        var videoUrl = _videoUrl; // Preserve the current URL
        var result = await LoadVideoInfoAsync(x, videoUrl);
        if (!result.IsOk)
        {
            System.Diagnostics.Debug.WriteLine($"Failed to load video info: {result.UnwrapErr()}");
        }
    }

    #region Properties

    public string VideoTitle
    {
        get => _videoTitle;
        set => SetProperty(ref _videoTitle, value);
    }

    public BitmapImage? VideoThumbnail
    {
        get => _videoThumbnail;
        set => SetProperty(ref _videoThumbnail, value);
    }

    public string LikesCount
    {
        get => _likesCount;
        set => SetProperty(ref _likesCount, value);
    }

    public string DislikesCount
    {
        get => _dislikesCount;
        set => SetProperty(ref _dislikesCount, value);
    }

    public string ViewCount
    {
        get => _viewCount;
        set => SetProperty(ref _viewCount, value);
    }

    public string VideoAuthor
    {
        get => _videoAuthor;
        set => SetProperty(ref _videoAuthor, value);
    }

    public string VideoPublishedDate
    {
        get => _videoPublishedDate;
        set => SetProperty(ref _videoPublishedDate, value);
    }

    public string VideoDescription
    {
        get => _videoDescription;
        set => SetProperty(ref _videoDescription, value);
    }

    public string VideoDuration
    {
        get => _videoDuration;
        set => SetProperty(ref _videoDuration, value);
    }

    public string VideoUrl
    {
        get => _videoUrl;
        set => SetProperty(ref _videoUrl, value);
    }

    public string VideoExtension
    {
        get => _videoExtension;
        set => SetProperty(ref _videoExtension, value);
    }

    public string VideoAvailable
    {
        get => _videoAvailable;
        set => SetProperty(ref _videoAvailable, value);
    }

    public string SoundAvailable
    {
        get => _soundAvailable;
        set => SetProperty(ref _soundAvailable, value);
    }

    public string MixedAvailable
    {
        get => _mixedAvailable;
        set => SetProperty(ref _mixedAvailable, value);
    }

    public bool IsLoading
    {
        get => _isLoading;
        set
        {
            if (SetProperty(ref _isLoading, value))
            {
                DownloadVideoCommand.RaiseCanExecuteChanged();
                DownloadAndAddToPlaylistCommand.RaiseCanExecuteChanged();
            }
        }
    }

    #endregion

    #region Commands

    public DelegateCommand DownloadVideoCommand { get; }
    public DelegateCommand DownloadAndAddToPlaylistCommand { get; }

    #endregion

    #region Public Methods

    public async ValueTask<Result<bool, string>> LoadVideoInfoAsync(Video video, string url,
        CancellationToken cancellationToken = default)
    {
        try
        {
            IsLoading = true;

            _currentVideoId = video.Id;
            var watchUrl = $"https://www.youtube.com/watch?v={_currentVideoId}";
            var embedUrl = $"https://www.youtube.com/embed/{_currentVideoId}?autoplay=1&playsinline=1&mute=1&enablejsapi=1";

            VideoTitle = video.Title ?? string.Empty;
            VideoAuthor = video.Author.ChannelTitle ?? string.Empty;
            VideoPublishedDate = video.UploadDate.ToString("yyyy-MM-dd");
            VideoDescription = video.Description ?? string.Empty;
            VideoDuration = video.Duration.ToString();
            VideoUrl = watchUrl;

            // Prefer WebView2 embed playback; set early, keep streams for info only
            _playbackUrl = embedUrl;

            // Load thumbnail
            await LoadThumbnailAsync(video, cancellationToken);

            // Load statistics
            LoadStatistics(video);

            // Load media stream info (for info UI, not for WebView2 playback)
            var mediaStreamResult = await LoadMediaStreamInfoAsync(watchUrl, cancellationToken);
            if (!mediaStreamResult.IsOk)
            {
                // Even if stream info fails, attempt to play via embed URL
                System.Diagnostics.Debug.WriteLine($"Stream manifest load failed: {mediaStreamResult.UnwrapErr()}");
            }

            // If we have a resolved playback URL, request playback in the VideoPanel via event
            if (!string.IsNullOrWhiteSpace(_playbackUrl))
            {
                _eventAggregator.GetEvent<VideoPlaybackRequestedEvent>().Publish(_playbackUrl);
            }

            return Result.Ok<bool, string>(true);
        }
        catch (OperationCanceledException)
        {
            return Result.Err<bool, string>("Operation was cancelled");
        }
        catch (Exception ex)
        {
            return Result.Err<bool, string>($"Failed to load video info: {ex.Message}");
        }
        finally
        {
            IsLoading = false;
        }
    }

    #endregion

    #region Private Methods

    private async ValueTask LoadThumbnailAsync(Video video, CancellationToken cancellationToken)
    {
        try
        {
            var thumbnails = video.Thumbnails;
            if (thumbnails.Any())
            {
                var thumbnailUrl = thumbnails[0].Url;
                var thumbnail = new BitmapImage();
                thumbnail.BeginInit();
                thumbnail.UriSource = new Uri(thumbnailUrl, UriKind.Absolute);
                thumbnail.EndInit();

                // Freeze for cross-thread access
                thumbnail.Freeze();
                VideoThumbnail = thumbnail;
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Failed to load thumbnail: {ex.Message}");
            // Continue without thumbnail
        }
    }

    private void LoadStatistics(Video video)
    {
        ViewCount = video.Engagement.ViewCount.ToString("N0") + " views";
        LikesCount = video.Engagement.LikeCount.ToString("N0") ?? "0";
        DislikesCount = "Disabled";
    }

    private const string AudioContainerFilter = "audio";

    private async ValueTask<Result<StreamManifest, string>> LoadMediaStreamInfoAsync(string url,
        CancellationToken cancellationToken)
    {
        try
        {
            var videoId = VideoId.Parse(url);
            var streamManifest = await _youtubeClient.Videos.Streams.GetManifestAsync(videoId, cancellationToken);

            UpdateVideoStreamProperties(streamManifest);
            UpdateAudioAndMixedStreamCounts(streamManifest);

            return Result.Ok<StreamManifest, string>(streamManifest);
        }
        catch (Exception ex)
        {
            return Result.Err<StreamManifest, string>($"Failed to load media stream info: {ex.Message}");
        }
    }

    private void UpdateVideoStreamProperties(StreamManifest streamManifest)
    {
        var bestQualityStream = streamManifest.GetMuxedStreams().GetWithHighestVideoQuality();
        if (bestQualityStream != null)
        {
            var extension = bestQualityStream.Container.Name;
            VideoExtension = extension;
            VideoAvailable = extension;

            // Capture a playable muxed URL for the media element
            _playbackUrl = bestQualityStream.Url;
        }
    }

    private void UpdateAudioAndMixedStreamCounts(StreamManifest streamManifest)
    {
        var audioStreamCount = streamManifest.GetAudioStreams().Count();
        var mixedStreamCount = streamManifest.GetMuxedStreams().Count();

        SoundAvailable = $"{audioStreamCount} audio streams";
        MixedAvailable = $"{mixedStreamCount} mixed streams";
    }

    private bool CanDownloadVideo()
    {
        return !IsLoading && !string.IsNullOrWhiteSpace(VideoUrl);
    }

    private async ValueTask DownloadVideoAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            IsLoading = true;

            // TODO: Implement video download functionality
            await Task.Delay(1000, cancellationToken); // Simulate download
            
            var video = await _youtubeClient.Videos.GetAsync(_currentVideoId!, cancellationToken);
            
            

            System.Diagnostics.Debug.WriteLine($"Downloading video: {VideoTitle}");
        }
        catch (OperationCanceledException)
        {
            System.Diagnostics.Debug.WriteLine("Download cancelled");
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Download failed: {ex.Message}");
        }
        finally
        {
            IsLoading = false;
        }
    }

    private async ValueTask DownloadAndAddToPlaylistAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            IsLoading = true;

            // TODO: Implement download and add to playlist functionality
            await Task.Delay(1000, cancellationToken); // Simulate operation

            System.Diagnostics.Debug.WriteLine($"Downloading and adding to playlist: {VideoTitle}");
        }
        catch (OperationCanceledException)
        {
            System.Diagnostics.Debug.WriteLine("Download and add to playlist cancelled");
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Download and add to playlist failed: {ex.Message}");
        }
        finally
        {
            IsLoading = false;
        }
    }

    #endregion

    public void Dispose()
    {
        _subscriptionToken?.Dispose();
    }

    public async ValueTask DisposeAsync()
    {
        if (_subscriptionToken != null) await CastAndDispose(_subscriptionToken);

        return;

        static async ValueTask CastAndDispose(IDisposable resource)
        {
            if (resource is IAsyncDisposable resourceAsyncDisposable)
                await resourceAsyncDisposable.DisposeAsync();
            else
                resource.Dispose();
        }
    }
}