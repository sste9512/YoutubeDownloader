using System.IO;
using System.Windows;
using MediatR;
using Microsoft.Win32;
using YoutubeExplode;
using YoutubeExplode.Videos.Streams;

namespace YoutubeDownloader_WPFCore.Core.Behavioural.CQRS.Intents.Queries;

public class DownloadMediaStreamCommand : INotification
{
    public string VideoName { get; set; }
    public IStreamInfo MediaStreamInfo { get; set; } 
    public WeakReference<MainWindow> MainWindowReference { get; set; }
}

public class DownloadMediaStreamCommandHandler(YoutubeClient youtubeClient)
    : INotificationHandler<DownloadMediaStreamCommand>
{
    public async Task Handle(DownloadMediaStreamCommand notification, CancellationToken cancellationToken)
    {
        // Create dialog 
        try
        {
            var fileExt = notification.MediaStreamInfo.Container.Name;
            var defaultFileName = notification.VideoName + "." + fileExt;

            var saveFileDialog = new SaveFileDialog
            {
                AddExtension = true,
                DefaultExt = fileExt,
                FileName = defaultFileName,
                Filter = $"{notification.MediaStreamInfo.Container.Name} Files|*.{fileExt}|All Files|*.*"
            };

            // Select file path
            if (saveFileDialog.ShowDialog() != true)
                return;
            var filePath = saveFileDialog.FileName;

            // Download to file
            // var isBusy = true;
            var progress = 0.0;

            var progressHandler = new Progress<double>(p => progress = p);
            await using var fileStream = File.Create(filePath);
            await youtubeClient.Videos.Streams.CopyToAsync(notification.MediaStreamInfo, fileStream, progressHandler, cancellationToken);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message); 
        }
    }
}