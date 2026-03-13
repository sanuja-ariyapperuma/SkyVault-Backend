using SkyVault.Services;
using System.Diagnostics;

namespace SkyVault.WebApi.Middlewares;

public sealed class RequestTrackingMiddleware(RequestDelegate next, ITelemetryService telemetryService)
{
    private readonly ITelemetryService _telemetryService = telemetryService;

    public async Task InvokeAsync(HttpContext httpContext)
    {
        var correlationId = (httpContext.Items["X-Correlation-ID"] ??= Guid.NewGuid().ToString()) as string ?? Guid.NewGuid().ToString();
        var stopwatch = Stopwatch.StartNew();
        var requestName = $"{httpContext.Request.Method} {httpContext.Request.Path}";
        var startTime = DateTime.UtcNow;

        try
        {
            await next(httpContext);
        }
        finally
        {
            stopwatch.Stop();
            var duration = stopwatch.Elapsed;
            var responseCode = httpContext.Response.StatusCode.ToString();
            var success = httpContext.Response.StatusCode < 400;

            var properties = new Dictionary<string, string>
            {
                ["CorrelationId"] = correlationId,
                ["UserAgent"] = httpContext.Request.Headers["User-Agent"].ToString(),
                ["RemoteIpAddress"] = httpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown"
            };

            _telemetryService.TrackRequest(
                name: requestName,
                startTime: startTime,
                duration: duration,
                responseCode: responseCode,
                success: success,
                url: httpContext.Request.Path + httpContext.Request.QueryString,
                properties: properties
            );
        }
    }
}
