using System.Windows;
using MediatR;
using YoutubeDownloader_WPFCore.Core.DependencyInjection;

namespace YoutubeDownloader_WPFCore.Core.WpfEnhancement;

public class AppWindow : Window
{
    
      public async Task<T> Send<T>(IRequest<T> request, CancellationToken cancellationToken = default)
      {
          var mediatr = Injector.Resolve<IMediator>();
          return await mediatr.Send(request, cancellationToken);
      }
      
      public async Task Publish(INotification notification, CancellationToken cancellationToken = default)
      {
          var mediatr = Injector.Resolve<IMediator>();
          await mediatr.Publish(notification, cancellationToken);
      }
      
      public T Resolve<T>() where T : class
      {
          return  Injector.Resolve<T>();
      }
}