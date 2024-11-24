using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;
using YoutubeExplode;
using YoutubeExplode.Common;
using YoutubeExplode.Playlists;

namespace youtubedownloader_vue.Server.Controllers;

public sealed class PlaylistController(YoutubeClient youtubeClient) : ApiControllerBase
{
    [HttpGet("[action]")]
    public async ValueTask<Playlist> GetAsync(PlaylistId playlistId, CancellationToken cancellationToken)
    {
        return await youtubeClient.Playlists.GetAsync(playlistId, cancellationToken);
    }

    [HttpGet("[action]")]
    public IAsyncEnumerable<PlaylistVideo> GetVideosAsync(PlaylistId playlistId, CancellationToken cancellationToken)
    {
        return youtubeClient.Playlists.GetVideosAsync(playlistId, cancellationToken);
    }

    [HttpGet("[action]")]
    public async IAsyncEnumerable<Batch<PlaylistVideo>> GetVideoBatchesAsync(PlaylistId playlistId,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        await foreach (var batch in youtubeClient.Playlists.GetVideoBatchesAsync(playlistId, cancellationToken))
        {
            yield return batch;
        }
        //return youtubeClient.Playlists.GetVideoBatchesAsync(playlistId, cancellationToken);
    }
}