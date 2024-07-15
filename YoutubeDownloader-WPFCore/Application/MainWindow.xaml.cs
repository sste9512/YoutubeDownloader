using System.Windows;
using YoutubeDownloader_WPFCore.Application.Aspects.TypeAspects;

namespace YoutubeDownloader_WPFCore.Application;

[ComponentBehaviors]
public partial class MainWindow : Window
{

    public MainWindow()
    {
        /*VideoPanel.UrlInput.LostFocus += UrlInput_OnLostFocus;
        VideoPanel.QueryVideoButton.Click += QueryVideoEvent;*/
    }

    protected override void OnActivated(EventArgs e)
    {
        VideoPanel.UrlInput.LostFocus += UrlInput_OnLostFocus;
        VideoPanel.QueryVideoButton.Click += QueryVideoEvent;
        base.OnActivated(e);
    }

    private async void UrlInput_OnLostFocus(object sender, RoutedEventArgs e)
    {
        /*Console.WriteLine("Lost Focus");
        var text = (TextBox) sender;
        await Send(
            new QueryVideoRequest
            {
                Url = text.Text,
                MainWindowReference = new WeakReference<MainWindow>(this)
            },
            _cancellationTokenSource.Token);*/
    }

    public async void QueryVideoEvent(object sender, RoutedEventArgs e)
    {
        try
        {
            Console.WriteLine("Query Video Event");
            var text = VideoPanel.UrlInput.GetLineText(0);
            /*await Send(
                new QueryVideoRequest
                {
                    Url = text,
                    MainWindowReference = new WeakReference<MainWindow>(this)
                },
                CancellationToken.None);*/
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
        }

    }

    public async void DownloadVideo(object sender, RoutedEventArgs e)
    {
        /*await Publish(
            new DownloadMediaStreamCommand {MainWindowReference = new WeakReference<MainWindow>(this)},
            _cancellationTokenSource.Token);*/
    }

    private void AddPlayList(object sender, RoutedEventArgs e)
    {
        //_windows[nameof(PlaylistCreationWindow)].Show();
    }

    public async void OpenProjectsPath_Click(object sender, RoutedEventArgs e)
    {
        //await _mediator.Publish(new OpenPathsWindowCommand());
    }

    private void CloseButton_OnClick(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private void MinButton_OnClick(object sender, RoutedEventArgs e)
    {
        WindowState = WindowState.Minimized;
    }

    private void MaxButton_OnClick(object sender, RoutedEventArgs e)
    {
        WindowState = WindowState.Maximized;
    }
}