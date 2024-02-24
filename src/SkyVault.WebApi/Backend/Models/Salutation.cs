using System;
using System.Collections.Generic;

namespace SkyVault.WebApi.Backend.Models;

public partial class Salutation
{
    public int Id { get; set; }

    public string SalutationName { get; set; } = null!;

    public virtual ICollection<CustomerProfile> CustomerProfiles { get; set; } = new List<CustomerProfile>();
}
