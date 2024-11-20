using Microsoft.AspNetCore.Mvc;
using YoutubeExplode;
using YoutubeExplode.Videos;

namespace youtubedownloader_vue.Server.Controllers;

public sealed class VideoController : ApiControllerBase
{
    [HttpGet("[action]")]
    public async ValueTask<Video> GetAsync([FromServices]YoutubeClient youtubeClient,string videoId, CancellationToken cancellationToken)
    {
        return await youtubeClient.Videos.GetAsync(videoId, cancellationToken);
    }
}