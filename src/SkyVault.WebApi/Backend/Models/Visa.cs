using System;
using System.Collections.Generic;

namespace SkyVault.WebApi.Backend.Models;

public partial class Visa
{
    public int Id { get; set; }

    public string VisaNumber { get; set; } = null!;

    public string IssuedPlace { get; set; } = null!;

    public DateOnly IssuedDate { get; set; }

    public DateOnly ExpireDate { get; set; }

    public int CountryId { get; set; }

    public int PassportId { get; set; }
    public string BirthPlace { get; set; } = null!;

    public virtual Country Country { get; set; } = null!;

    public virtual Passport Passport { get; set; } = null!;
}
