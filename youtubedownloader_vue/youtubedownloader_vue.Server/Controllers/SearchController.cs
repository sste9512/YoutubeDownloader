using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;
using YoutubeExplode;
using YoutubeExplode.Common;
using YoutubeExplode.Search;

namespace youtubedownloader_vue.Server.Controllers;

public sealed class SearchController(YoutubeClient youtubeClient) : ApiControllerBase
{
    [HttpGet("[action]")]
    public IAsyncEnumerable<ISearchResult> GetResultsAsync(string searchQuery, CancellationToken cancellationToken)
    {
        return youtubeClient.Search.GetResultsAsync(searchQuery, cancellationToken);
    }

    [HttpGet("[action]")]
    public IAsyncEnumerable<VideoSearchResult> GetVideosAsync(string searchQuery, CancellationToken cancellationToken)
    {
        return youtubeClient.Search.GetVideosAsync(searchQuery, cancellationToken);
    }

    [HttpGet("[action]")]
    public IAsyncEnumerable<PlaylistSearchResult> GetPlaylistsAsync(string searchQuery,
        CancellationToken cancellationToken)
    {
        return youtubeClient.Search.GetPlaylistsAsync(searchQuery, cancellationToken);
    }

    [HttpGet("[action]")]
    public IAsyncEnumerable<ChannelSearchResult> GetChannelsAsync(string searchQuery,
        CancellationToken cancellationToken)
    {
        return youtubeClient.Search.GetChannelsAsync(searchQuery, cancellationToken);
    }

    [HttpGet("[action]")]
    public async IAsyncEnumerable<Batch<ISearchResult>> GetResultBatchesAsync(string searchQuery, SearchFilter searchFilter,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        await foreach (var batch in youtubeClient.Search.GetResultBatchesAsync(searchQuery, searchFilter, cancellationToken))
        {
            yield return batch;
        }
        //return youtubeClient.Search.GetResultBatchesAsync(searchQuery, searchFilter, cancellationToken);
    }
}