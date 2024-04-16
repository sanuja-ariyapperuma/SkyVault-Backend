namespace SkyVault.Payloads.ResponsePayloads
{
    public record SearchProfileResponse(
    string Query,
    IEnumerable<SearchProfileItem> Profiles
    );
}
