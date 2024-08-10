using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyVault.Payloads.RequestPayloads;
    public record VisaReqeust(
        string? Id,
        string? CustomerProfileId,
        string? SystemUserId,
        string? PassportId,
        string? VisaNumber,
        string? IssuedPlace,
        string? IssuedDate,
        string? ExpiryDate,
        string? CountryId
    );

