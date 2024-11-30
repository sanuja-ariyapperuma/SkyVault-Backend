using System;
using System.Collections.Generic;

namespace SkyVault.WebApi.Backend.Models;

public partial class Nationality
{
    public int Id { get; set; }

    public string NationalityName { get; set; } = null!;

    public string NationalityCode { get; set; } = String.Empty;

    public virtual ICollection<Passport> Passports { get; set; } = new List<Passport>();
}
