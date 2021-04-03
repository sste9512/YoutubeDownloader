using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using Autofac;
using Module = Autofac.Module;

namespace YoutubeDownloader.DependencyInjection
{
    public class WindowModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
          
            builder.Register(x => new MainWindow())
                .OnActivated(x =>
                {
                    x.Instance.CloseButton.Click += x.Instance.QueryVideoEvent;
                    x.Instance. VideoInfoPanel.DownloadButton.Click += x.Instance.DownloadVideo;
                })
                .SingleInstance();
            
            
            var dataAccess = Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(dataAccess)
                .Where(t => t == typeof(Window))
                .PublicOnly()
                .Except<MainWindow>();
            
           
            builder.RegisterAssemblyTypes(dataAccess)
                .Where(t => t == typeof(UserControl))
                .PublicOnly()
                .Except<MainWindow>();
                
         
            base.Load(builder);
        }
    }
}