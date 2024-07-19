using LiteDB;
using YoutubeDownloader_WPFCore.Application.Configuration;
using YoutubeExplode.Common;
using YoutubeExplode.Playlists;
using YoutubeExplode.Videos;

namespace YoutubeDownloader_WPFCore.Application.Interfaces;

public interface IDocumentStore
{
    ILiteCollection<Video> SavedVideos { get; }
    ILiteCollection<PlaylistVideo> PlaylistVideos { get; }

    ILiteCollection<Author> FavoriteAuthors { get; }
}