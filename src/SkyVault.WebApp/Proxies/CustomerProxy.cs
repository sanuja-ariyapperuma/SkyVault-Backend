using System.Net;
using Microsoft.AspNetCore.Mvc;
using SkyVault.Payloads.CommonPayloads;
using SkyVault.Payloads.RequestPayloads;
using SkyVault.Payloads.ResponsePayloads;

namespace SkyVault.WebApp.Proxies;

public sealed class CustomerProxy(HttpClient httpClient)
{
    public SkyResult<SearchProfileResponse>? SearchProfile(SearchProfileRequest searchProfileRequest)
    {
        var postResponse = httpClient.PostAsJsonAsync("/searchprofile", searchProfileRequest).Result;
        
        switch (postResponse.StatusCode)
        {
            case HttpStatusCode.BadRequest:
            {
                var failedPayload = postResponse.Content.ReadFromJsonAsync<ValidationProblemDetails>().Result;
            
                return new SkyResult<SearchProfileResponse>()
                    .Fail(message: failedPayload?.Detail,
                        errorCode: failedPayload?.Extensions?["errorCode"]?.ToString(),
                        correlationId: failedPayload?.Extensions?["correlationId"]?.ToString());
            }
            case HttpStatusCode.InternalServerError:
            {
                var failedPayload = postResponse.Content.ReadFromJsonAsync<ProblemDetails>().Result;
            
                return new SkyResult<SearchProfileResponse>()
                    .Fail(message: failedPayload?.Detail,
                        errorCode: failedPayload?.Extensions?["errorCode"]?.ToString(),
                        correlationId: failedPayload?.Extensions?["correlationId"]?.ToString());
            }
        }
        
        postResponse.EnsureSuccessStatusCode();
        
        var payload = postResponse.Content.ReadFromJsonAsync<SearchProfileResponse>().Result;
        
        return new SkyResult<SearchProfileResponse>().SucceededWithValue(payload!);
    }

    public SkyResult<ProfilePayload>? GetCustomerProfile(GetProfileRequest getProfileRequest)
    {
        var postResponse = httpClient.PostAsJsonAsync("/getprofile", getProfileRequest).Result;    
        
        switch (postResponse.StatusCode)
        {
            case HttpStatusCode.BadRequest:
            {
                var failedPayload = postResponse.Content.ReadFromJsonAsync<ValidationProblemDetails>().Result;
            
                return new SkyResult<ProfilePayload>()
                    .Fail(message: failedPayload?.Detail,
                        errorCode: failedPayload?.Extensions?["errorCode"]?.ToString(),
                        correlationId: failedPayload?.Extensions?["correlationId"]?.ToString());
            }
            case HttpStatusCode.InternalServerError:
            {
                var failedPayload = postResponse.Content.ReadFromJsonAsync<ProblemDetails>().Result;
            
                return new SkyResult<ProfilePayload>()
                    .Fail(message: failedPayload?.Detail,
                        errorCode: failedPayload?.Extensions?["errorCode"]?.ToString(),
                        correlationId: failedPayload?.Extensions?["correlationId"]?.ToString());
            }
        }
        
        postResponse.EnsureSuccessStatusCode();
        
        var payload = postResponse.Content.ReadFromJsonAsync<ProfilePayload>().Result;
        
        return new SkyResult<ProfilePayload>().SucceededWithValue(payload!);
    }
}
