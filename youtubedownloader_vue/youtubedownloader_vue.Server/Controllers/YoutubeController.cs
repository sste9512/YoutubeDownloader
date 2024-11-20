using System.Runtime.CompilerServices;
using LiteDB;
using Microsoft.AspNetCore.Mvc;
using YoutubeExplode;
using YoutubeExplode.Channels;
using YoutubeExplode.Common;
using YoutubeExplode.Playlists;
using YoutubeExplode.Search;
using YoutubeExplode.Videos;

namespace youtubedownloader_vue.Server.Controllers;

//TODO : Add proper async enumerable method handling in the endpoints, for streaming

public sealed class YoutubeController(YoutubeClient youtubeClient) : ApiControllerBase
{
    private VideoClient Videos => youtubeClient.Videos;
    private PlaylistClient Playlists => youtubeClient.Playlists;
    private ChannelClient Channels => youtubeClient.Channels;
    private SearchClient Search => youtubeClient.Search;

    [HttpGet("[action]")]
    public async ValueTask<Video> GetByVideoIdAsync(VideoId videoId, CancellationToken cancellationToken)
    {
        return await Videos.GetAsync(videoId, cancellationToken);
    }

    [HttpGet("[action]")]
    public async ValueTask<Playlist> GetByPlayListIdAsync(PlaylistId playlistId, CancellationToken cancellationToken)
    {
        return await Playlists.GetAsync(playlistId, cancellationToken);
    }

    [HttpGet("[action]")]
    public IAsyncEnumerable<PlaylistVideo> GetVideosByPlayListIdAsync(PlaylistId playlistId,
        CancellationToken cancellationToken)
    {
        return Playlists.GetVideosAsync(playlistId, cancellationToken);
    }

    [HttpGet("[action]")]
    public IAsyncEnumerable<Batch<PlaylistVideo>> GetVideoBatchesAsync(PlaylistId playlistId,
        CancellationToken cancellationToken)
    {
        return Playlists.GetVideoBatchesAsync(playlistId, cancellationToken);
    }

    [HttpGet("[action]")]
    public async ValueTask<Channel> GetByChannelIdAsync(ChannelId channelId, CancellationToken cancellationToken)
    {
        return await Channels.GetAsync(channelId, cancellationToken);
    }

    [HttpGet("[action]")]
    public async ValueTask<Channel> GetByUserAsync(UserName userName, CancellationToken cancellationToken)
    {
        return await Channels.GetByUserAsync(userName, cancellationToken);
    }

    [HttpGet("[action]")]
    public async ValueTask<Channel> GetBySlugAsync(ChannelSlug channelSlug, CancellationToken cancellationToken)
    {
        return await Channels.GetBySlugAsync(channelSlug, cancellationToken);
    }

    [HttpGet("[action]")]
    public async ValueTask<Channel> GetByHandleAsync(ChannelHandle channelHandle, CancellationToken cancellationToken)
    {
        return await Channels.GetByHandleAsync(channelHandle, cancellationToken);
    }

    [HttpGet("[action]")]
    public IAsyncEnumerable<PlaylistVideo> GetUploadsAsync(ChannelId channelId, CancellationToken cancellationToken)
    {
        return Channels.GetUploadsAsync(channelId, cancellationToken);
    }

    [HttpGet("[action]")]
    public IAsyncEnumerable<ISearchResult> GetResultsAsync(string searchQuery, CancellationToken cancellationToken)
    {
        return Search.GetResultsAsync(searchQuery, cancellationToken);
    }

    [HttpGet("[action]")]
    public IAsyncEnumerable<VideoSearchResult> GetVideosAsync(string searchQuery, CancellationToken cancellationToken)
    {
        return Search.GetVideosAsync(searchQuery, cancellationToken);
    }

    [HttpGet("[action]")]
    public IAsyncEnumerable<PlaylistSearchResult> GetPlaylistsAsync(string searchQuery,
        CancellationToken cancellationToken)
    {
        return Search.GetPlaylistsAsync(searchQuery, cancellationToken);
    }


    [HttpGet("[action]")]
    public async IAsyncEnumerable<ChannelSearchResult> GetChannelsAsync(string searchQuery,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        var results = Search.GetChannelsAsync(searchQuery, cancellationToken);
        await foreach (var result in results.WithCancellation(cancellationToken))
        {
            // Save result to LiteDB
            using (var db = new LiteDatabase(@"Filename=MyData.db;Connection=shared"))
            {
                var collection = db.GetCollection<ChannelSearchResult>("channels");
                collection.Insert(result);
            }

            yield return result;
        }
    }

    [HttpGet("[action]")]
    public IAsyncEnumerable<Batch<ISearchResult>> GetResultBatchesAsync(string searchQuery, SearchFilter searchFilter,
        CancellationToken cancellationToken)
    {
        return Search.GetResultBatchesAsync(searchQuery, searchFilter, cancellationToken);
    }

    [HttpGet("[action]")]
    public IAsyncEnumerable<Batch<ISearchResult>> GetResultBatchesWithFilterAsync(string searchQuery,
        CancellationToken cancellationToken)
    {
        return Search.GetResultBatchesAsync(searchQuery, cancellationToken);
    }
}