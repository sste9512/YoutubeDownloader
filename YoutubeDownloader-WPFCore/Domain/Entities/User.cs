namespace YoutubeDownloader_WPFCore.Domain.Entities;

public sealed class User
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string CreatedAt { get; set; }
    public string UpdatedAt { get; set; }
    public string CreatedBy { get; set; }
    public string UpdatedBy { get; set; }
    public string Status { get; set; }
    public string Type { get; set; }
    public string Category { get; set; }

    public static readonly string TableName = "user";
}