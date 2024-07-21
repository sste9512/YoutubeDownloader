using Polly.Registry;

namespace CleanArchitecture.Infrastructure.Persistence.Network
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
