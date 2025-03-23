using Microsoft.AspNetCore.Mvc;
using SkyVault.Payloads.CommonPayloads;
using SkyVault.Payloads.RequestPayloads;
using SkyVault.WebApi.Backend;
using SkyVault.WebApi.Backend.Models;
using SkyVault.WebApi.Helper;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace SkyVault.WebApi.Workloads;

internal static class AuthenticationWorkload
{

    public static IResult AuthenticateUser(
        [FromBody] LoginUserRequest request,
        HttpContext context,
        SkyvaultContext dbContext
        )
    {
        var accessToken = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        var correlationId = context.Items["X-Correlation-ID"]?.ToString();

        if (request.Upn == null)
            return Results.Problem(new ProblemDetails().ToProblemDetails("Cannot find the upn",
                "", correlationId));

        if (request.UserRole == null)
            return Results.Problem(new ProblemDetails().ToProblemDetails("Cannot find the user role",
                "", correlationId));

        var systemUserData = new SystemUserData(dbContext);


        var firstname = context.User.FindFirst("name")?.Value;
        var lastName = context.User.FindFirst(ClaimTypes.Surname)?.Value;

        Payloads.CommonPayloads.SystemUserRole userRole = request.UserRole switch
        {
            "SuperAdmin" => Payloads.CommonPayloads.SystemUserRole.SuperAdmin,
            "Admin" => Payloads.CommonPayloads.SystemUserRole.Admin,
            "Staff" => Payloads.CommonPayloads.SystemUserRole.Staff,
            _ => throw new ArgumentException("Invalid role")
        };

        var loginUser = new SystemUserCreateOrUpdateDto(request.Upn, firstname, lastName, userRole);

        var result = systemUserData.CreateOrGetUser(loginUser, correlationId);

        if (!result.Succeeded)
            return Results.Problem(new ProblemDetails().ToProblemDetails(result.Message,
                result.ErrorCode, result.CorrelationId));

        var cookieOptions = new CookieOptions
        {
            HttpOnly = false,
            Secure = true, // Only send over HTTPS
            SameSite = SameSiteMode.Lax, // Mitigates CSRF attacks
            Expires = DateTime.UtcNow.AddMinutes(60) // Match token expiry
        };

        var sysUser = result.Value;

        var cookieData = new AuthenticatedUser(
            $"{sysUser.FirstName} {sysUser.LastName}",
            sysUser.UserRole!.ToString(),
            accessToken!
        );

        var serializedCookieData = JsonSerializer.Serialize(cookieData);
        var encodedCookieData = Convert.ToBase64String(Encoding.UTF8.GetBytes(serializedCookieData));

        // Set the access token in a cookie
        context.Response.Cookies.Append("TravelChannel", encodedCookieData, cookieOptions);

        return Results.Ok();
    }
}