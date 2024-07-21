using Google.Apis.Drive.v3.Data;

namespace CleanArchitecture.Infrastructure.Persistence.Data.Services;

public interface IDriveService
{
    Task<Drive> CreateFolder();
    Task<bool> UploadFile(string filePath, string folderId);
}