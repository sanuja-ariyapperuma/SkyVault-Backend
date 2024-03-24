using SkyVault.WebApi.Backend.Models;

namespace SkyVault.WebApi.Backend.Seeds
{
    public class SalutationSeedData
    {
        private static int idCounter = 1;

        public static readonly List<Salutation> salutations = new List<Salutation>
        {
            new Salutation { Id=idCounter++, SalutationName = "Mr" },
            new Salutation { Id=idCounter++, SalutationName = "Mrs" },
            new Salutation { Id=idCounter++, SalutationName = "Miss" },
            new Salutation { Id=idCounter++, SalutationName = "Dr" },
            new Salutation { Id=idCounter++, SalutationName = "Prof" },
            new Salutation { Id=idCounter++, SalutationName = "Rev" },
            new Salutation { Id=idCounter++, SalutationName = "Hon" }
        };  
    }
}
