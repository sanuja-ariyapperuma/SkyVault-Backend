using SkyVault.Payloads.RequestPayloads;
using SkyVault.Payloads.ResponsePayloads;

namespace SkyVault.WebApp.Proxies;

public sealed class AuthorityProxy(HttpClient httpClient)
{
    public async Task<WelcomeUserResponse?> GetUserInfo(ValidateUserRequest validateUserRequest)
    {
        //Get user information from API
        using var responseMessage = await httpClient.PostAsJsonAsync<ValidateUserRequest>("users/inituser", 
            validateUserRequest);

        //At all times a user will exist or be created if NOT!
        //Access is dictated through Azure AD App Registration IAM Access to this application
        responseMessage.EnsureSuccessStatusCode();

        return await responseMessage.Content.ReadFromJsonAsync<WelcomeUserResponse>();
    }
        
}