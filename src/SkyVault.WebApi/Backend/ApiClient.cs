using Azure.Core;
using Azure.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using SkyVault.Payloads.ResponsePayloads;
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

        public async Task<EmailAccountInfo> GetEmailAccountInformation()
        {

            var functionKey = Environment.GetEnvironmentVariable("AzureFunctionKey");
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            if (string.Equals(environment, "Production", StringComparison.OrdinalIgnoreCase) &&
                    string.IsNullOrEmpty(functionKey))
            {
                throw new InvalidOperationException("AzureFunctionKey not found");
            }

            _httpClient.DefaultRequestHeaders.Add("x-functions-key", functionKey);
            var response = await _httpClient.GetAsync("/api/GetAccountInformtaionFunction");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            using var doc = JsonDocument.Parse(json);
            var dataElement = doc.RootElement.GetProperty("data");

            var emailAccountInfo = JsonSerializer.Deserialize<EmailAccountInfo>(
                dataElement.GetRawText(),
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );

            return emailAccountInfo!;

        }

        public async Task<string> PostBroardcastMessageAsync(int templateId, string promotionalType)
        {
            var requestBody = new
            {
                TemplateId = templateId,
                PromotionType = promotionalType
            };


            var functionKey = Environment.GetEnvironmentVariable("AzureFunctionKey");
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            if (string.Equals(environment, "Production", StringComparison.OrdinalIgnoreCase) &&
                    string.IsNullOrEmpty(functionKey))
            {
                throw new InvalidOperationException("AzureFunctionKey not found");
            }

            _httpClient.DefaultRequestHeaders.Add("x-functions-key", functionKey);

            var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/api/PromotionalHTTPFunction", content);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }



        //private async Task<string> GetCredentialsAsync()
        //{
            
        //        var tenantId = _configuration["AzureAD:TenantId"];
        //        var clientId = _configuration["AzureAD:ClientId"];
        //        var clientSecret = _configuration["AzureAD:ClientSecret"];  // This is getting expired in 5/18/2027 (remember to add the value of the client secret not the secret id)

        //    var clientSecretCredential = new ClientSecretCredential(tenantId, clientId, clientSecret);

        //        var scope = $"api://{clientId}/.default";

        //        var token = await clientSecretCredential.GetTokenAsync(new TokenRequestContext(new[] { scope }));

        //        return token.Token;
            
        //}
    }
}
