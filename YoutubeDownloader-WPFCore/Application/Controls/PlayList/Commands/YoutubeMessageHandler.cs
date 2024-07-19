using Polly;
using Polly.Registry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace YoutubeDownloader_WPFCore.Application.Controls.PlayList.Commands
{
    public class YoutubeMessageHandler(ResiliencePipelineProvider<string> pipelineProvider) : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var retry = pipelineProvider.GetPipeline("youtube-download-strategy");
            return await retry.ExecuteAsync(async x => await base.SendAsync(request, cancellationToken), cancellationToken);
        }
    }
}
