using Serilog;
using Serilog.Context;
using Serilog.Core;
using Serilog.Events;

namespace SkyVault.Services;

public class ApplicationInsightsTelemetryService : ITelemetryService
{
    private readonly ILogger _logger;

    public ApplicationInsightsTelemetryService()
    {
        _logger = Log.ForContext<ApplicationInsightsTelemetryService>();
    }

    public void TrackException(Exception exception, string? correlationId = null, Dictionary<string, string>? properties = null)
    {
        using var logContext = LogContext.Push(new ExceptionEnricher(correlationId, properties));
        
        _logger.Error(exception, "Exception tracked with correlationId: {correlationId}", correlationId ?? "None");
    }

    public void TrackEvent(string eventName, Dictionary<string, string>? properties = null)
    {
        using var logContext = LogContext.Push(new EventEnricher(eventName, properties));
        
        _logger.Information("Custom event: {eventName}", eventName);
    }

    public void TrackMetric(string metricName, double value, Dictionary<string, string>? properties = null)
    {
        using var logContext = LogContext.Push(new MetricEnricher(metricName, value, properties));
        
        _logger.Information("Metric tracked: {metricName} = {metricValue}", metricName, value);
    }

    public void TrackTrace(string message, SeverityLevel severity = SeverityLevel.Information, Dictionary<string, string>? properties = null)
    {
        using var logContext = LogContext.Push(new TraceEnricher(properties));
        
        var logLevel = severity switch
        {
            SeverityLevel.Verbose => LogEventLevel.Verbose,
            SeverityLevel.Information => LogEventLevel.Information,
            SeverityLevel.Warning => LogEventLevel.Warning,
            SeverityLevel.Error => LogEventLevel.Error,
            SeverityLevel.Critical => LogEventLevel.Fatal,
            _ => LogEventLevel.Information
        };

        _logger.Write(logLevel, "Trace message: {message}", message);
    }

    public void TrackDependency(string dependencyType, string target, string name, string data, DateTime startTime, TimeSpan duration, bool success, int resultCode = 0, Dictionary<string, string>? properties = null)
    {
        using var logContext = LogContext.Push(new DependencyEnricher(dependencyType, target, name, data, duration, success, resultCode, properties));
        
        var logLevel = success ? LogEventLevel.Information : LogEventLevel.Error;

        _logger.Write(logLevel, "Dependency call: {dependencyType} to {target} - {name} took {duration}ms - Success: {success}", 
                      dependencyType, target, name, duration.TotalMilliseconds, success);
    }

    public void TrackRequest(string name, DateTime startTime, TimeSpan duration, string responseCode, bool success, string? url = null, Dictionary<string, string>? properties = null)
    {
        using var logContext = LogContext.Push(new RequestEnricher(name, duration, responseCode, success, url, properties));
        
        var logLevel = success ? LogEventLevel.Information : LogEventLevel.Error;

        _logger.Write(logLevel, "Request: {name} to {url} took {duration}ms - Response: {responseCode} - Success: {success}", 
                      name, url ?? "Unknown", duration.TotalMilliseconds, responseCode, success);
    }
}

// Helper enricher classes
public class ExceptionEnricher : ILogEventEnricher
{
    private readonly string? _correlationId;
    private readonly Dictionary<string, string>? _properties;

    public ExceptionEnricher(string? correlationId, Dictionary<string, string>? properties)
    {
        _correlationId = correlationId;
        _properties = properties;
    }

    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        if (!string.IsNullOrEmpty(_correlationId))
        {
            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("CorrelationId", _correlationId));
        }

        if (_properties != null)
        {
            foreach (var prop in _properties)
            {
                logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty(prop.Key, prop.Value));
            }
        }
    }
}

public class EventEnricher : ILogEventEnricher
{
    private readonly string _eventName;
    private readonly Dictionary<string, string>? _properties;

    public EventEnricher(string eventName, Dictionary<string, string>? properties)
    {
        _eventName = eventName;
        _properties = properties;
    }

    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("EventName", _eventName));

        if (_properties != null)
        {
            foreach (var prop in _properties)
            {
                logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty(prop.Key, prop.Value));
            }
        }
    }
}

public class MetricEnricher : ILogEventEnricher
{
    private readonly string _metricName;
    private readonly double _metricValue;
    private readonly Dictionary<string, string>? _properties;

    public MetricEnricher(string metricName, double metricValue, Dictionary<string, string>? properties)
    {
        _metricName = metricName;
        _metricValue = metricValue;
        _properties = properties;
    }

    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("MetricName", _metricName));
        logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("MetricValue", _metricValue));

        if (_properties != null)
        {
            foreach (var prop in _properties)
            {
                logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty(prop.Key, prop.Value));
            }
        }
    }
}

public class TraceEnricher : ILogEventEnricher
{
    private readonly Dictionary<string, string>? _properties;

    public TraceEnricher(Dictionary<string, string>? properties)
    {
        _properties = properties;
    }

    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        if (_properties != null)
        {
            foreach (var prop in _properties)
            {
                logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty(prop.Key, prop.Value));
            }
        }
    }
}

public class DependencyEnricher : ILogEventEnricher
{
    private readonly string _dependencyType;
    private readonly string _target;
    private readonly string _name;
    private readonly string _data;
    private readonly TimeSpan _duration;
    private readonly bool _success;
    private readonly int _resultCode;
    private readonly Dictionary<string, string>? _properties;

    public DependencyEnricher(string dependencyType, string target, string name, string data, TimeSpan duration, bool success, int resultCode, Dictionary<string, string>? properties)
    {
        _dependencyType = dependencyType;
        _target = target;
        _name = name;
        _data = data;
        _duration = duration;
        _success = success;
        _resultCode = resultCode;
        _properties = properties;
    }

    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("DependencyType", _dependencyType));
        logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("DependencyTarget", _target));
        logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("DependencyName", _name));
        logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("DependencyData", _data));
        logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("DependencyDuration", _duration.TotalMilliseconds));
        logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("DependencySuccess", _success));
        logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("DependencyResultCode", _resultCode));

        if (_properties != null)
        {
            foreach (var prop in _properties)
            {
                logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty(prop.Key, prop.Value));
            }
        }
    }
}

public class RequestEnricher : ILogEventEnricher
{
    private readonly string _name;
    private readonly TimeSpan _duration;
    private readonly string _responseCode;
    private readonly bool _success;
    private readonly string? _url;
    private readonly Dictionary<string, string>? _properties;

    public RequestEnricher(string name, TimeSpan duration, string responseCode, bool success, string? url, Dictionary<string, string>? properties)
    {
        _name = name;
        _duration = duration;
        _responseCode = responseCode;
        _success = success;
        _url = url;
        _properties = properties;
    }

    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("RequestName", _name));
        logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("RequestDuration", _duration.TotalMilliseconds));
        logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("RequestResponseCode", _responseCode));
        logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("RequestSuccess", _success));

        if (!string.IsNullOrEmpty(_url))
        {
            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("RequestUrl", _url));
        }

        if (_properties != null)
        {
            foreach (var prop in _properties)
            {
                logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty(prop.Key, prop.Value));
            }
        }
    }
}
