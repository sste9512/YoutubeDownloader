using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using Autofac;
using Autofac.Features.Indexed;
using MediatR;
using Module = Autofac.Module;

namespace YoutubeDownloader_WPFCore.Core.DependencyInjection;

public class WindowModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.Register<MainWindow>(x => 
                new MainWindow(x.Resolve<IIndex<string, Window>>(), 
                    x.Resolve<CancellationTokenSource>()))
            .OnActivated(x =>
            {
                x.Instance.InitializeComponent();
                x.Instance.CloseButton.Click += (sender, args) => { x.Instance.Close(); };
                x.Instance.RootWindow.MouseDown += (sender, args) => { x.Instance.DragMove(); };
                x.Instance.VideoInfoPanel.DownloadButton.Click += x.Instance.DownloadVideo;
                x.Instance.MenuPanel.ManageProjectsMenuItem.Click += x.Instance.OpenProjectsPath_Click;
            })
            .SingleInstance();


        var dataAccess = Assembly.GetExecutingAssembly();

        builder.RegisterAssemblyTypes(dataAccess)
            .Where(t => t == typeof(Window))
            .Keyed<Window>(x => nameof(x))
            .PublicOnly()
            .Except<MainWindow>();
            
        var targetAssembly = Assembly.GetExecutingAssembly();
        var subtypes = targetAssembly.GetTypes().Where(t => t.IsSubclassOf(typeof(Window)));
            
        foreach (var form in subtypes)
        {
            builder.RegisterType(form).Keyed<Window>(form.Name);
        }
            

        builder.RegisterAssemblyTypes(dataAccess)
            .Where(t => t == typeof(UserControl))
            .PublicOnly()
            .Except<MainWindow>();


        base.Load(builder);
    }
}