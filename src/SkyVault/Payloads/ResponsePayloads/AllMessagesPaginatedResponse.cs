using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyVault.Payloads.ResponsePayloads
{
    public record AllMessagesPaginatedResponse(int CurrentPage, int TotalPages, int TotalMessages, List<MessageSummary> Messages);

    public record MessageSummary (int Id, int MessageTypeId, bool ActiveStatus, String Summary);
}
