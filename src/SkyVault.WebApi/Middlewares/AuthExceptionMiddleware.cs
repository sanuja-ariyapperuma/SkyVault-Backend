using System.Text.Json;

namespace SkyVault.WebApi.Middlewares;

public class AuthExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<AuthExceptionMiddleware> _logger;

    public AuthExceptionMiddleware(RequestDelegate next, ILogger<AuthExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (InvalidOperationException ex) when (
            ex.Message.Contains("authentication", StringComparison.OrdinalIgnoreCase) ||
            ex.Message.Contains("AzureAd", StringComparison.OrdinalIgnoreCase) ||
            ex.Message.Contains("JwtBearer", StringComparison.OrdinalIgnoreCase) ||
            ex.Message.Contains("MicrosoftIdentity", StringComparison.OrdinalIgnoreCase))
        {
            _logger.LogCritical(ex, "Authentication configuration error. Check AzureAD settings.");
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";

            var response = new
            {
                error = "Authentication service misconfigured",
                correlationId = context.Items["X-Correlation-ID"]?.ToString()
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
        catch (ArgumentException ex) when (
            ex.Message.Contains("authority", StringComparison.OrdinalIgnoreCase) ||
            ex.Message.Contains("tenant", StringComparison.OrdinalIgnoreCase) ||
            ex.Message.Contains("clientid", StringComparison.OrdinalIgnoreCase))
        {
            _logger.LogCritical(ex, "Invalid authentication configuration argument.");
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";

            var response = new
            {
                error = "Invalid authentication configuration",
                correlationId = context.Items["X-Correlation-ID"]?.ToString()
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
