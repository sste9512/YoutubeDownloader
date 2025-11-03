using System.Windows.Controls;
using YoutubeDownloader_WPFCore.Controls.PlayList.ViewModel;
using YoutubeExplode;

namespace YoutubeDownloader_WPFCore.Controls.PlayList.View;

public partial class PlayListControl : UserControl
{
    private PlayListControlViewModel? _viewModel;

    public PlayListControl()
    {
        InitializeComponent();
    }

    public void SetViewModel(PlayListControlViewModel viewModel)
    {
        _viewModel = viewModel;
        DataContext = _viewModel;
    }

    public async void InitPlayListFromUrl(string playListUrl, YoutubeClient client)
    {
        if (_viewModel != null)
        {
            await _viewModel.LoadPlaylistFromUrl(playListUrl, client);
        }
    }
}