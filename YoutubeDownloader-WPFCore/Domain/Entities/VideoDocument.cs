namespace YoutubeDownloader_WPFCore.Domain.Entities;

[Serializable]
public record VideoDocument(
    string Id,
    string Title,
    string Author,
    TimeSpan Duration,
    string Description,
    string ThumbnailUrl,
    long ViewCount,
    long LikeCount,
    DateTimeOffset UploadDate
)
{
    public static readonly string TableName = "video";
}