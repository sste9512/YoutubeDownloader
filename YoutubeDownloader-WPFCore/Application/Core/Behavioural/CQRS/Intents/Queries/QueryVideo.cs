using System.Windows;
using MediatR;
using YoutubeExplode;
using YoutubeExplode.Videos;

namespace YoutubeDownloader_WPFCore.Application.Core.Behavioural.CQRS.Intents.Queries;

public sealed class QueryVideoRequest : IRequest<Video>
{
    public string Url { get; init; }
    public WeakReference<MainWindow> MainWindowReference { get; init; }
}

public sealed class QueryVideoRequestHandler(YoutubeClient youtubeClient)
    : IRequestHandler<QueryVideoRequest, Video>
{
  

    public async Task<Video> Handle(QueryVideoRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var video = await youtubeClient.Videos.GetAsync(request.Url, cancellationToken);
            Console.WriteLine(video.Description);
            request.MainWindowReference.TryGetTarget(out MainWindow target);
            target.VideoInfoPanel.SyncInfoToPanel(video, target.VideoPanel.UrlInput.Text, youtubeClient);
            target.PlayList.InitPlayListFromUrl(request.Url, youtubeClient);
            return video;
        }
        catch (Exception exception)
        {
            MessageBox.Show(exception.Message);
            return null;
        }
    }
}