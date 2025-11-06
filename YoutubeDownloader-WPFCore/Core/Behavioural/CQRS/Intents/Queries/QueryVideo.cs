using System.Windows;
using MediatR;
using Microsoft.Extensions.Logging;

using YoutubeExplode;
using YoutubeExplode.Videos;


namespace YoutubeDownloader_WPFCore.Core.Behavioural.CQRS.Intents.Queries;

public sealed class QueryVideoRequest : IRequest<Video>
{
    public string Url { get; set; }
    public WeakReference<MainWindow> MainWindowReference { get; set; }
}

public sealed class QueryVideoRequestHandler(YoutubeClient youtubeClient, ILogger logger)
    : IRequestHandler<QueryVideoRequest, Video>
{
    private readonly ILogger _logger = logger;

    public async Task<Video> Handle(QueryVideoRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var video = await youtubeClient.Videos.GetAsync(request.Url, cancellationToken);
           
            // request.MainWindowReference.VideoInfoPanel.SyncInfoToPanel(video, VideoPanel.UrlInput.Text, _model.Client, _model.MediaStreamInfos));
            // PlayList.InitPlayListFromUrl(url, _model.Client);
            return video;
        }
        catch (Exception exception)
        {
            MessageBox.Show(exception.Message);
            return null;
        }
    }
}