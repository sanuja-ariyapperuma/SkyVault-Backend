using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyVault.Payloads.ResponsePayloads
{
    public record EmailAccountInfo(string CompanyName, string CompanyEmailAddress, Plan[] Plans);
    public record Plan(string Type, string CreditsType, int Credits);
}
