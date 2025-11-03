using System.Windows.Controls;
using System.Windows.Media;

namespace YoutubeDownloader_WPFCore.Controls.PlayList.ViewModel;

/// <summary>
/// Interaction logic for PlayListItemControl.xaml
/// </summary>
public partial class PlayListItemControl : UserControl
{
    public PlayListItemControl()
    {
        InitializeComponent();
        this.MouseEnter += (_, _) =>
        {
            DecoBorder.Background = new SolidColorBrush(Color.FromRgb(60, 60, 60));
        };
        this.MouseLeave += (_, _) =>
        {
            DecoBorder.Background = Brushes.Transparent;
        };
    }
}