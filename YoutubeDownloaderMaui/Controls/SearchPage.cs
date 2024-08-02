using YoutubeDownloaderMaui.Core.Aspects.TypeAspects;

namespace YoutubeDownloaderMaui.Controls;

public class SearchPage : ContentPage
{
 
    public SearchPage()
    {
        Content = new VerticalStackLayout
        {
            Children =
            {
                new Label
                {
                    HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center,
                    Text = "Welcome to .NET MAUI!"
                },
                new SearchBar()
                {
                      Text = "Search here"
                }
            }
        };
    }
}