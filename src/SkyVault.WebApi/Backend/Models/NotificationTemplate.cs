using System;
using System.Collections.Generic;

namespace SkyVault.WebApi.Backend.Models;

public partial class NotificationTemplate
{
    public int Id { get; set; }

    public string? Content { get; set; }

    public int NotificationType { get; set; }

    public string? File { get; set; }

    public virtual ICollection<Job> Jobs { get; set; } = new List<Job>();

    public virtual NotificationType NotificationTypeNavigation { get; set; } = null!;
}
