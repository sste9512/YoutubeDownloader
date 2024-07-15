using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace YoutubeDownloader_WPFCore.Application.Controls.UserPlayListPanel.View;

/// <summary>
/// Interaction logic for UserPlayListPanel.xaml
/// </summary>
public partial class UserPlayListPanel : UserControl
{
    public UserPlayListPanel()
    {
        InitializeComponent();

        for (var i = 0; i < 20; i++)
        {
            var border = new Border
            {
                BorderBrush = Brushes.Red,
                BorderThickness = new Thickness(0, 0, 0, 0),
                Background = Brushes.Transparent,
                Margin = new Thickness(12, 12, 12, 12),
                CornerRadius = new CornerRadius(5, 5, 5, 5)
            };

            var stack = new StackPanel { Orientation = Orientation.Vertical };
            var image = new Image { Height = 140, Width = 155 };


            var label = new Label
            {
                Content = "Playlist" + i,
                Foreground = Brushes.DarkGray,
                FontSize = 15,
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(0, 0, 0, 0)
            };

            var subjectLabel = new Label
            {
                Content = "type",
                Foreground = Brushes.DarkGray,
                FontSize = 12,
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(0, 0, 0, 0)
            };

            border.Child = stack;
            stack.Children.Add(image);
            stack.Children.Add(label);
            stack.Children.Add(subjectLabel);

            var myDropShadowEffect = new DropShadowBitmapEffect();
            // Set the color of the shadow to Black.
            var myShadowColor = new Color
            {
                ScA = 1, ScB = 0, ScG = 0, ScR = 0
            };
            myDropShadowEffect.Color = myShadowColor;

            // Set the direction of where the shadow is cast to 320 degrees.
            myDropShadowEffect.Direction = 320;

            // Set the depth of the shadow being cast.
            myDropShadowEffect.ShadowDepth = 8;

            // Set the shadow softness to the maximum (range of 0-1).
            myDropShadowEffect.Softness = 1;
            // Set the shadow opacity to half opaque or in other words - half transparent.
            // The range is 0-1.
            myDropShadowEffect.Opacity = 0.8;

            // Apply the bitmap effect to the Button.
            border.BitmapEffect = myDropShadowEffect;

            var item = new ListBoxItem { Content = border };
            item.MouseEnter += (s, e) =>
            {
                border.Background = Brushes.Red;
                label.Foreground = Brushes.White;
            };
            item.MouseLeave += (s, e) =>
            {
                border.Background = Brushes.Transparent;
                label.Foreground = Brushes.DarkGray;
            };
            AlbumItemsList.Items.Add(item);
        }
    }
}

