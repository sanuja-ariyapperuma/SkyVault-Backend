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

    public static SkyResult<bool> AuthenticateUser(
        [FromBody] LoginUserRequest request,
        HttpContext context,
        SkyvaultContext dbContext
        )
    {
        var accessToken = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        var correlationId = context.Items["X-Correlation-ID"]?.ToString() ?? "";

        if (request.Upn == null)
            return new SkyResult<bool>().Fail("Cannot find the upn", "2ac5059f-0005", correlationId);

        if (request.UserRole == null)
            return new SkyResult<bool>().Fail("Cannot find the user role", "2ac5059f-0006", correlationId);

        var systemUserData = new SystemUserData(dbContext);

        var firstname = context.User.FindFirst("name")?.Value ?? "";
        var lastName = context.User.FindFirst(ClaimTypes.Surname)?.Value ?? "";

        Payloads.CommonPayloads.SystemUserRole userRole;
        
        switch (request.UserRole)
        {
            case "SuperAdmin":
                userRole = Payloads.CommonPayloads.SystemUserRole.SuperAdmin;
                break;
            case "Admin":
                userRole = Payloads.CommonPayloads.SystemUserRole.Admin;
                break;
            case "Staff":
                userRole = Payloads.CommonPayloads.SystemUserRole.Staff;
                break;
            default:
                return new SkyResult<bool>().Fail("Invalid role", "2ac5059f-0007", correlationId);
        }

        var loginUser = new SystemUserCreateOrUpdateDto(request.Upn, firstname, lastName, userRole);

        var result = systemUserData.CreateOrGetUser(loginUser, correlationId);

        if (!result.Succeeded)
            return new SkyResult<bool>().Fail(result.Message, result.ErrorCode, result.CorrelationId);

        var sysUser = result.Value;
        
        if (sysUser == null)
            return new SkyResult<bool>().Fail("User not found", "2ac5059f-0008", correlationId);

        var cookieOptions = new CookieOptions
        {
            HttpOnly = false,
            Secure = true, // Only send over HTTPS
            SameSite = SameSiteMode.Lax, // Mitigates CSRF attacks
            Expires = DateTime.UtcNow.AddMinutes(60) // Match token expiry
        };

        var cookieData = new AuthenticatedUser(
            $"{sysUser.FirstName ?? ""} {sysUser.LastName ?? ""}",
            sysUser.UserRole?.ToString() ?? "",
            accessToken!
        );

        var serializedCookieData = JsonSerializer.Serialize(cookieData);
        var encodedCookieData = Convert.ToBase64String(Encoding.UTF8.GetBytes(serializedCookieData));

        // Set the access token in a cookie
        context.Response.Cookies.Append("SkyVault", encodedCookieData, cookieOptions);

        return new SkyResult<bool>().SucceededWithValue(true);
    }
}