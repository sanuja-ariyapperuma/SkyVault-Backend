using Azure.Core;
using Azure.Identity;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace SkyVault.WebApi.Backend
{
    public class ApiClient : IApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public ApiClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<string> PostBroardcastMessageAsync(int templateId, string promotionalType)
        {
            var requestBody = new
            {
                TemplateId = templateId,
                PromotionType = promotionalType
            };

            var credentials = await GetCredentialsAsync();

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", credentials);

            var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/api/PromotionalHTTPFunction", content);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        private async Task<string> GetCredentialsAsync() 
        {
            var tenantId = _configuration["AzureAD:TenantId"];
            var clientId = _configuration["AzureAD:ClientId"];
            var clientSecret = _configuration["AzureAD:ClientSecret"];

            var clientSecretCredential = new ClientSecretCredential(tenantId, clientId, clientSecret);

            var scope = "api://371a1a76-a0cc-46f3-8318-8f91058f3cef/.default";

            var token = await clientSecretCredential.GetTokenAsync(new TokenRequestContext(new[] { scope }));

            return token.Token;
        }
    }
}
