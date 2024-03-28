using SkyVault.WebApi.Backend.Models;

namespace SkyVault.WebApi.Backend.Seeds
{
    public class ComMethodSeedData
    {
        public static readonly List<CommunicationMethod> comMethod = new List<CommunicationMethod>
        {
            new CommunicationMethod { Id = 1, CommTitle = "Non" },
            new CommunicationMethod { Id = 2, CommTitle = "Email" },
            new CommunicationMethod { Id = 3, CommTitle = "WhatsApp" }
        };
    }
}
