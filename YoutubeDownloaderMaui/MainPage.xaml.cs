using Microsoft.Extensions.Logging;

namespace YoutubeDownloaderMaui
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        [Meta.Dependency] private ILogger _logger;


        public MainPage()
        {
            InitializeComponent();
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            _logger.LogInformation(CounterBtn.Text);
            SemanticScreenReader.Announce(CounterBtn.Text);
        }
    }
}