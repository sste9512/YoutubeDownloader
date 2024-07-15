using LiteDB;
using YoutubeExplode.Videos;

namespace YoutubeDownloader_WPFCore.Application.Interfaces;

public interface IDocumentStore 
{ 
     ILiteCollection<Video>  SavedVideos { get; }
   
}