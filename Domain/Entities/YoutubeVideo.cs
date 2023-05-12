namespace CleanArchitecture.Domain.Entities;

public class YoutubeVideo : BaseAuditableEntity
{
    string VideoUrl { get; set; }
    byte[] Data { get; set; }
}