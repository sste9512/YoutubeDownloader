using CleanArchitecture.Application.Common.Interfaces;
using LiteDB;
using YoutubeExplode.Common;
using YoutubeExplode.Playlists;
using YoutubeExplode.Videos;

namespace CleanArchitecture.Infrastructure.Persistence.Stores;

public sealed class DocumentStore(LiteDatabase liteDatabase) : IDocumentStore
{
    public ILiteCollection<Video> SavedVideos { get; } = liteDatabase.GetCollection<Video>();
    public ILiteCollection<PlaylistVideo> PlaylistVideos { get; } = liteDatabase.GetCollection<PlaylistVideo>();
    public ILiteCollection<Author> FavoriteAuthors { get; } = liteDatabase.GetCollection<Author>();
}