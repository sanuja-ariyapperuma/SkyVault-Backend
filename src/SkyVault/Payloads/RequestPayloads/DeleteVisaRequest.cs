using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyVault.Payloads.RequestPayloads
{
    public record DeleteVisaRequest(string SystemUserId, string CustomerProfileId);

}
