using Metalama.Extensions.DependencyInjection;
using YoutubeExplode;

namespace YoutubeDownloader_WPFCore.Application.Core;

public partial class YoutubeDownloaderService {
    
    [Dependency]
    private readonly YoutubeClient _youtubeClient;

    public YoutubeDownloaderService(YoutubeClient youtubeClient)
    {
        _youtubeClient = youtubeClient;
    }
    
     
    
    
    
    
    
}