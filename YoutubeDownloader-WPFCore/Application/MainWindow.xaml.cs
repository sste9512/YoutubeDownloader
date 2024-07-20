using System.Windows;
using YoutubeDownloader_WPFCore.Application.Aspects.TypeAspects;
using YoutubeDownloader_WPFCore.Application.Core;
using YoutubeExplode;
using OneOf;

namespace YoutubeDownloader_WPFCore.Application;

//[GraphConstructorAspect]
[ComponentBehaviors]
public partial class MainWindow : Window
{

    private Signaler<string> Signaler;
    private Signaler<OneOf<string, bool>> ValueSignaler;
    
    public MainWindow()
    {
        Signaler = new Signaler<string>(Inject<ChannelFactory>());
        ValueSignaler = new Signaler<OneOf<string, bool>>(Inject<ChannelFactory>());
        
        Signaler.OnSignal(async x =>
        {
            Console.WriteLine("I am the activating Call" + x);
            await Task.Delay(TimeSpan.FromSeconds(2));
        });
        
        ValueSignaler.OnSignal(x =>
        {
            x.Switch(@string =>
                {
                    Console.WriteLine(@string);
                },

                y =>
                {
                    Console.WriteLine(y);
                });
        });
    }

    protected override async void OnActivated(EventArgs e)
    {
        base.OnActivated(e);

        await Signaler.Signal("I am the payload", CancellationToken.None);
    }

    protected override async void OnContentRendered(EventArgs e)
    {
        base.OnContentRendered(e);
        
        await ValueSignaler.Signal("I am the oneof string", CancellationToken.None);
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
        await Signaler.Signal("I am the button click", CancellationToken.None);
        WindowState = WindowState.Minimized;
    }

    private void MaxButton_OnClick(object sender, RoutedEventArgs e)
    {
        WindowState = WindowState.Maximized;
    }
}