namespace SkyVault.Services;

public interface ITelemetryService
{
    void TrackException(Exception exception, string? correlationId = null, Dictionary<string, string>? properties = null);
    void TrackEvent(string eventName, Dictionary<string, string>? properties = null);
    void TrackMetric(string metricName, double value, Dictionary<string, string>? properties = null);
    void TrackTrace(string message, SeverityLevel severity = SeverityLevel.Information, Dictionary<string, string>? properties = null);
    void TrackDependency(string dependencyType, string target, string name, string data, DateTime startTime, TimeSpan duration, bool success, int resultCode = 0, Dictionary<string, string>? properties = null);
    void TrackRequest(string name, DateTime startTime, TimeSpan duration, string responseCode, bool success, string? url = null, Dictionary<string, string>? properties = null);
}

public enum SeverityLevel
{
    Verbose,
    Information,
    Warning,
    Error,
    Critical
}
