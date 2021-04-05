using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Win32;

namespace YoutubeDownloader.Wpf.Application
{
    public class OpenPathsWindowCommandHandler : INotificationHandler<OpenPathsWindowCommand>
    {
        public Task Handle(OpenPathsWindowCommand notification, CancellationToken cancellationToken)
        {
            var openDialogCommand = new OpenFileDialog();
            openDialogCommand.ShowDialog();
            return Task.CompletedTask;
        }
    }
}