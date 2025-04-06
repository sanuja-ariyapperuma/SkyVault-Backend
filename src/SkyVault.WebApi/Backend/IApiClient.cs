namespace SkyVault.WebApi.Backend
{
    public interface IApiClient
    {
        Task<string> PostBroardcastMessageAsync(int templateId, string promotionalType);
    }
}
