using System.Windows.Controls;
using YoutubeExplode.Models;

namespace YoutubeDownloader.PlayList.ViewModel
{
    internal class PlayListItem : ListBoxItem
    {
        public Video Video
        {
            get; set;
        }

        public PlayListItem(Video video)
        {
            this.Video = video;
        }

        public void SetupPlayListItem()
        {
            /* Border border = new Border();
             border.BorderBrush = Brushes.Red;
             border.BorderThickness = new Thickness(0, 0, 0, 0);
             border.Background = Brushes.Transparent;
             border.Margin = new Thickness(12, 12, 12, 12);
             border.CornerRadius = new CornerRadius(5, 5, 5, 5);

             StackPanel stack = new StackPanel();
             stack.Orientation = Orientation.Horizontal;

             Image numImage = new Image();
             numImage.Height = 30;
             numImage.Width = 30;

             Image thumbNail = new Image();
             thumbNail.Height = 30;
             thumbNail.Width = 30;

             StackPanel titleStack = new StackPanel();
             titleStack.Orientation = Orientation.Vertical;

             Label label = new Label();
             label.Content = "Item";
             label.Foreground = Brushes.DarkGray;
             label.FontSize = 15;
             label.HorizontalAlignment = HorizontalAlignment.Center;

             border.Child = stack;

             stack.Children.Add(image);
             stack.Children.Add(label);

             DropShadowBitmapEffect myDropShadowEffect = new DropShadowBitmapEffect();
             // Set the color of the shadow to Black.
             Color myShadowColor = new Color();
             myShadowColor.ScA = 1;
             myShadowColor.ScB = 0;
             myShadowColor.ScG = 0;
             myShadowColor.ScR = 0;
             myDropShadowEffect.Color = myShadowColor;

             // Set the direction of where the shadow is cast to 320 degrees.
             myDropShadowEffect.Direction = 320;

             // Set the depth of the shadow being cast.
             myDropShadowEffect.ShadowDepth = 8;

             // Set the shadow softness to the maximum (range of 0-1).
             myDropShadowEffect.Softness = 1;
             myDropShadowEffect.Opacity = 0.8;

             // Apply the bitmap effect to the Button.
             border.BitmapEffect = myDropShadowEffect;

             this.Content = border;
             this.MouseEnter += (s, e) =>
             {
                 border.Background = Brushes.Red;
                 label.Foreground = Brushes.White;
             };
             this.MouseLeave += (s, e) =>
             {
                 border.Background = Brushes.Transparent;
                 label.Foreground = Brushes.DarkGray;
             };
           */
        }
    }
}