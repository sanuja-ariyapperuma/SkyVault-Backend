using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyVault.Payloads.RequestPayloads
{
    public class FFNRequest
    {
        public string? CustomerProfileId { get; set; }
        public string? FFN { get; set; } = String.Empty;
        public string? SystemUser { get; set; } = String.Empty;
    }
}
