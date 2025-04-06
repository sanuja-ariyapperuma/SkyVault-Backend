using Azure.Core;
using Azure.Identity;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace SkyVault.WebApi.Backend
{
    public class ApiClient : IApiClient
    {
        private readonly HttpClient _httpClient;

        public ApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> PostBroardcastMessageAsync(int templateId, string promotionalType)
        {
            var requestBody = new
            {
                TemplateId = templateId,
                PromotionType = promotionalType
            };

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await GetCredentialsAsync());

            var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/api/PromotionalHTTPFunction", content);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        private async Task<string> GetCredentialsAsync() 
        {
            var scope = "api://371a1a76-a0cc-46f3-8318-8f91058f3cef/.default";
            var credential = new DefaultAzureCredential();
            var token = await credential.GetTokenAsync(new TokenRequestContext(new[] { scope }));

            return token.Token;
        }
    }
}
