using System;
using System.Collections.Generic;

namespace SkyVault.WebApi.Backend.Models;

public partial class CommunicationMethod
{
    public int Id { get; set; }

    public string CommTitle { get; set; } = null!;

    public virtual ICollection<CustomerProfile> CustomerProfiles { get; set; } = new List<CustomerProfile>();
}
