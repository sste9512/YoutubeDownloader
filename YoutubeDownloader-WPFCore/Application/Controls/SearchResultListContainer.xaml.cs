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
using YoutubeDownloader_WPFCore.Application.Controls.PlayList.Commands;
using YoutubeExplode;
using YoutubeExplode.Channels;
using YoutubeExplode.Search;

namespace YoutubeDownloader_WPFCore.Application.Controls
{
    /// <summary>
    /// Interaction logic for SearchResultList.xaml
    /// </summary>
    public partial class SearchResultList : UserControl
    {
        [Dependency]
        private YoutubeClient _youtubeClient;

        public SearchResultList()
        {
            InitializeComponent();
        }

        public async void AddItem(ChannelSearchResult channel)
        {
          

            var fullChannel = await _youtubeClient.Channels.GetAsync(channel.Id, CancellationToken.None);

            var icon = fullChannel.Thumbnails[0].Url.BitmapFromUrl();

            var item = new AuthorResultItem
            {
                AuthorName =
                {
                    Content = channel.Title
                },
                AuthorDescription =
                {
                    Content = channel.Url
                },
                AuthIcon =
                {
                    Source = icon
                }
            };


            var listboxItem = new ListBoxItem()
            {
                Content = item
            };
            ListBox.Items.Add(listboxItem);
        }
    }
}