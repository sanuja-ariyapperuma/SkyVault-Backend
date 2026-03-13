using SkyVault.Services;

namespace SkyVault.WebApi.Services;

public class TelemetryUsageExample(ITelemetryService telemetryService)
{
    private readonly ITelemetryService _telemetryService = telemetryService;

    public void ExampleUsage()
    {
        // Track custom events
        _telemetryService.TrackEvent("UserLogin", new Dictionary<string, string>
        {
            ["UserId"] = "12345",
            ["LoginMethod"] = "OAuth"
        });

        // Track metrics
        _telemetryService.TrackMetric("DatabaseQueryTime", 150.5, new Dictionary<string, string>
        {
            ["QueryType"] = "SELECT",
            ["TableName"] = "Users"
        });

        // Track traces
        _telemetryService.TrackTrace("Processing user request started", SeverityLevel.Information);
        _telemetryService.TrackTrace("Potential performance issue detected", SeverityLevel.Warning);

        // Track dependency calls (e.g., external API calls)
        var startTime = DateTime.UtcNow;
        var duration = TimeSpan.FromMilliseconds(250);
        
        _telemetryService.TrackDependency(
            dependencyType: "HTTP",
            target: "api.external.com",
            name: "GET /users/12345",
            data: "GET https://api.external.com/users/12345",
            startTime: startTime,
            duration: duration,
            success: true,
            resultCode: 200,
            properties: new Dictionary<string, string>
            {
                ["UserId"] = "12345",
                ["ApiVersion"] = "v1"
            }
        );
    }

    public void ExampleExceptionHandling(Exception exception, string correlationId)
    {
        var properties = new Dictionary<string, string>
        {
            ["ServiceName"] = "TelemetryUsageExample",
            ["Operation"] = "ExampleUsage",
            ["Environment"] = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Unknown"
        };

        _telemetryService.TrackException(exception, correlationId, properties);
    }
}
