namespace SkyVault.Payloads.ResponsePayloads
{
    public record FamilyMembersResponse(int CustomerId, string LastName, string OtherNames, string PassportNumber, bool IsParent);
}
