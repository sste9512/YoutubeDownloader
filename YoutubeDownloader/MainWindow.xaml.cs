using System;
using System.Threading;
using System.Windows;
using Autofac.Features.Indexed;
using Castle.Core.Logging;
using MediatR;
using YoutubeDownloader.Domain.Intents.Queries;
using YoutubeDownloader.Windows.PlaylistCreationWindow.View;

namespace YoutubeDownloader
{
    public partial class MainWindow : Window
    {
        private readonly IMediator _mediator;
        private readonly IIndex<string, Window> _windows;
        private readonly ILogger _logger;
        private readonly CancellationTokenSource _cancellationTokenSource;

        public MainWindow()
        {
        }

        public MainWindow(IMediator mediator, IIndex<string, Window> windows, ILogger logger,
            CancellationTokenSource cancellationTokenSource)
        {
            _mediator = mediator;
            _windows = windows;
            _logger = logger;
            _cancellationTokenSource = cancellationTokenSource;
        }

        public async void QueryVideoEvent(object sender, RoutedEventArgs e)
        {
            var video = await _mediator.Send(
                new QueryVideoRequest {MainWindowReference = new WeakReference<MainWindow>(this)},
                _cancellationTokenSource.Token);
        }

        public async void DownloadVideo(object sender, RoutedEventArgs e)
        {
            await _mediator.Publish(
                new DownloadMediaStreamCommand {MainWindowReference = new WeakReference<MainWindow>(this)},
                _cancellationTokenSource.Token);
        }

        private void AddPlayList(object sender, RoutedEventArgs e)
        {
            _windows[nameof(PlaylistCreationWindow)].Show();
        }

        public async void OpenProjectsPath_Click(object sender, RoutedEventArgs e)
        {
            await _mediator.Publish(new OpenPathsWindowCommand());
        }
    }
}