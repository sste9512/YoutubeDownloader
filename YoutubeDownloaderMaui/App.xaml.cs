using YoutubeDownloaderMaui.Core.Aspects.TypeAspects;

namespace YoutubeDownloaderMaui
{
    [GraphConstructorAspect]
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}
