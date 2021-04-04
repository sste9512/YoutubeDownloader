using System.Diagnostics;
using System.Reflection;
using Autofac;
using Autofac.Diagnostics;
using MediatR;
using MediatR.Extensions.Autofac.DependencyInjection;

namespace YoutubeDownloader.Domain.DependencyInjection
{
    public static class Injector
    {
        private static IContainer _container;

        internal static T Resolve<T>() where T : class
        {
            if (!(_container is null)) return _container.Resolve<T>();
            
            var containerBuilder = new ContainerBuilder();
            
            containerBuilder
                .RegisterAssemblyTypes(typeof(IRequest<>).Assembly)
                .Where(t => t.IsClosedTypeOf(typeof(IRequest<>)))
                .AsImplementedInterfaces();

            containerBuilder
                .RegisterAssemblyTypes(typeof(IRequestHandler<>).Assembly)
                .Where(t => t.IsClosedTypeOf(typeof(IRequestHandler<>)))
                .AsImplementedInterfaces();
            
            containerBuilder.RegisterMediatR(Assembly.GetAssembly(typeof(App)));
            containerBuilder.RegisterModule<TaskModule>();
            containerBuilder.RegisterModule<NetworkModule>();
            containerBuilder.RegisterModule<WindowModule>();
            
            
            _container = containerBuilder.Build();
            
            
            var tracer = new DefaultDiagnosticTracer();
            tracer.OperationCompleted += (sender, args) =>
            {
                Trace.WriteLine(args.TraceContent);
            };
            _container.SubscribeToDiagnostics(tracer);
          
            return _container.Resolve<T>();
        }

        internal static void DisposeOf()
        {
            _container?.Dispose();
        }
    }
}