using System;
using System.Collections.Generic;

namespace SkyVault.WebApi.Backend.Models;

public partial class NotificationType
{
    public int Id { get; set; }

    public string TypeName { get; set; } = null!;

    public virtual ICollection<NotificationTemplate> NotificationTemplates { get; set; } = new List<NotificationTemplate>();
}
