
using FFMpegSharp;
using FFMpegSharp.Enums;
using FFMpegSharp.FFMPEG;
using FFMpegSharp.FFMPEG.Enums;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using YoutubeDownloader.PlayList.ViewModel;
using YoutubeExplode;
using YoutubeExplode.Models;
using YoutubeExplode.Models.ClosedCaptions;
using YoutubeExplode.Models.MediaStreams;

namespace YoutubeDownloader
{
    
    public partial class MainWindow : Window
    {
        private bool _isBusy;
        private string _query;
        YoutubeClient client = new YoutubeClient();
        private Channel _channel;
        private MediaStreamInfoSet _mediaStreamInfos;
        private MediaStreamInfo mediaStream;
        private IReadOnlyList<ClosedCaptionTrackInfo> _closedCaptionTrackInfos;
        private double _progress;
        private bool _isProgressIndeterminate;
        Video video;



        public MainWindow()
        {
            InitializeComponent();
            videoPanel.queryVideoButton.Click += this.queryVideoEvent;
            videoInfoPanel.downloadButton.Click += this.Download_Video;
        }




        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }



        private void RootWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }




        private async void queryVideoEvent(object sender, RoutedEventArgs e)
        {
            try
            { 
              string url = videoPanel.urlInput.Text;
              video = await client.GetVideoAsync(YoutubeClient.ParseVideoId(url));
              videoInfoPanel.syncInfoToPanel(video, url, client, _mediaStreamInfos);
              playList.initPlayListFromURL(url, client);
             
            }
            catch (Exception ex)
            {

            }

           
        }







        public void encodeToMP4(VideoType type, string inputFile, string outputFile)
        {


            //string inputVideoFile = "C:/Users/steve/Desktop/Private world (Vaporwave mix).webm",
            //outputVideoFile = "C:/Users/steve/Desktop/Private world (Vaporwave mix).Mp4";
           /* FileInfo outputFile = new FileInfo(outputVideoFile);

           var video = VideoInfo.FromPath(inputVideoFile);

         
            // easily track conversion progress
            //video.OnConversionProgress += video_OnConversionProgress;

            // input and output strings are required
            // all other parameters are optional
            video.ConvertTo(VideoType.Mp4, outputFile, Speed.UltraFast,
                VideoSize.Original,
                AudioQuality.Hd,
                true,
                false);
                */
            
        }






        private async void DownloadMediaStream(MediaStreamInfo info)
        {
            // Create dialog
            try
            {
                var fileExt = info.Container.GetFileExtension();
                var defaultFileName = video.Title + "." + fileExt;
                // .Replace(Path.GetInvalidFileNameChars(), '_');
                var sfd = new SaveFileDialog
                {
                    AddExtension = true,
                    DefaultExt = fileExt,
                    FileName = defaultFileName,
                    Filter = $"{info.Container} Files|*.{fileExt}|All Files|*.*"
                };

                // Select file path
                if (sfd.ShowDialog() != true)
                    return;

                var filePath = sfd.FileName;

                // Download to file
                _isBusy = true;
                _progress = 0;

                var progressHandler = new Progress<double>(p => _progress = p);
                await client.DownloadMediaStreamAsync(info, filePath, progressHandler);

                _isBusy = false;
                _progress = 0;
            }
            catch (Exception ex)
            {

            }

        }






        private async void Download_Video(object sender, RoutedEventArgs e)
        {
            DownloadMediaStream(_mediaStreamInfos.Muxed.WithHighestVideoQuality());
        }






        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
           // encodeToMP4();
        }






        private void AddPlayList(object sender, RoutedEventArgs e)
        {
            PlaylistCreationWindow window = new PlaylistCreationWindow();
            window.Show();
        }
    }
}
