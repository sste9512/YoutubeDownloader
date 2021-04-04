using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using YoutubeExplode;
using YoutubeExplode.Models;
using YoutubeExplode.Models.MediaStreams;

namespace YoutubeDownloader.VideoInfoPanel.View
{
    public partial class VideoInfoPanel : UserControl
    {
        public VideoInfoPanel()
        {
            InitializeComponent();
        }

        public async void SyncInfoToPanel(Video video, string url, YoutubeClient client, MediaStreamInfoSet mediaStreamInfos)
        {
            try
            {
                this.VideoTitle.Content = video.Title;

                BitmapImage thmbNail = new BitmapImage();
                thmbNail.BeginInit();
                thmbNail.UriSource = new Uri(video.Thumbnails.MediumResUrl, UriKind.Absolute);
                thmbNail.EndInit();
                this.ViewCount.Content = video.Statistics.ViewCount + " views";
                this.LikesCount.Content = video.Statistics.LikeCount;
                this.DislikesCount.Content = video.Statistics.DislikeCount;
                this.VideoThumbnail.Source = thmbNail;
                this.VideoPublishedDate.Content = video.UploadDate;
                this.VideoDesciption.Content = video.Description;
                this.VideoAuthor.Content = video.Author;
                this.VideoDuration.Content = video.Duration;
                mediaStreamInfos = await client.GetVideoMediaStreamInfosAsync(YoutubeClient.ParseVideoId(url));
                var streaminfo = mediaStreamInfos.Muxed.WithHighestVideoQuality();

                VideoAvailable.Content = streaminfo.Container.GetFileExtension();
                SoundAvailable.Content = mediaStreamInfos.GetAll();
                VideoUrl.Content = url;
            }
            catch (Exception ex)
            {
                // ignored
            }
        }
    }
}