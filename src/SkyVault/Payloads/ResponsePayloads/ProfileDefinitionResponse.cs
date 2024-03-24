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

    //public record BasicInfo(string Id, string Name);

    //public record Salutation: BasicInfo 
    //{
    //    public Salutation(string id, string name) : base(id, name) { }
    //}
    //public record Nationality : BasicInfo { 
    //    public Nationality(string id, string name) : base(id, name) { }
    //}
    //public record Gender : BasicInfo
    //{
    //    public Gender(string id, string name) : base(id, name) { }
    //}
    //public record Country : BasicInfo
    //{
    //    public Country(string id, string name) : base(id, name) { }
    //}

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
