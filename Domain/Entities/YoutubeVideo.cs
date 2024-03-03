namespace CleanArchitecture.Domain.Entities;

public sealed class YoutubeVideo : BaseAuditableEntity
{
    string VideoUrl { get; set; } 
    string VideoFormat { get; set; }
    byte[] Data { get; set; }
}