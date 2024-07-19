using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Google.Apis.Logging;
using MaterialDesignThemes.Wpf;
using MediatR;
using Metalama.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using YoutubeDownloader_WPFCore.Application.Controls.PlayList.View;
using YoutubeDownloader_WPFCore.Application.Controls.PlayList.ViewModel;
using YoutubeDownloader_WPFCore.Application.Core.Behavioural.CQRS.Pipelines;
using YoutubeDownloader_WPFCore.Application.Interfaces;
using YoutubeDownloader_WPFCore.Infrastructure.Stores;
using YoutubeExplode;
using YoutubeExplode.Playlists;

#pragma warning disable IDE0301

#pragma warning disable CS0649 // Field is never assigned to, and will always have its default value
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace YoutubeDownloader_WPFCore.Application.Controls.PlayList.Commands
{
    public class UpdatePlaylistCommand : ViewRequest<IReadOnlyList<PlaylistVideo>, PlayListControl>
    {
        public string PlayListId { get; init; }
    }

    public class UpdatePlaylistCommandHandler : IRequestHandler<UpdatePlaylistCommand, IReadOnlyList<PlaylistVideo>>
    {
      
        
        [Dependency] private YoutubeClient _client;
        [Dependency] private IDocumentStore _documentStore;
        [Dependency] private ILogger<UpdatePlaylistCommandHandler> _logger;
        [Dependency] private ImageStore _imageStore;


        public async Task<IReadOnlyList<PlaylistVideo>> Handle(UpdatePlaylistCommand request,
            CancellationToken cancellationToken)
        {
            var list = new List<PlaylistVideo>();

            try
            {
                request.View.ProgressBar.Visibility = Visibility.Visible;

                await foreach (var batch in _client.Playlists.GetVideoBatchesAsync(request.PlayListId, cancellationToken))
                {
                    foreach (var video in batch.Items)
                    {
                        var playlistItem = new PlayListItemControl
                        {
                            NumberLabel = { Content = video.Duration }, 
                            VideoTitle = { Content = video.Title },
                            VideoThumbnail = { Source = video.Thumbnails[video.Thumbnails.Count - 1].Url.BitmapFromUrl() },
                            VideoAuthor = { Content = video.Author },
                            VideoDuration = { Content = video.Duration }
                        };

                        var bitmap = video.Thumbnails[video.Thumbnails.Count - 1].Url.BitmapFromUrl();

                       

                        if (bitmap.StreamSource is not null)
                        {
                          
                            var stream = new MemoryStream();

                            await bitmap.StreamSource.CopyToAsync(stream, cancellationToken);
                           
                            _imageStore
                                .To(video.FromPlaylistVideo())
                                .SaveImageFromFileOrStream(stream);
                        }
                        else
                        {
                            _logger.LogError($"Could not save {video.Title} to image storage");
                        }

                        var item = new ListBoxItem()
                        {
                            Content = playlistItem
                        };
                       
                        request.View.PlayListBox.Items.Add(item);
                        list.Add(video);
                    }

                    request.View.ProgressBar.Visibility = Visibility.Hidden;
                    _documentStore.PlaylistVideos.Upsert(list);
                    return list;
                }
            }
            catch (Exception ex)
            {
                await request.View.SnackBar.ShowWithTimeout(ex.Message, () =>
                {
                    _logger.LogError(ex.Message);
                    request.View.ProgressBar.Visibility = Visibility.Hidden;
                });
            }

            return Array.Empty<PlaylistVideo>();
        }
    }
}

public static class WpfExtensions
{
    public static BitmapImage BitmapFromUrl(this string url)
    {
        var bitmap = new BitmapImage();
        bitmap.BeginInit();
        bitmap.UriSource = new Uri(url);
        bitmap.EndInit();
        return bitmap;
    }
}

public static class MaterialExtensions
{
    public static async Task<Snackbar> ShowWithTimeout(this Snackbar snackBar, string message, Action action)
    {
        snackBar.IsActive = true;
        snackBar.Message = new SnackbarMessage
        {
            Content = message
        };
        action.Invoke();
        await Task.Delay(TimeSpan.FromSeconds(2), CancellationToken.None);
        snackBar.IsActive = false;
        return snackBar;
    }
}