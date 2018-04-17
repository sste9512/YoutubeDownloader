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
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace YoutubeDownloader.UserPlayListPanel.View
{
    /// <summary>
    /// Interaction logic for UserPlayListPanel.xaml
    /// </summary>
    public partial class UserPlayListPanel : UserControl
    {
        public UserPlayListPanel()
        {
            InitializeComponent();
            for (int i = 0; i < 20; i++)
            {
                Border border = new Border();
                border.BorderBrush = Brushes.Red;
                border.BorderThickness = new Thickness(0, 0, 0, 0);
                border.Background = Brushes.Transparent;
                border.Margin = new Thickness(12, 12, 12, 12);
                border.CornerRadius = new CornerRadius(5, 5, 5, 5);
                StackPanel stack = new StackPanel();
                stack.Orientation = Orientation.Vertical;
                Image image = new Image();

                image.Height = 140;
                image.Width = 155;


                Label label = new Label();
                label.Content = "Playlist" + i;
                label.Foreground = Brushes.DarkGray;
                label.FontSize = 15;
                label.HorizontalAlignment = HorizontalAlignment.Center;
                label.Margin = new Thickness(0, 0, 0, 0);

                Label subjectLabel = new Label();
                subjectLabel.Content = "type";
                subjectLabel.Foreground = Brushes.DarkGray;
                subjectLabel.FontSize = 12;
                subjectLabel.HorizontalAlignment = HorizontalAlignment.Center;
                subjectLabel.Margin = new Thickness(0, 0, 0, 0);

                border.Child = stack;
                stack.Children.Add(image);
                stack.Children.Add(label);
                stack.Children.Add(subjectLabel);


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
                // Set the shadow opacity to half opaque or in other words - half transparent.
                // The range is 0-1.
                myDropShadowEffect.Opacity = 0.8;

                // Apply the bitmap effect to the Button.
                border.BitmapEffect = myDropShadowEffect;








                ListBoxItem item = new ListBoxItem();
                item.Content = border;
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
                this.albumItemsList.Items.Add(item);
            }
            /*
            for(int i = 0; i < 20; i++)
            {
                ListBoxItem item = new ListBoxItem();
                PlayListItemControl playItem = new PlayListItemControl();
                playItem.numberLabel.Content = i;
                item.Content = playItem;
                playListBox.Items.Add(item);
            }
         */
        }


        public async void initListFromUserPlayList()
        {



        }
    }
}
