using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyVault.Payloads.RequestPayloads;
    public record PassportRequest(
            string? Id,
            string? CustomerProfileId,
            string? SystemUserId,
            string? ParentId,
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
            string? SalutationId
        );
