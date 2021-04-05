using System;
using MediatR;

namespace YoutubeDownloader.Wpf.Application
{
    public class OpenPathsWindowCommand : INotification
    {
        private WeakReference<MainWindow> MainWindowReference { get; set; }
    }
}