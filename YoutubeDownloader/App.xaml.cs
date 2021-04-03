

using System;
using System.Windows;
using Autofac;
using Microsoft.Extensions.DependencyInjection;
using YoutubeDownloader.Config;
using YoutubeDownloader.DependencyInjection;

namespace YoutubeDownloader
{
    public partial class App : Application
    {
        
        private ServiceProvider serviceProvider;
        
        
        public App()
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            serviceProvider = services.BuildServiceProvider();
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

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);

           // MessageBox.Show("This is the activated lifecycle");
        }

        protected override void OnSessionEnding(SessionEndingCancelEventArgs e)
        {
            base.OnSessionEnding(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            Injector.DisposeOf();
            base.OnExit(e);
        }
    }
}