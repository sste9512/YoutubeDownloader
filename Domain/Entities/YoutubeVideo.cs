using System.ComponentModel.DataAnnotations;

namespace CleanArchitecture.Domain.Entities;

public sealed class YoutubeVideo : BaseAuditableEntity
{
    [MaxLength(50)] public string? VideoUrl { get; set; } 
    [MaxLength(50)] public string? VideoFormat { get; set; }
    public byte[]? Data { get; set; }
}