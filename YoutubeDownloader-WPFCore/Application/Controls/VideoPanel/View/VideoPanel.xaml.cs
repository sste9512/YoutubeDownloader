using System.Windows;
using System.Windows.Controls;

namespace YoutubeDownloader_WPFCore.Application.Controls.VideoPanel.View;

/// <summary>
/// Interaction logic for VideoPanel.xaml
/// </summary>
public partial class VideoPanel : UserControl
{
    public VideoPanel()
    {
            InitializeComponent();
        }

    private void UrlInput_OnLostFocus(object sender, RoutedEventArgs e)
    {
         
    }
}