using System.Windows;
using System.Windows.Input;

namespace YoutubeDownloader_WPFCore.Features.PlaylistCreationWindow.View;

/// <summary>
/// Interaction logic for PlaylistCreationWindow.xaml
/// </summary>
public partial class PlaylistCreationWindow : Window
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlaylistCreationWindow"/> class.
    /// </summary>
    public PlaylistCreationWindow()
    {
        InitializeComponent();
    }

    /// <summary>
    /// The RootWindow_MouseDown
    /// </summary>
    /// <param name="sender">The <see cref="object"/></param>
    /// <param name="e">The <see cref="MouseButtonEventArgs"/></param>
    private void RootWindow_MouseDown(object sender, MouseButtonEventArgs e)
    {
        this.DragMove();
    }

    /// <summary>
    /// The CloseButton_Click
    /// </summary>
    /// <param name="sender">The <see cref="object"/></param>
    /// <param name="e">The <see cref="RoutedEventArgs"/></param>
    private void CloseButton_Click(object sender, RoutedEventArgs e)
    {
        this.Close();
    }
}