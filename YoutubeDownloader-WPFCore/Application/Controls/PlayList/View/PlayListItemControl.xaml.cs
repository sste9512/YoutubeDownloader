using System.Windows;
using System.Windows.Controls;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using YoutubeExplode.Playlists;
using OneOf;
using YoutubeDownloader_WPFCore.Application.Aspects.TypeAspects;

namespace YoutubeDownloader_WPFCore.Application.Controls.PlayList.View;



/// <summary>
/// Interaction logic for PlayListItemControl.xaml
/// </summary>
[Signaler]
public partial class PlayListItemControl : UserControl
{
    

    private PlaylistVideo _playlistVideo { get; set; }

    public PlayListItemControl()
    {
        InitializeComponent();

        /*_ = Id<int>(_playlistVideo.Id) >> (x =>
        {

        });*/
        
        
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