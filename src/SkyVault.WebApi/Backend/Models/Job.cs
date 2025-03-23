namespace SkyVault.WebApi.Backend.Models;

public partial class Job
{
    public int Id { get; set; }

    public DateOnly DateTime { get; set; }

    public string Status { get; set; } = null!;

    public int CustomerProfileId { get; set; }

    public int TemplateId { get; set; }

    public string? Log { get; set; }

    public virtual CustomerProfile CustomerProfile { get; set; } = null!;

    public virtual NotificationTemplate Template { get; set; } = null!;
}
