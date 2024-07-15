using System.Windows;
using MediatR;
using YoutubeExplode;

namespace YoutubeDownloader_WPFCore.Application.Core.Behavioural.CQRS.Intents.Queries;

public class DownloadMediaStreamCommand : INotification
{
    public string? VideoName { get; set; }
   // public MediaStreamInfo? MediaStreamInfo { get; set; }
    public WeakReference<MainWindow>? MainWindowReference { get; set; }
}

public sealed class DownloadMediaStreamCommandHandler(YoutubeClient youtubeClient)
    : INotificationHandler<DownloadMediaStreamCommand>
{
    public async Task Handle(DownloadMediaStreamCommand notification, CancellationToken cancellationToken)
    {
        // Create dialog
        try
        {
            /*var fileExt = notification.MediaStreamInfo?.Container.GetFileExtension();
            var defaultFileName = notification.VideoName + "." + fileExt;

            var saveFileDialog = new SaveFileDialog
            {
                AddExtension = true,
                DefaultExt = fileExt,
                FileName = defaultFileName,
                Filter = $"{notification.MediaStreamInfo!.Container} Files|*.{fileExt}|All Files|*.*"
            };

            // Select file path
            if (saveFileDialog.ShowDialog() != true)
                return;
            var filePath = saveFileDialog.FileName;

            // Download to file
            // var isBusy = true;
            var progress = 0.0;

            var progressHandler = new Progress<double>(p => progress = p);
            await youtubeClient.DownloadMediaStreamAsync(notification.MediaStreamInfo, filePath, progressHandler,
                cancellationToken);*/
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }
}