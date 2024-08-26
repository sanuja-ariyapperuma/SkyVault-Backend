using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyVault.Payloads.CommonPayloads
{
    public record AuthenticatedUser(
            string DisplayName,
            string UserRole,
            string AccessToken
        );
}
