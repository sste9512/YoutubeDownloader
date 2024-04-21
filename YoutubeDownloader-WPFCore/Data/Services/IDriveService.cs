using Google.Apis.Drive.v3.Data;

namespace YoutubeDownloader_WPFCore.Data.Services;

public interface IDriveService
{
    Task<Drive> CreateFolder();
    Task<bool> UploadFile(string filePath, string folderId);
}