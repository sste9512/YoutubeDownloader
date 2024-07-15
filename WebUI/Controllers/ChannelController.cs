using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;
using YoutubeExplode;
using YoutubeExplode.Channels;
using YoutubeExplode.Playlists;

namespace WebUI.Controllers;

public sealed class ChannelController(YoutubeClient youtubeClient) : ApiControllerBase
{
    [HttpGet("[action]")]
    public async ValueTask<Channel> GetAsync(ChannelId channelId, CancellationToken cancellationToken)
    {
        return await youtubeClient.Channels.GetAsync(channelId, cancellationToken);
    }

    [HttpGet("[action]")]
    public async ValueTask<Channel> GetByUserAsync(UserName userName, CancellationToken cancellationToken)
    {
        return await youtubeClient.Channels.GetByUserAsync(userName, cancellationToken);
    }

    [HttpGet("[action]")]
    public async ValueTask<Channel> GetBySlugAsync(ChannelSlug channelSlug, CancellationToken cancellationToken)
    {
        return await youtubeClient.Channels.GetBySlugAsync(channelSlug, cancellationToken);
    }

    [HttpGet("[action]")]
    public async ValueTask<Channel> GetByHandleAsync(ChannelHandle channelHandle, CancellationToken cancellationToken)
    {
        return await youtubeClient.Channels.GetByHandleAsync(channelHandle, cancellationToken);
    }

    [HttpGet("[action]")]
    public async IAsyncEnumerable<PlaylistVideo> GetUploadsAsync(ChannelId channelId,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        await foreach (var video in youtubeClient.Channels.GetUploadsAsync(channelId, cancellationToken))
        {
            yield return video;
        }
        //return youtubeClient.Channels.GetUploadsAsync(channelId, cancellationToken);
    }
}