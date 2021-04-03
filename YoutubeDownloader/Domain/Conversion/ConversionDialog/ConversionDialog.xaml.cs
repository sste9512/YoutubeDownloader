using System.Windows;
using System.Windows.Input;

namespace YoutubeDownloader.Domain.Conversion.ConversionDialog
{
    /// <summary>
    /// Interaction logic for ConversionDialog.xaml
    /// </summary>
    public partial class ConversionDialog : Window
    {
        public ConversionDialog()
        {
            InitializeComponent();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void RootWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

    }
}
