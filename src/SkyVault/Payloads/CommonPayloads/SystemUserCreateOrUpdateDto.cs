
namespace SkyVault.Payloads.CommonPayloads
{
    public record SystemUserCreateOrUpdateDto(
            string Upn,
            string FirstName,
            string LastName,
            SystemUserRole UserRole
        );
}
