using System.Windows;
using MediatR;
using Metalama.Extensions.DependencyInjection;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using YoutubeDownloader_WPFCore.Application.Controls.PlayList.View;
using YoutubeDownloader_WPFCore.Application.Controls.PlayList.ViewModel;
using YoutubeDownloader_WPFCore.Application.Core.Behavioural.CQRS.Pipelines;
using YoutubeDownloader_WPFCore.Application.Interfaces;
using YoutubeExplode;
using YoutubeExplode.Common;
using YoutubeExplode.Playlists;

namespace YoutubeDownloader_WPFCore.Application.Controls.PlayList
{
    public class UpdatePlaylistCommand : ViewRequest<IReadOnlyList<PlaylistVideo>, PlayListControl>
    { 
           public string PlayListId { get; init; }

    }

    public class UpdatePlaylistCommandHandler : IRequestHandler<UpdatePlaylistCommand, IReadOnlyList<PlaylistVideo>>
    {

        [Dependency]
        private YoutubeClient _client;

        [Dependency]
        private IDocumentStore _documentStore;


        public async Task<IReadOnlyList<PlaylistVideo>> Handle(UpdatePlaylistCommand request, CancellationToken cancellationToken)
        {
            var playlist = await _client.Playlists.GetVideosAsync("https://www.youtube.com/playlist?list=PL4vbGURud_Hpzk-DC_xQZ8e_cwwJyxt_W", cancellationToken);

            foreach (var playlistVideo in playlist)
            {
                var item = new ListBoxItem();

                PlayListItemControl playItem = new PlayListItemControl
                {
                    NumberLabel =
                    {
                        Content = playlistVideo.Duration
                    },
                    VideoTitle =
                    {
                        Content = playlistVideo.Title
                    }
                };


                _documentStore.PlaylistVideos.Upsert(playlistVideo);
             
                // TODO : Store these bitmaps in litedb, along with video stream data
                var bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(playlistVideo.Thumbnails[0].Url);
                bitmap.EndInit();

               
                playItem.VideoThumbnail.Source = bitmap;
                if (playlistVideo.Author.Equals(""))
                {
                    playItem.VideoAuthor.Content = "Youtube";
                }
                else
                {
                    playItem.VideoAuthor.Content = playlistVideo.Author;
                }
                playItem.VideoDuration.Content = playlistVideo.Duration;
                item.Content = playItem; 
                request.View.PlayListBox.Items.Add(item);
            }

            return playlist;
        }
    }
}
