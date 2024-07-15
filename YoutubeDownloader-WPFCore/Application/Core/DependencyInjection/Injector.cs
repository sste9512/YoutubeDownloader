

namespace YoutubeDownloader_WPFCore.Application.Core.DependencyInjection;


        /*if (!(_container is null)) return _container.Resolve<T>();
            
        var containerBuilder = new ContainerBuilder();
            
        containerBuilder
            .RegisterAssemblyTypes(typeof(IRequest<>).Assembly)
            .Where(t => t.IsClosedTypeOf(typeof(IRequest<>)))
            .AsImplementedInterfaces();

        containerBuilder
            .RegisterAssemblyTypes(typeof(IRequestHandler<>).Assembly)
            .Where(t => t.IsClosedTypeOf(typeof(IRequestHandler<>)))
            .AsImplementedInterfaces();
            
        var configuration = MediatRConfigurationBuilder
            .Create(typeof(App).Assembly)
            .WithRegistrationScope(RegistrationScope.Scoped)
            .WithAllOpenGenericHandlerTypesRegistered()
            .Build();
        
        containerBuilder.RegisterMediatR(configuration);
        containerBuilder.RegisterModule<TaskModule>();
        containerBuilder.RegisterModule<NetworkModule>();
        containerBuilder.RegisterModule<WindowModule>();
            
            
        _container = containerBuilder.Build();
            
            
        var tracer = new DefaultDiagnosticTracer();
        tracer.OperationCompleted += (sender, args) =>
        {
            Trace.WriteLine(args.TraceContent);
        };
        _container.SubscribeToDiagnostics(tracer);*/
          
