using Microsoft.AspNetCore.Mvc;
using SkyVault.Exceptions;
using SkyVault.Payloads.RequestPayloads;
using SkyVault.Payloads.ResponsePayloads;
using SkyVault.WebApi.Backend;
using SkyVault.WebApi.Backend.Models;
using SkyVault.WebApi.Helper;

namespace SkyVault.WebApi.Workloads;

internal static class AuthenticationWorkload
{
    public static IResult AuthenticateUser([FromBody] ValidateUserRequest request,
        HttpContext context, SkyvaultContext dbContext)
    {
        var correlationId = context.Items["X-Correlation-ID"]?.ToString();

        var propertiesToCheck = new[]
        {
            request.Upn, request.Email,
            request.LastName, request.FirstName, request.Role
        };

        if (propertiesToCheck.Any(string.IsNullOrWhiteSpace))
        {
            return Results.Problem(new ValidationProblemDetails().ToValidationProblemDetails(
                "One or more required fields are missing. Please provide all required fields.",
                "67010904-0000", correlationId));
        }

        var systemUserData = new SystemUserData(dbContext);
        var result = systemUserData.CreateOrGetUser(request, correlationId);

        if (!result.Succeeded)
            return Results.Problem(new ProblemDetails().ToProblemDetails(result.Message,
                result.ErrorCode, result.CorrelationId));

        var sysUser = result.Value;

        return Results.Ok(new WelcomeUserResponse(
            sysUser!.Id.ToString(),
            sysUser!.SamProfileId,
            $"{sysUser.FirstName} {sysUser.LastName}",
            DateTime.Today.ToLongDateString(),
            sysUser.UserRole,
            sysUser.SamProfileId
        ));
    }
}