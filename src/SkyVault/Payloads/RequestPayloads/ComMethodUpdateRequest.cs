using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyVault.Payloads.RequestPayloads;

public record ComMethodUpdateRequest(
    string? CustomerProfileId,
    string? EmailAddress,
    string? PrefCommId,
    string? WhatsAppNumber
    );
