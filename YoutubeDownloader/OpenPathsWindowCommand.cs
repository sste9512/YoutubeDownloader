using System;
using MediatR;

namespace YoutubeDownloader
{
    public class OpenPathsWindowCommand : INotification
    {
        private WeakReference<MainWindow> MainWindowReference { get; set; }
    }
}