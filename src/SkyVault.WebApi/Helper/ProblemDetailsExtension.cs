using Microsoft.AspNetCore.Mvc;

namespace SkyVault.WebApi.Helper;

internal static class ProblemDetailsExtension
{
    public static ProblemDetails ToProblemDetails(this ProblemDetails problemDetails,
        string? detail, string? errorCode, string? correlationId)
    {
        problemDetails.Title = "An error occurred while processing the request.";
        problemDetails.Detail = detail;
        problemDetails.Status = StatusCodes.Status500InternalServerError;
        problemDetails.Instance = $"urn:skyvault:error:{correlationId}";
        problemDetails.Type = "https://httpstatuses.com/500";
        problemDetails.Extensions = new Dictionary<string, object>
        {
            { "correlationId", correlationId ??= "Not specified" },
            { "errorCode", errorCode ??= "Not specified" }
        }!;

        return problemDetails;
    }
}