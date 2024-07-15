using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;

namespace YoutubeDownloader_WPFCore.Application.Controls.MenuPanel.View;

/// <summary>
/// Interaction logic for MenuPanel.xaml
/// </summary>
public partial class MenuPanel : UserControl
{
    public MenuPanel()
    {
        InitializeComponent();
    }

    private void OpenFolder_OnClick(object sender, RoutedEventArgs e)
    {
        var openFileDialog = new OpenFileDialog();
        if (openFileDialog.ShowDialog() == true)
        {
            
        }
    }
}