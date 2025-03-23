using System.ComponentModel.DataAnnotations.Schema;

namespace SkyVault.WebApi.Backend.Models;

public partial class NotificationTemplate
{
    public int Id { get; set; }

    public string? Content { get; set; }

    public int NotificationType { get; set; }

    public string? File { get; set; }

    [ForeignKey(nameof(CreatedByUser))]
    public int CreatedBy { get; set; }

    [ForeignKey(nameof(UpdatedByUser))]
    public int? UpdatedBy { get; set; }

    public bool Active { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Job> Jobs { get; set; } = new List<Job>();

    public virtual NotificationType NotificationTypeNavigation { get; set; } = null!;

    public virtual SystemUser CreatedByUser { get; set; } = null!;

    public virtual SystemUser? UpdatedByUser { get; set; }
}
