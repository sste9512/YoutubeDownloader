using System.Windows.Controls;
using YoutubeDownloader_WPFCore.Controls.PlayList.ViewModel;
using YoutubeExplode;

namespace YoutubeDownloader_WPFCore.Controls.PlayList.View;

public partial class PlayListControl : UserControl
{
    private readonly PlayListControlViewModel _viewModel;

    public PlayListControl()
    {
        InitializeComponent();
        _viewModel = new PlayListControlViewModel();
        DataContext = _viewModel;
    }

    public async void InitPlayListFromUrl(string playListUrl, YoutubeClient client)
    {
        await _viewModel.LoadPlaylistFromUrl(playListUrl, client);
    }
}