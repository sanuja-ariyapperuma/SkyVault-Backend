using Serilog;
using SkyVault.Services;

namespace SkyVault.Exceptions;

public static class SkyExceptionExtension
{
    public static void LogException(this Exception exception, string? correlationId)
    {
        // Log to Serilog as fallback
        Log.Error(exception, "An exception occured with correlationId: {correlationId}", correlationId);
    }

    public static void LogException(this Exception exception, string? correlationId, ITelemetryService telemetryService)
    {
        // Log to both Serilog and Application Insights
        Log.Error(exception, "An exception occured with correlationId: {correlationId}", correlationId);
        
        var properties = new Dictionary<string, string>
        {
            ["ExceptionType"] = exception.GetType().Name,
            ["ExceptionMessage"] = exception.Message,
            ["StackTrace"] = exception.StackTrace ?? "No stack trace available"
        };

        telemetryService.TrackException(exception, correlationId, properties);
    }
}