using LiteDB;
using YoutubeDownloader_WPFCore.Application.Interfaces;
using YoutubeExplode.Playlists;
using YoutubeExplode.Videos;

namespace YoutubeDownloader_WPFCore.Infrastructure.Stores;

public sealed class DocumentStore(LiteDatabase liteDatabase) : IDocumentStore
{
    public ILiteCollection<Video> SavedVideos { get; } = liteDatabase.GetCollection<Video>();

    public ILiteCollection<PlaylistVideo> PlaylistVideos { get; } =  liteDatabase.GetCollection<PlaylistVideo>();
    
    
}