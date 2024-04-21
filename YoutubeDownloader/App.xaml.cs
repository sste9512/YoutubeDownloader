using System;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using YoutubeDownloader.Domain.DependencyInjection;

namespace YoutubeDownloader
{
    public partial class App : Application
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