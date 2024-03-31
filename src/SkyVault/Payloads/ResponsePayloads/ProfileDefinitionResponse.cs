using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyVault.Payloads.ResponsePayloads
{
    public record ProfileDefinitionResponse(
    List<Salutation>? Salutations,
    List<Nationality>? Nationalities,
    List<Gender>? Genders,
    List<Country>? Countries
    );

    public class Salutation 
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
    public class Nationality
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
    public class Country
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
    public class Gender
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
