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
using System.Windows.Shapes;

namespace YoutubeDownloader.Conversion.ConversionDialog
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
