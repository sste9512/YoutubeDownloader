using CleanArchitecture.Application;
using Metalama.Extensions.DependencyInjection.ServiceLocator;
using Microsoft.Extensions.Logging;

namespace YoutubeDownloaderMaui
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            builder.Services.AddLogging();
#if DEBUG
    		builder.Logging.AddDebug();
#endif
            builder.Services.AddApplicationServices();
            var app = builder.Build();

            ServiceProviderProvider.ServiceProvider = () => app.Services;
            return app;
        }
    }
}
