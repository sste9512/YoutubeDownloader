using LiteDB;
using YoutubeExplode.Playlists;
using YoutubeExplode.Videos;

namespace YoutubeDownloader_WPFCore.Application.Interfaces;

public interface IDocumentStore 
{ 
     ILiteCollection<Video>  SavedVideos { get; }
     ILiteCollection<PlaylistVideo> PlaylistVideos { get; }
   
}