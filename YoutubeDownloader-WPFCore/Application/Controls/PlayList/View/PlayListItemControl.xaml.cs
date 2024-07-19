using System.Windows;
using System.Windows.Controls;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using YoutubeExplode.Playlists;
using OneOf;

namespace YoutubeDownloader_WPFCore.Application.Controls.PlayList.View;

public sealed class NotificationHub : Hub
{
    
    public Task NotifyAll(OneOf<bool, string> notification) =>
        Clients.All.SendAsync("NotificationReceived", notification);
}

public sealed class NotificationService(IHubContext<NotificationHub> hubContext)
{
    public Task SendNotificationAsync(OneOf<bool, string> notification) =>
        hubContext.Clients.All.SendAsync("NotificationReceived", notification);
}

/// <summary>
/// Interaction logic for PlayListItemControl.xaml
/// </summary>
public partial class PlayListItemControl : UserControl
{
    

    private PlaylistVideo _playlistVideo { get; set; }

    public PlayListItemControl()
    {
        InitializeComponent();

        MouseEnter += (s, e) =>
        {
            //decoBorder.Background = Brushes.DarkGray;
        };
        MouseLeave += (s, e) =>
        {
            // decoBorder.Background = Brushes.Transparent;
        };
    }


    public void StopProgress()
    {
        ProgressBar.Visibility = Visibility.Hidden;
    }

    public void StartProgress()
    {
        ProgressBar.Visibility = Visibility.Visible;
    }

}