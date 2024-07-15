using System.Windows.Controls;
using System.Windows.Media.Imaging;
using YoutubeExplode;
using YoutubeExplode.Videos;

namespace YoutubeDownloader_WPFCore.Application.Controls.VideoInfoPanel.View;

public partial class VideoInfoPanel : UserControl
{
    public VideoInfoPanel()
    {
        InitializeComponent();
    }
        
        

    public async void SyncInfoToPanel(Video video, string url, YoutubeClient client)
    {
        try
        {
            VideoTitle.Content = video.Title;

            var thmbNail = new BitmapImage();
            thmbNail.BeginInit();
            thmbNail.UriSource = new Uri(video.Thumbnails.First().Url, UriKind.Absolute);
            thmbNail.EndInit();
            ViewCount.Content = video.Engagement.ViewCount + " views";
            LikesCount.Content = video.Engagement.LikeCount;
            DislikesCount.Content = video.Engagement.DislikeCount;
            VideoThumbnail.Source = thmbNail;
            VideoPublishedDate.Content = video.UploadDate;
            VideoDesciption.Content = video.Description;
            VideoAuthor.Content = video.Author;
            VideoDuration.Content = video.Duration;
            /*var mediaStreamInfos = await client.GetVideoMediaStreamInfosAsync(YoutubeClient.ParseVideoId(url));
            var streaminfo = mediaStreamInfos.Muxed.WithHighestVideoQuality();

            VideoAvailable.Content = streaminfo.Container.GetFileExtension();
            SoundAvailable.Content = mediaStreamInfos.GetAll();
            VideoUrl.Content = url;*/
        }
        catch (Exception ex)
        {
            Console.Out.WriteLine(ex.InnerException);
            // ignored
        }
    }
}