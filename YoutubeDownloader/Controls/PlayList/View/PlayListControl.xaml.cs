using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using YoutubeDownloader.Controls.PlayList.ViewModel;
using YoutubeExplode;

namespace YoutubeDownloader.Controls.PlayList.View
{
    public partial class PlayListControl : UserControl
    {
        public PlayListControl()
        {
            InitializeComponent();
        }

        public async void InitPlayListFromUrl(string playListUrl, YoutubeClient client)
        {
            try
            {
                var playlist = await client.GetPlaylistAsync(YoutubeClient.ParsePlaylistId(playListUrl));
                this.PlaylistAuthor.Content = playlist.Author;
                this.PlaylistTitle.Content = playlist.Title;
                for (int i = 0; i < playlist.Videos.Count; i++)
                {
                    ListBoxItem item = new ListBoxItem();
                    PlayListItemControl playItem = new PlayListItemControl();
                    playItem.NumberLabel.Content = i + 1;
                    playItem.VideoTitle.Content = playlist.Videos[i].Title;

                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(playlist.Videos[i].Thumbnails.MediumResUrl, UriKind.Absolute);
                    bitmap.EndInit();
                    playItem.VideoThumbnail.Source = bitmap;
                    if (playlist.Videos[i].Author.Equals(""))
                    {
                        playItem.VideoAuthor.Content = "Youtube";
                    }
                    else
                    {
                        playItem.VideoAuthor.Content = playlist.Videos[i].Author;
                    }
                    playItem.VideoDuration.Content = playlist.Videos[i].Duration;
                    item.Content = playItem;
                    PlayListBox.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.InnerException);
            }
        }
    }
}