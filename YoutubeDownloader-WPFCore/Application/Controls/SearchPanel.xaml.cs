using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Metalama.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using YoutubeExplode;
using YoutubeExplode.Common;

namespace YoutubeDownloader_WPFCore.Application.Controls
{
    /// <summary>
    /// Interaction logic for SearchPanel.xaml
    /// </summary>
    public partial class SearchPanel : UserControl
    {
        [Dependency]
        private YoutubeClient _youtubeClient;

        [Dependency]
        private ILogger<SearchPanel> _logger;

        public SearchPanel()
        {
            InitializeComponent();
        }

        private async void QueryVideoButton_OnClick(object sender, RoutedEventArgs e)
        {
            var input = UrlInput.Text.Trim();

            await foreach (var search in  _youtubeClient.Search.GetResultBatchesAsync(input, CancellationToken.None))
            {
                foreach (var searchResult in search.Items)
                {
                    searchResult.
                }
            }

            var author = await _youtubeClient.Search.GetChannelsAsync(input);

            foreach (var results in author)
            {
                 _logger.LogInformation(results.Title);
                 SearchResultListContainer.AddItem(results);
            }
        }
    }
}
