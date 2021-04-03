using Microsoft.Win32;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Autofac.Features.Indexed;
using MediatR;
using YoutubeDownloader.Queries;
using YoutubeDownloader.ViewModels;
using YoutubeExplode.Models.MediaStreams;

namespace YoutubeDownloader
{
    public partial class MainWindow : Window
    {
        private readonly IMediator _mediator;
        private readonly IIndex<string, Window> _windows;
        private readonly MainWindowViewModel _model;
        
        public MainWindow()
        {
            InitializeComponent();
        }

        public MainWindow(IMediator mediator, IIndex<string, Window> windows)
        {
            InitializeComponent();
            _mediator = mediator;
            _windows = windows;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void RootWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        public async void QueryVideoEvent(object sender, RoutedEventArgs e)
        {
            var video = await _mediator.Send(new QueryVideoRequest
            {
                Url = VideoPanel.UrlInput.Text
            });
            
           // VideoInfoPanel.SyncInfoToPanel(video, VideoPanel.UrlInput.Text, _model.Client, _model.MediaStreamInfos);
           // PlayList.InitPlayListFromUrl(url, _model.Client);
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
               //  _model.IsBusy = true;
               // _model.Progress = 0;
               //
               //  var progressHandler = new Progress<double>(p => _model.Progress = p);
               //  await _model.Client.DownloadMediaStreamAsync(info, filePath, progressHandler);
               //
               //  _model.IsBusy = false;
               //  _model.Progress = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        public async void DownloadVideo(object sender, RoutedEventArgs e)
        {
            await Task.Run(() =>
            {
                DownloadMediaStream(_model.MediaStreamInfos.Muxed.WithHighestVideoQuality());
            });
        }


        private void AddPlayList(object sender, RoutedEventArgs e)
        {
            _windows[nameof(PlaylistCreationWindow)].Show();
        }
    }
}