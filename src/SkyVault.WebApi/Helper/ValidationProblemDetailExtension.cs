using Microsoft.AspNetCore.Mvc;

namespace SkyVault.WebApi.Helper;

internal static class ValidationProblemDetailExtension
{
    private static void SetCommonDetails(ValidationProblemDetails validationProblemDetails, string? detail,
        string? errorCode, string? correlationId)
    {
        validationProblemDetails.Title = "One or more validation errors occurred.";
        validationProblemDetails.Detail = detail;
        validationProblemDetails.Status = StatusCodes.Status400BadRequest;
        validationProblemDetails.Instance = $"urn:skyvault:error:{correlationId}";
        validationProblemDetails.Type = "https://httpstatuses.com/400";
        validationProblemDetails.Extensions = new Dictionary<string, object>
        {
            { "correlationId", correlationId ??= "Not specified" },
            { "errorCode", errorCode ??= "Not specified" }
        }!;
    }

    public static ValidationProblemDetails ToValidationProblemDetails(
        this ValidationProblemDetails validationProblemDetails,
        string? detail, string? errorCode, string? correlationId)
    {
        SetCommonDetails(validationProblemDetails, detail, errorCode, correlationId);
        return validationProblemDetails;
    }

    public static ValidationProblemDetails ToValidationProblemDetails(
        this ValidationProblemDetails validationProblemDetails,
        string? detail, string? errorCode, string? correlationId, IDictionary<string, string[]> errors)
    {
        SetCommonDetails(validationProblemDetails, detail, errorCode, correlationId);
        validationProblemDetails.Errors = errors;
        return validationProblemDetails;
    }
}