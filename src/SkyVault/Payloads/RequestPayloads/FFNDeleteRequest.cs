using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyVault.Payloads.RequestPayloads
{
    public class FFNDeleteRequest
    {
        public string FFNId { get; set; }
        public string SystemUserId { get; set; }
        public string CustomerProfileId { get; set; }
    }
}
