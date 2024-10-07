using SkyVault.Payloads.CommonPayloads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyVault.Payloads.ResponsePayloads
{
    public class ComMethod
    {
        public int CommunicationMethod { get; set; }
        public string WhatsAppNumber { get; set; }
        public string Email { get; set; }
    }
}
