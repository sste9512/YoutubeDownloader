using Microsoft.Extensions.Logging;
using YoutubeDownloaderMaui.Core.Aspects.TypeAspects;

namespace YoutubeDownloaderMaui
{
    [GraphConstructorAspect]
    [ComponentBehaviors]
    public partial class MainPage : ContentPage
    {
        int count = 0;

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

            var graph = Inject<Dictionary<int, object>>();

            foreach (var item in graph)
            {
                CounterBtn.Text += item.Key + " " + item.Value.GetType().Name + Environment.NewLine;
            }
            
            SemanticScreenReader.Announce(CounterBtn.Text);
        }
    }
}