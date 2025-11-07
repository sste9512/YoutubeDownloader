using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Web.WebView2.Core;
using YoutubeDownloader_WPFCore.Controls.VideoPanel.ViewModel;

namespace YoutubeDownloader_WPFCore.Controls.VideoPanel.View;

/// <summary>
/// Interaction logic for VideoPanel.xaml
/// </summary>
public partial class VideoPanel : UserControl
{
    private bool _webViewInitialized;

    public VideoPanel()
    {
        InitializeComponent();
        Loaded += OnLoaded;
    }

    private async void OnLoaded(object sender, RoutedEventArgs e)
    {
        if (_webViewInitialized)
            return;

        var initResult = await InitializeWebView2Async();
        if (!initResult)
        {
            System.Diagnostics.Debug.WriteLine("Failed to initialize WebView2 for VideoPanel.");
        }
        else
        {
            _webViewInitialized = true;
        }
    }

    private async ValueTask<bool> InitializeWebView2Async(CancellationToken cancellationToken = default)
    {
        try
        {
            // Ensure a persistent user data folder for WebView2 to avoid runtime issues
            var localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var userDataFolder = Path.Combine(localAppData, "YoutubeDownloader", "WebView2");
            Directory.CreateDirectory(userDataFolder);

            var env = await CoreWebView2Environment.CreateAsync(userDataFolder: userDataFolder);
            await YoutubePlayer.EnsureCoreWebView2Async(env);

            var settings = YoutubePlayer.CoreWebView2.Settings;
            settings.AreDefaultContextMenusEnabled = false;
            settings.AreDevToolsEnabled = false;
            settings.IsStatusBarEnabled = false;
            settings.IsBuiltInErrorPageEnabled = true;
            settings.IsPinchZoomEnabled = true;
            settings.IsSwipeNavigationEnabled = true;
            settings.IsPasswordAutosaveEnabled = false;
            settings.IsGeneralAutofillEnabled = false;

            // Hint to allow autoplay via query param on YouTube embed; WebView2 generally respects site behavior
            // No explicit autoplay setting currently exposed in stable SDK

            return true;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"WebView2 initialization error: {ex.Message}");
            return false;
        }
    }
}