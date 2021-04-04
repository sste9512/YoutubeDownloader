using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using YoutubeDownloader.ViewModels;
using YoutubeExplode;
using YoutubeExplode.Models;
using YoutubeExplode.Models.ClosedCaptions;
using YoutubeExplode.Models.MediaStreams;

namespace YoutubeDownloader
{
    public partial class MainWindow : Window
    {
        private readonly MainWindowViewModel _model;
        
        public MainWindow()
        {
            InitializeComponent();
            this._model = new MainWindowViewModel();
            VideoPanel.QueryVideoButton.Click += this.QueryVideoEvent;
            VideoInfoPanel.DownloadButton.Click += this.Download_Video;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void RootWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private async void QueryVideoEvent(object sender, RoutedEventArgs e)
        {
            try
            {
                string url = VideoPanel.UrlInput.Text;
                _model.Video = await _model.Client.GetVideoAsync(YoutubeClient.ParseVideoId(url));
                VideoInfoPanel.SyncInfoToPanel(_model.Video, url, _model.Client, _model.MediaStreamInfos);
                PlayList.InitPlayListFromUrl(url, _model.Client);
            }
            catch (Exception ex)
            {
                // ignored
            }
        }


        private async void DownloadMediaStream(MediaStreamInfo info)
        {
            // Create dialog
            try
            {
                var fileExt = info.Container.GetFileExtension();
                var defaultFileName = _model.Video.Title + "." + fileExt;
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
                _model.IsBusy = true;
               _model.Progress = 0;

                var progressHandler = new Progress<double>(p => _model.Progress = p);
                await _model.Client.DownloadMediaStreamAsync(info, filePath, progressHandler);

                _model.IsBusy = false;
                _model.Progress = 0;
            }
            catch (Exception ex)
            {
                // ignored
            }
        }


        private async void Download_Video(object sender, RoutedEventArgs e)
        {
            await Task.Factory.StartNew(() =>
            {
                DownloadMediaStream(_model.MediaStreamInfos.Muxed.WithHighestVideoQuality());
            });
        }


        private void AddPlayList(object sender, RoutedEventArgs e)
        {
            PlaylistCreationWindow window = new PlaylistCreationWindow();
            window.Show();
        }
    }
}