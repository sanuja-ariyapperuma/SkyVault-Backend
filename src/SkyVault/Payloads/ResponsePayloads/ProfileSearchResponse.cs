using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyVault.Payloads.ResponsePayloads
{
    public record ProfileSearchResponse(
        string? ProfileId,
        string? FullName,
        string? PassportNumber
    );
        
}
