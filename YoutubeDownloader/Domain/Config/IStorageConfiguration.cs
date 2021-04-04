namespace YoutubeDownloader.Domain.Config
{
    public interface IStorageConfiguration
    {
          string DefaultPath { get; set; }
          bool SortFilesByPlaylist { get; set; } 
    }
}