using System;
using System.Collections.Generic;

namespace SkyVault.WebApi.Backend.Models;

public partial class Passport
{
    public int Id { get; set; }

    public int CustomerProfileId { get; set; }

    public string LastName { get; set; } = null!;

    public string? OtherNames { get; set; }

    public string PassportNumber { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public DateOnly DateOfBirth { get; set; }

    public DateOnly? ExpiryDate { get; set; }

    public string? PlaceOfBirth { get; set; }

    public int NationalityId { get; set; }

    public string IsPrimary { get; set; } = null!;

    public virtual CustomerProfile CustomerProfile { get; set; } = null!;

    public virtual Nationality Nationality { get; set; } = null!;

    public virtual ICollection<Visa> Visas { get; set; } = new List<Visa>();
}
