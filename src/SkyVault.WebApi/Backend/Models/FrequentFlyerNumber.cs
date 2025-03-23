namespace SkyVault.WebApi.Backend.Models;

public partial class FrequentFlyerNumber
{
    public int Id { get; set; }

    public string FlyerNumber { get; set; } = null!;

    public int CustomerProfileId { get; set; }

    public virtual CustomerProfile CustomerProfile { get; set; } = null!;
}
