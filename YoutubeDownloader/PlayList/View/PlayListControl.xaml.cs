using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using YoutubeDownloader.PlayList.ViewModel;
using YoutubeExplode;

namespace YoutubeDownloader.PlayList.View
{
    public partial class PlayListControl : UserControl
    {
        public PlayListControl()
        {
            InitializeComponent();
        }

        public async void initPlayListFromURL(string playListUrl, YoutubeClient client)
        {
            try
            {
                var playlist = await client.GetPlaylistAsync(YoutubeClient.ParsePlaylistId(playListUrl));
                this.playlistAuthor.Content = playlist.Author;
                this.playlistTitle.Content = playlist.Title;
                for (int i = 0; i < playlist.Videos.Count; i++)
                {
                    ListBoxItem item = new ListBoxItem();
                    PlayListItemControl playItem = new PlayListItemControl();
                    playItem.numberLabel.Content = i + 1;
                    playItem.videoTitle.Content = playlist.Videos[i].Title;

                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(playlist.Videos[i].Thumbnails.MediumResUrl, UriKind.Absolute);
                    bitmap.EndInit();
                    playItem.videoThumbnail.Source = bitmap;
                    if (playlist.Videos[i].Author.Equals(""))
                    {
                        playItem.videoAuthor.Content = "Youtube";
                    }
                    else
                    {
                        playItem.videoAuthor.Content = playlist.Videos[i].Author;
                    }
                    playItem.videoDuration.Content = playlist.Videos[i].Duration;
                    item.Content = playItem;
                    playListBox.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}