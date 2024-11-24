using System.ComponentModel.DataAnnotations;

namespace CleanArchitecture.Domain.Entities;

public class UserPlaylist : BaseAuditableEntity
{
    public Guid UserId { get; set; }
    [MaxLength(50)] public string Name { get; set; } = string.Empty;
    [MaxLength(50)] public string Category { get; set; } = string.Empty;

    public string PlaylistDownloadPath { get; set; } = string.Empty;

    public string PlaylistThumbnailPath { get; set; } = string.Empty;

    public string PlaylistDescription { get; set; } = string.Empty;

    public string PreferredStorageType { get; set; } = string.Empty;
}