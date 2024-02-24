using System;
using System.Collections.Generic;

namespace SkyVault.WebApi.Backend.Models;

public partial class Country
{
    public int Id { get; set; }

    public string? CountryCode { get; set; }

    public string CountryName { get; set; } = null!;

    public virtual ICollection<Visa> Visas { get; set; } = new List<Visa>();
}
