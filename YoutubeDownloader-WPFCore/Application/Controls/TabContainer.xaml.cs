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

namespace YoutubeDownloader_WPFCore.Application.Controls
{
    /// <summary>
    /// Interaction logic for TabContainer.xaml
    /// </summary>
    public partial class TabContainer : UserControl
    {
        public TabContainer()
        {
            InitializeComponent();
            Do();


        }

        public void Do()
        {
            Tabs.Items.Add(new TabItem
            {
                Header = "Tab",
                Content = new SearchPanel()
            });
        }
    }
}
