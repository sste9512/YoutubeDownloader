using System.IO;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using File = Google.Apis.Drive.v3.Data.File;

namespace YoutubeDownloader_WPFCore.Infrastructure.Data.Services;

public sealed class GoogleDriveService(DriveService driveService) : IDriveService
{
    private readonly DriveService _driveService = driveService;
    private DrivesResource Drives => driveService.Drives;
    private FilesResource Files => driveService.Files;

    public async Task<Drive> CreateFolder()
    {
        var request = Drives.Create(new Drive
        {
            BackgroundImageFile = null,
            BackgroundImageLink = null,
            Capabilities = null,
            ColorRgb = null,
            CreatedTimeRaw = null,
            CreatedTimeDateTimeOffset = null,
            Hidden = null,
            Id = null,
            Kind = null,
            Name = null,
            OrgUnitId = null,
            Restrictions = null,
            ThemeId = null,
            ETag = null
        }, " ");

        var stream = await request.ExecuteAsync();

        return stream;
    }

    public async Task<bool> UploadFile(string filePath, string folderId)
    {
        var fileMetadata = new File
        {
            Name = Path.GetFileName(filePath),
            Parents = new List<string> { folderId }
        };

        FilesResource.CreateMediaUpload request;

        await using (var stream = new FileStream(filePath, FileMode.Open))
        {
            request = Files.Create(fileMetadata, stream, "application/octet-stream");
            request.Fields = "id";
            await request.UploadAsync();
        }

        return request.ResponseBody != null;
    }
}