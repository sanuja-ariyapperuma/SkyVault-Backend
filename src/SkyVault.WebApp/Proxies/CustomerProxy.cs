using System.Net;
using Microsoft.AspNetCore.Mvc;
using SkyVault.Payloads.CommonPayloads;
using SkyVault.Payloads.RequestPayloads;
using SkyVault.Payloads.ResponsePayloads;

namespace SkyVault.WebApp.Proxies;

public sealed class CustomerProxy(HttpClient httpClient)
{
    private SkyResult<T> HandleResponse<T>(HttpResponseMessage response)
    {
        switch (response.StatusCode)
        {
            case HttpStatusCode.BadRequest:
            case HttpStatusCode.InternalServerError:
            {
                var failedPayload = response.Content.ReadFromJsonAsync<ProblemDetails>().Result;

                return new SkyResult<T>()
                    .Fail(message: failedPayload?.Detail,
                        errorCode: failedPayload?.Extensions?["errorCode"]?.ToString(),
                        correlationId: failedPayload?.Extensions?["correlationId"]?.ToString());
            }
            default:
                response.EnsureSuccessStatusCode();
                var payload = response.Content.ReadFromJsonAsync<T>().Result;
                return new SkyResult<T>().SucceededWithValue(payload!);
        }
    }

    public SkyResult<Passport> GetPassport(GetPassportRequest passportRequest)
    {
        var postResponse = httpClient.PostAsJsonAsync("/getpassport", passportRequest).Result;
        return HandleResponse<Passport>(postResponse);
    }
    
    public SkyResult<Visa> GetVisa(GetVisaRequest visaRequest)
    {
        var postResponse = httpClient.PostAsJsonAsync("/getvisa", visaRequest).Result;
        return HandleResponse<Visa>(postResponse);
    }
    
    public SkyResult<SaveUpdateCustomerProfileResponse> SavePassport(PassportRequest passportRequest)
    {
        var postResponse = httpClient.PostAsJsonAsync("/addpassport", passportRequest).Result;
        return HandleResponse<SaveUpdateCustomerProfileResponse>(postResponse);
    }

    public SkyResult<SearchProfileResponse>? SearchProfile(SearchProfileRequest searchProfileRequest)
    {
        var postResponse = httpClient.PostAsJsonAsync("/searchprofile", searchProfileRequest).Result;
        return HandleResponse<SearchProfileResponse>(postResponse);
    }

    public SkyResult<ProfilePayload>? GetCustomerProfile(GetProfileRequest getProfileRequest)
    {
        var postResponse = httpClient.PostAsJsonAsync("/getprofile", getProfileRequest).Result;
        return HandleResponse<ProfilePayload>(postResponse);
    }

    public SkyResult<ProfileDefinitionResponse>? GetProfileDefinitionData()
    {
        var getResponse = httpClient.GetAsync("/profilepage-commondata").Result;
        return HandleResponse<ProfileDefinitionResponse>(getResponse);
    }
}