using System.ComponentModel.DataAnnotations;

namespace CleanArchitecture.Domain.Entities;

public class UserPlaylist : BaseAuditableEntity
{
    public Guid UserId { get; set; }
    [MaxLength(50)] public string Name { get; set; } = string.Empty;
    [MaxLength(50)] public string Category { get; set; } = string.Empty;
}