using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyVault.Payloads
{
    public sealed record ProfilePayload(
    string? Id,
    string? SalutationId,
    Passport[]? Passports,
    string[] FrequentFlyerNumbers,
    string? PreffdComMth,
    string? ParentId,
    string? SystemUserId
        );

    public sealed record Passport(
        string? Id,
        string? LastName,
        string? OtherNames,
        string? PassportNumber,
        string? Gender,
        string? DateOfBirth,
        string? PlaceOfBirth,
        string? ExpiryDate,
        string? NationalityId,
        string? CountryId,
        string? IsPrimary,
        Visa[]? Visa
        );

    public sealed record Visa(
    string? Id,
    string? VisaNumber,
    string? CountryId,
    string? IssuedPlace,
    string? IssuedDate,
    string? ExpireDate,
    string? AssignWithPrimary
        );
}
