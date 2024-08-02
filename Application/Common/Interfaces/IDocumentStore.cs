using LiteDB;
using YoutubeExplode.Common;
using YoutubeExplode.Playlists;
using YoutubeExplode.Videos;

namespace CleanArchitecture.Application.Common.Interfaces;

public interface IDocumentStore
{
    ILiteCollection<Video> SavedVideos { get; }
    ILiteCollection<PlaylistVideo> PlaylistVideos { get; }
    ILiteCollection<Author> FavoriteAuthors { get; }
}