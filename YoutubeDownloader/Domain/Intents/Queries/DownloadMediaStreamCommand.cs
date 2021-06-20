using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using MediatR;
using Microsoft.Win32;
using YoutubeExplode;
using YoutubeExplode.Models.MediaStreams;

namespace YoutubeDownloader.Domain.Intents.Queries
{
    public class DownloadMediaStreamCommand : INotification
    {
        public string VideoName { get; set; }
        public MediaStreamInfo MediaStreamInfo { get; set; }
        public WeakReference<MainWindow> MainWindowReference { get; set; }
    }

    public class DownloadMediaStreamCommandHandler : INotificationHandler<DownloadMediaStreamCommand>
    {
        
        private readonly YoutubeClient _youtubeClient;

        public DownloadMediaStreamCommandHandler(YoutubeClient youtubeClient)
        {
            _youtubeClient = youtubeClient;
        }

        public async Task Handle(DownloadMediaStreamCommand notification, CancellationToken cancellationToken)
        {
            // Create dialog
            try
            {
                var fileExt = notification.MediaStreamInfo.Container.GetFileExtension();
                var defaultFileName = notification.VideoName + "." + fileExt;

                var saveFileDialog = new SaveFileDialog
                {
                    AddExtension = true,
                    DefaultExt = fileExt,
                    FileName = defaultFileName,
                    Filter = $"{notification.MediaStreamInfo.Container} Files|*.{fileExt}|All Files|*.*"
                };

                // Select file path
                if (saveFileDialog.ShowDialog() != true)
                    return;
                var filePath = saveFileDialog.FileName;

                // Download to file
                // var isBusy = true;
                var progress = 0.0;

                var progressHandler = new Progress<double>(p => progress = p);
                await _youtubeClient.DownloadMediaStreamAsync(notification.MediaStreamInfo, filePath, progressHandler,
                    cancellationToken);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}