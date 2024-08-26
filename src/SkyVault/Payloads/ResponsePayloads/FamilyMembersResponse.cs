using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyVault.Payloads.ResponsePayloads
{
    public record FamilyMembersResponse(int CustomerId, string LastName, string OtherNames, string PassportNumber, bool IsParent);
}
