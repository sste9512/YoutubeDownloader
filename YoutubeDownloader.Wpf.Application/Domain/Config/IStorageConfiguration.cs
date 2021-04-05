namespace YoutubeDownloader.Wpf.Application.Domain.Config
{
    public interface IStorageConfiguration
    {
          string DefaultPath { get; set; }
          bool SortFilesByPlaylist { get; set; } 
    }
}