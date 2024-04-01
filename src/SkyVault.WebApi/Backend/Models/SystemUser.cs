using System;
using System.Collections.Generic;

namespace SkyVault.WebApi.Backend.Models;

public partial class SystemUser
{
    public int Id { get; set; }

    public string? FirstName { get; set; } = null!;

    public string? LastName { get; set; } = null!;

    public string? UserRole { get; set; } = null!;

    public string? SamProfileId { get; set; } = null!;

    public string? ProfilePicture { get; set; }

    public string? Active { get; set; }

    public virtual ICollection<CustomerProfile> CustomerProfiles { get; set; } = new List<CustomerProfile>();
}
