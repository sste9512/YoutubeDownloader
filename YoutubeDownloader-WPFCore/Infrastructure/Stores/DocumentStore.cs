using LiteDB;
using YoutubeDownloader_WPFCore.Application.Interfaces;
using YoutubeExplode.Videos;

namespace YoutubeDownloader_WPFCore.Infrastructure.Stores;

public sealed class DocumentStore(LiteDatabase liteDatabase) : IDocumentStore
{
    public ILiteCollection<Video> SavedVideos { get; } = liteDatabase.GetCollection<Video>();
    
    
}