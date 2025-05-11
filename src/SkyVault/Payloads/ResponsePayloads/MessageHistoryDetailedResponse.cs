using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyVault.Payloads.ResponsePayloads
{
    public record MessageHistoryDetailedResponse(
        int MessageId, 
        string MessageSubject, 
        string MessageBody, 
        string MessageType, 
        string? Attachment, 
        bool ActiveStatus,
        string CreatedAt, 
        string CreatedBy,
        string? UpdatedAt, 
        string UpdatedBy, 
        string CommiunicationMethod);
}
