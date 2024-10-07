using System;
using System.Collections.Generic;

namespace SkyVault.WebApi.Backend.Models;

public partial class CustomerProfile
{
    public int Id { get; set; }

    public int SalutationId { get; set; }

    public int PreferredCommId { get; set; }

    public int SystemUserId { get; set; }

    public int? ParentId { get; set; }

    public string WhatsAppNumber { get; set; } = String.Empty;

    public string EmailAddress { get; set; } = String.Empty;

    public virtual ICollection<FrequentFlyerNumber> FrequentFlyerNumbers { get; set; } = new List<FrequentFlyerNumber>();

    public virtual ICollection<CustomerProfile> InverseParent { get; set; } = new List<CustomerProfile>();

    public virtual ICollection<Job> Jobs { get; set; } = new List<Job>();

    public virtual CustomerProfile? Parent { get; set; } = null!;

    public virtual ICollection<Passport> Passports { get; set; } = new List<Passport>();

    public virtual CommunicationMethod PreferredComm { get; set; } = null!;

    public virtual Salutation Salutation { get; set; } = null!;

    public virtual SystemUser SystemUser { get; set; } = null!;
}
