using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyVault.Payloads.RequestPayloads;
    public sealed record SearchProfileRequest(
        string? SysUserId,
        string? SearchQuery
        );

