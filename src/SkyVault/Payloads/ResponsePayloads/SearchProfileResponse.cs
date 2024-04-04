using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
