using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyVault.Payloads.RequestPayloads
{
    public class GetComMethodRequest
    {
        public string CustomerProfileId { get; set; }
        public string SystemUserId { get; set; }
    }
}
