using System.Windows;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Events;
using Prism.Mvvm;
using SurrealDb.Embedded.RocksDb;
using YoutubeDownloader_WPFCore.Controls.PlayList.View;
using YoutubeExplode;
using YoutubeDownloader_WPFCore.Controls.VideoPanel.ViewModel;
using YoutubeDownloader_WPFCore.Controls.PlayList.ViewModel;
using YoutubeDownloader_WPFCore.Controls.UserPlayListPanel.View;
using YoutubeDownloader_WPFCore.Controls.VideoInfoPanel.View;
using YoutubeDownloader_WPFCore.Controls.VideoInfoPanel.ViewModel;
using YoutubeDownloader_WPFCore.Controls.VideoPanel.View;

namespace YoutubeDownloader_WPFCore;

/// <summary>
/// Interaction logic for App.xaml integrating Prism
/// </summary>
public partial class App : PrismApplication
{
    protected override Window CreateShell()
    {
        // Resolve MainWindow from Autofac container which has all the necessary registrations
        return Core.DependencyInjection.Injector.Resolve<MainWindow>();
    }

    protected override void RegisterTypes(IContainerRegistry containerRegistry)
    {
        // MainWindow is registered in Autofac's WindowModule, so we don't register it here
        // Register views/services for Prism navigation or DI here if needed.


        // Configure ViewModelLocationProvider
        // ViewModelLocationProvider.Register<MainWindow, MainWindowViewModel>();
        ViewModelLocationProvider.Register<VideoPanel, VideoPanelViewModel>();
        ViewModelLocationProvider.Register<PlayListControl, PlayListControlViewModel>();
        ViewModelLocationProvider.Register<VideoInfoPanel, VideoInfoPanelViewModel>();
        // ViewModelLocationProvider.Register<UserPlayListPanel, UserPlayListPanelViewModel>();

        // Register Prism's EventAggregator as singleton (should be auto-registered, but explicit for clarity)
        containerRegistry.RegisterSingleton<IEventAggregator, EventAggregator>();

        // Register YoutubeClient as a singleton instance with cookies disabled to avoid empty-domain cookie errors
        var ytHandler = new System.Net.Http.SocketsHttpHandler
        {
            UseCookies = false
        };
        var ytHttpClient = new System.Net.Http.HttpClient(ytHandler);
        containerRegistry.RegisterInstance(new YoutubeClient(ytHttpClient));
        containerRegistry.RegisterInstance(new SurrealDbRocksDbClient("data/rocksdb"));

        // Register ViewModels for dependency injection
        //   containerRegistry.Register<MainWindowViewModel>();
        containerRegistry.Register<VideoPanelViewModel>();
        containerRegistry.Register<PlayListControlViewModel>();
        containerRegistry.Register<VideoInfoPanelViewModel>();
        //   containerRegistry.Register<UserPlayListPanelViewModel>();
    }

    protected override void OnExit(ExitEventArgs e)
    {
        // Dispose old Autofac container if used elsewhere
        Core.DependencyInjection.Injector.DisposeOf();
        base.OnExit(e);
    }
}