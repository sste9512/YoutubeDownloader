using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using YoutubeDownloader.Wpf.Application.Domain.DependencyInjection;

namespace YoutubeDownloader.Wpf.Application
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        
        private ServiceProvider _serviceProvider;


        public App()
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            _serviceProvider = services.BuildServiceProvider();
        }

        private void ConfigureServices(ServiceCollection services)
        {
            
            
        }


        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var mainWindow = Injector.Resolve<MainWindow>();

            mainWindow.Show();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            Injector.DisposeOf();
            base.OnExit(e);
        }
    }
}