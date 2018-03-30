
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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
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



            for (int i = 0; i < 20; i++)
            {
                Border border = new Border();
                border.BorderBrush = Brushes.Red;
                border.BorderThickness = new Thickness(0,0,0,0);
                border.Background = Brushes.Transparent;
                border.Margin = new Thickness(12,12,12,12);
                border.CornerRadius = new CornerRadius(5, 5, 5, 5);
                StackPanel stack = new StackPanel();
                stack.Orientation = Orientation.Vertical;
                Image image = new Image();
                
                image.Height = 140;
                image.Width = 155;


                Label label = new Label();
                label.Content = "Playlist" + i;
                label.Foreground = Brushes.DarkGray;
                label.FontSize = 15;
                label.HorizontalAlignment = HorizontalAlignment.Center;
                label.Margin = new Thickness(0, 0, 0, 0);

                Label subjectLabel = new Label();
                subjectLabel.Content = "type";
                subjectLabel.Foreground = Brushes.DarkGray;
                subjectLabel.FontSize = 12;
                subjectLabel.HorizontalAlignment = HorizontalAlignment.Center;
                subjectLabel.Margin = new Thickness(0, 0, 0, 0);

                border.Child = stack;
                stack.Children.Add(image);
                stack.Children.Add(label);
                stack.Children.Add(subjectLabel);


                DropShadowBitmapEffect myDropShadowEffect = new DropShadowBitmapEffect();
                // Set the color of the shadow to Black.
                Color myShadowColor = new Color();
                myShadowColor.ScA = 1;
                myShadowColor.ScB = 0;
                myShadowColor.ScG = 0;
                myShadowColor.ScR = 0;
                myDropShadowEffect.Color = myShadowColor;

                // Set the direction of where the shadow is cast to 320 degrees.
                myDropShadowEffect.Direction = 320;

                // Set the depth of the shadow being cast.
                myDropShadowEffect.ShadowDepth = 8;

                // Set the shadow softness to the maximum (range of 0-1).
                myDropShadowEffect.Softness = 1;
                // Set the shadow opacity to half opaque or in other words - half transparent.
                // The range is 0-1.
                myDropShadowEffect.Opacity = 0.8;

                // Apply the bitmap effect to the Button.
                border.BitmapEffect = myDropShadowEffect;








                ListBoxItem item = new ListBoxItem();
                item.Content = border;
                item.MouseEnter += (s, e) =>
                {
                    border.Background = Brushes.Red;
                    label.Foreground = Brushes.White;
                };
                item.MouseLeave += (s, e) =>
                {
                    border.Background = Brushes.Transparent;
                    label.Foreground = Brushes.DarkGray;
                };
                albumItemsList.Items.Add(item);
            }
            /*
            for(int i = 0; i < 20; i++)
            {
                ListBoxItem item = new ListBoxItem();
                PlayListItemControl playItem = new PlayListItemControl();
                playItem.numberLabel.Content = i;
                item.Content = playItem;
                playListBox.Items.Add(item);
            }
         */
          }




        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void RootWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            string url = urlInput.Text;

          
             video = await client.GetVideoAsync(YoutubeClient.ParseVideoId(url));

            videoTitle.Content = video.Title; 

            BitmapImage thmbNail = new BitmapImage();
            thmbNail.BeginInit();
            thmbNail.UriSource = new Uri(video.Thumbnails.MediumResUrl, UriKind.Absolute);
            thmbNail.EndInit();
            viewCount.Content = video.Statistics.ViewCount + " views";
            likesCount.Content = video.Statistics.LikeCount;
            dislikesCount.Content = video.Statistics.DislikeCount;
            videoThumbnail.Source = thmbNail;
            videoPublishedDate.Content = video.UploadDate;
            videoDesciption.Content = video.Description;
            videoAuthor.Content = video.Author;
            videoDuration.Content = video.Duration; 
         
           

            _mediaStreamInfos = await client.GetVideoMediaStreamInfosAsync(YoutubeClient.ParseVideoId(url));
             var streaminfo = _mediaStreamInfos.Muxed.WithHighestVideoQuality();
         
            videoAvailable.Content = streaminfo.Container.GetFileExtension();
            soundAvailable.Content = _mediaStreamInfos.GetAll();
            videoURL.Content = url;
            try
            {
                YoutubeClient.ParsePlaylistId(url);
                var playlist = await client.GetPlaylistAsync(YoutubeClient.ParsePlaylistId(url));
                playlistAuthor.Content = playlist.Author;
                playlistTitle.Content = playlist.Title;
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
                
               youtubePlayer.Play();
            }
            catch (Exception ex)
            {

            }

           
        }


        public void encodeToMP4(VideoType type, string inputFile, string outputFile)
        {


            //string inputVideoFile = "C:/Users/steve/Desktop/Private world (Vaporwave mix).webm",
                //   outputVideoFile = "C:/Users/steve/Desktop/Private world (Vaporwave mix).Mp4";
          //  FileInfo outputFile = new FileInfo(outputVideoFile);

         //   var video = VideoInfo.FromPath(inputVideoFile);

         
            // easily track conversion progress
            //video.OnConversionProgress += video_OnConversionProgress;

            // input and output strings are required
            // all other parameters are optional
          /*  video.ConvertTo(VideoType.Mp4, outputFile, Speed.UltraFast,
                VideoSize.Original,
                AudioQuality.Hd,
                true,
                false);*/
            
        }


        private async void DownloadMediaStream(MediaStreamInfo info)
        {
            // Create dialog
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
