using SkyVault.Payloads.ResponsePayloads;

namespace SkyVault.WebApi.Backend
{
    public interface IApiClient
    {
        Task<string> PostBroardcastMessageAsync(int templateId, string promotionalType);
        Task<EmailAccountInfo> GetEmailAccountInformation();
    }
}
