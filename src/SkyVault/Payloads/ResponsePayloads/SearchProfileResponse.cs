namespace SkyVault.Payloads.ResponsePayloads
{
    public record SearchProfileResponse(
    int ProfileId,
    string LastName,
    string OtherNames,
    string PassportNumber,
    string Salutation
    );
}
