using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyVault.Payloads.RequestPayloads
{
    public record BroadcastMessageRequest(
        string Subject, 
        string Content, 
        string BroadcastType, 
        string? FileName, 
        bool IsEmergency = false
    );
}
