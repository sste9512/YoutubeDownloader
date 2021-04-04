using System.Windows.Controls;

namespace YoutubeDownloader.Controls.PlayList.ViewModel
{
    /// <summary>
    /// Interaction logic for PlayListItemControl.xaml
    /// </summary>
    public partial class PlayListItemControl : UserControl
    {
        public PlayListItemControl()
        {
            InitializeComponent();
            this.MouseEnter += (s, e) =>
            {
                //decoBorder.Background = Brushes.DarkGray;
            };
            this.MouseLeave += (s, e) =>
            {
                // decoBorder.Background = Brushes.Transparent;
            };
        }
    }
}