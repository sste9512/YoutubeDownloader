namespace YoutubeDownloader_WPFCore.Application.Configuration;

public interface IFileSystemConfiguration
{
       public Guid UserId { get; set; }
       public string DownloadsFolder { get; set; }
       
}