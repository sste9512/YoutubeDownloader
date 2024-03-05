using System.Windows.Controls;
using System.Windows.Media.Imaging;
using YoutubeExplode;
using YoutubeExplode.Models;
using YoutubeExplode.Models.MediaStreams;


namespace YoutubeDownloader_WPFCore.Controls.VideoInfoPanel.View;

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
            VideoTitle.Content = video.Title;

            var thmbNail = new BitmapImage();
            thmbNail.BeginInit();
            thmbNail.UriSource = new Uri(video.Thumbnails.MediumResUrl, UriKind.Absolute);
            thmbNail.EndInit();
            ViewCount.Content = video.Statistics.ViewCount + " views";
            LikesCount.Content = video.Statistics.LikeCount;
            DislikesCount.Content = video.Statistics.DislikeCount;
            VideoThumbnail.Source = thmbNail;
            VideoPublishedDate.Content = video.UploadDate;
            VideoDesciption.Content = video.Description;
            VideoAuthor.Content = video.Author;
            VideoDuration.Content = video.Duration;
            mediaStreamInfos = await client.GetVideoMediaStreamInfosAsync(YoutubeClient.ParseVideoId(url));
            var streaminfo = mediaStreamInfos.Muxed.WithHighestVideoQuality();

            VideoAvailable.Content = streaminfo.Container.GetFileExtension();
            SoundAvailable.Content = mediaStreamInfos.GetAll();
            VideoUrl.Content = url;
        }
        catch (Exception ex)
        {
            Console.Out.WriteLine(ex.InnerException);
            // ignored
        }
    }
}