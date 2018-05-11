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

        public async void syncInfoToPanel(Video video, string url, YoutubeClient client, MediaStreamInfoSet _mediaStreamInfos)
        {
            try
            {
                this.videoTitle.Content = video.Title;

                BitmapImage thmbNail = new BitmapImage();
                thmbNail.BeginInit();
                thmbNail.UriSource = new Uri(video.Thumbnails.MediumResUrl, UriKind.Absolute);
                thmbNail.EndInit();
                this.viewCount.Content = video.Statistics.ViewCount + " views";
                this.likesCount.Content = video.Statistics.LikeCount;
                this.dislikesCount.Content = video.Statistics.DislikeCount;
                this.videoThumbnail.Source = thmbNail;
                this.videoPublishedDate.Content = video.UploadDate;
                this.videoDesciption.Content = video.Description;
                this.videoAuthor.Content = video.Author;
                this.videoDuration.Content = video.Duration;
                _mediaStreamInfos = await client.GetVideoMediaStreamInfosAsync(YoutubeClient.ParseVideoId(url));
                var streaminfo = _mediaStreamInfos.Muxed.WithHighestVideoQuality();

                videoAvailable.Content = streaminfo.Container.GetFileExtension();
                soundAvailable.Content = _mediaStreamInfos.GetAll();
                videoURL.Content = url;
            }
            catch (Exception ex)
            {
            }
        }
    }
}