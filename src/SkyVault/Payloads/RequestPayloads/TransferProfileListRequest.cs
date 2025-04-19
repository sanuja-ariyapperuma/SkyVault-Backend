using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyVault.Payloads.RequestPayloads
{
    public record TransferProfileRequest(int TransferTo, TransferProfile[] Customers);
    public record TransferProfile(int CustomerId, string CustomerName);
}
