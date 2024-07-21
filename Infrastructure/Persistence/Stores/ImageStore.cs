using LiteDB;
using OneOf;
using YoutubeDownloader_WPFCore.Application.Configuration;
using YoutubeExplode.Playlists;
using YoutubeExplode.Videos;

namespace CleanArchitecture.Infrastructure.Persistence.Stores;

public class ImageCollectionPathBuilder
{
    public string FileCollectionName { get; set; } = string.Empty;
    public string ChunkName { get; set; } = string.Empty;

    public string ImageId { get; set; } = string.Empty;

    public string ImageName { get; set; } = string.Empty;
}

public class ImageCollectionPath
{
    public string FileCollectionName { get; set; }
    public string ChunkName { get; set; }
    public string ImageId { get; set; }
    public string ImageName { get; set; }
}

public class ImageStore
{
    private readonly LiteDatabase _liteDatabase;
    private readonly IFileSystemConfiguration _fileSystemConfiguration;

    private ImageCollectionPath _collectionPath;


    public ImageStore(LiteDatabase liteDatabase, IFileSystemConfiguration fileSystemConfiguration)
    {
        _liteDatabase = liteDatabase;
        _fileSystemConfiguration = fileSystemConfiguration;
    }

    public ImageStore To(ImageCollectionPath imageCollectionPath)
    {
        _collectionPath = imageCollectionPath;
        return this;
    }


    /// <summary>
    /// Saves images to particular paths via LiteDb
    /// </summary>
    /// <param name="fileFormat"></param>
    /// <returns></returns>
    public LiteFileInfo<string> SaveImageFromFileOrStream(OneOf<string, Stream> fileFormat)
    {
        // Partitions here for easier navigation
        var liteStorage = _liteDatabase.GetStorage<string>("YoutubeDownloader-" + _collectionPath.FileCollectionName , _collectionPath.ChunkName);

        if (fileFormat.IsT0)
        {
            // Upload a file from file system
            return liteStorage.Upload($"/{_fileSystemConfiguration.DownloadsFolder}/thumbnails/{_collectionPath.ImageId}",
                @"C:\Temp\picture-01.jpg");
        }

        // Upload a file from a Stream
        return liteStorage.Upload("$/{_fileSystemConfiguration.DownloadsFolder}/thumbnails/{_collectionPath.ImageId}", _collectionPath.ImageName, fileFormat.AsT1);
    }

    public void QueryAsStream(string fieldId, Stream inputStream)
    {
        var liteStorage = _liteDatabase.GetStorage<string>("YoutubeDownloader-" + _collectionPath.FileCollectionName, _collectionPath.ChunkName);

        // Find file reference only - returns null if not found
        var file = liteStorage.FindById(fieldId);

        // Or get binary data as Stream and copy to another Stream
        file.CopyTo(inputStream);
    }

    /*public void QueryImage(string fileId, )
        {

            var liteStorage = _liteDatabase.GetStorage<string>("myFiles", "myChunks");

            // Find file reference only - returns null if not found
            var file = liteStorage.FindById("$/photos/2014/picture-01.jpg");

            // Now, load binary data and save to file system
             file.SaveAs(@"C:\Temp\new-picture.jpg");

            // Or get binary data as Stream and copy to another Stream
            file.CopyTo(Response.OutputStream);

            // Find all files references in a "directory"
            var files = liteStorage.Find("$/photos/2014/");
        }*/
}

public static class ImagePathExtensions
{
    public static ImageCollectionPath FromPlaylistVideo(this PlaylistVideo playlistVideo)
    {
        return new ImageCollectionPath
        {
            FileCollectionName = playlistVideo.Title,
            ChunkName = "Videos",
            ImageId = playlistVideo.Id,
            ImageName = playlistVideo.Title + ".bmp"
        };
    }

    public static ImageCollectionPath FromVideo(this Video video)
    {
        return new ImageCollectionPath
        {
            FileCollectionName = video.Title,
            ChunkName = "Videos",
            ImageId = video.Id,
            ImageName = video.Title + ".bmp"
        };
    }
}