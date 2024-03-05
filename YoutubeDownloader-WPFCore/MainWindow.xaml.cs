using System.Windows;
using Autofac.Features.Indexed;
using YoutubeDownloader_WPFCore.Core.Behavioural.CQRS.Intents.Queries;
using YoutubeDownloader_WPFCore.Core.WpfEnhancement;
using YoutubeDownloader_WPFCore.Features.PlaylistCreationWindow.View;

namespace YoutubeDownloader_WPFCore;

public partial class MainWindow : AppWindow
{
  
    private readonly IIndex<string, Window> _windows;
    private readonly CancellationTokenSource _cancellationTokenSource;

    public MainWindow()
    {
    }

    public MainWindow( IIndex<string, Window> windows,
        CancellationTokenSource cancellationTokenSource)
    {
        _windows = windows;
        _cancellationTokenSource = cancellationTokenSource;
    }

    public async void QueryVideoEvent(object sender, RoutedEventArgs e)
    {
        await Send(
            new QueryVideoRequest {MainWindowReference = new WeakReference<MainWindow>(this)},
            _cancellationTokenSource.Token);
    }

    public async void DownloadVideo(object sender, RoutedEventArgs e)
    {
        await Publish(
            new DownloadMediaStreamCommand {MainWindowReference = new WeakReference<MainWindow>(this)},
            _cancellationTokenSource.Token);
    }

    private void AddPlayList(object sender, RoutedEventArgs e)
    {
        _windows[nameof(PlaylistCreationWindow)].Show();
    }

    public async void OpenProjectsPath_Click(object sender, RoutedEventArgs e)
    {
        //await _mediator.Publish(new OpenPathsWindowCommand());
    }
}