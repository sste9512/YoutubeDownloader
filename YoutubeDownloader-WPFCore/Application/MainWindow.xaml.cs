using System.Windows;
using YoutubeDownloader_WPFCore.Application.Aspects.TypeAspects;

namespace YoutubeDownloader_WPFCore.Application;

[ComponentBehaviors]
public partial class MainWindow : Window
{

    public MainWindow()
    {
        
    }

    protected override async void OnActivated(EventArgs e)
    {
        base.OnActivated(e);

       
    }

    protected override async void OnContentRendered(EventArgs e)
    {
        base.OnContentRendered(e);
        
    }

    private async void UrlInput_OnLostFocus(object sender, RoutedEventArgs e)
    {
    }

    public async void QueryVideoEvent(object sender, RoutedEventArgs e)
    {
    }

    public async void DownloadVideo(object sender, RoutedEventArgs e)
    {
    }

    private void AddPlayList(object sender, RoutedEventArgs e)
    {
    }

    public async void OpenProjectsPath_Click(object sender, RoutedEventArgs e)
    {
    }

    private void CloseButton_OnClick(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private async void MinButton_OnClick(object sender, RoutedEventArgs e)
    {
        WindowState = WindowState.Minimized;
    }

    private void MaxButton_OnClick(object sender, RoutedEventArgs e)
    {
        WindowState = WindowState.Maximized;
    }
}