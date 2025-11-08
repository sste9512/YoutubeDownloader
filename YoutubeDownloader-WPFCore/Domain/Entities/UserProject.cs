namespace YoutubeDownloader_WPFCore.Domain.Entities;

public sealed class UserProject
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string CreatedAt { get; set; }
    public string UpdatedAt { get; set; }
    public string CreatedBy { get; set; }
    public string UpdatedBy { get; set; }
    public string Status { get; set; }
    public string Type { get; set; }
    public string Category { get; set; }
    public string SubCategory { get; set; }
    public string Tags { get; set; }
    public static readonly string TableName = "user_project";
    public required string FilePath { get; set; }
}