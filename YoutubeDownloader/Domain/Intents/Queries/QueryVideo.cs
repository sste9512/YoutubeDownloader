using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Castle.Core.Logging;
using MediatR;
using YoutubeDownloader.Controls.VideoInfoPanel.View;
using YoutubeDownloader.Controls.VideoPanel.View;
using YoutubeExplode;
using YoutubeExplode.Models;

namespace YoutubeDownloader.Domain.Intents.Queries
{
    public class QueryVideoRequest : IRequest<Video>
    {
        public string Url { get; set; }
        public WeakReference<MainWindow> MainWindowReference { get; set; }
    }

    public class QueryVideoRequestHandler : IRequestHandler<QueryVideoRequest, Video>
    {
        private readonly YoutubeClient _youtubeClient;
        private readonly ILogger _logger;

        public QueryVideoRequestHandler(YoutubeClient youtubeClient, ILogger logger)
        {
            _youtubeClient = youtubeClient;
            _logger = logger;
        }

        public async Task<Video> Handle(QueryVideoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var video = await _youtubeClient.GetVideoAsync(YoutubeClient.ParseVideoId(request.Url));
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
}