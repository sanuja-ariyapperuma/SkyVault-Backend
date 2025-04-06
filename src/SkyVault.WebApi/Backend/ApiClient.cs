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

            var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/api/PromotionalHTTPFunction", content);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}
