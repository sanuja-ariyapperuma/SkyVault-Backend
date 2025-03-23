using SkyVault.Payloads.CommonPayloads;

namespace SkyVault.Payloads.ResponsePayloads;

public record ProfileDefinitionResponse(
List<Salutation>? Salutations,
List<Nationality>? Nationalities,
List<Gender>? Genders,
List<Country>? Countries
);

