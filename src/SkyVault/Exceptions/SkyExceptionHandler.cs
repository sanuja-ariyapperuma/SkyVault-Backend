using SkyVault.Services;

namespace SkyVault.Exceptions;

//Written for my reference and this class should be used for console applications
public static class SkyExceptionHandler
{
    private static ITelemetryService? _telemetryService;

    public static void Initialize()
    {
        AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;
    }

    public static void Initialize(ITelemetryService telemetryService)
    {
        _telemetryService = telemetryService;
        AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;
    }

    private static void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
        if (e.ExceptionObject is not Exception exception) return;

        var correlationId = Guid.NewGuid().ToString();

        if (_telemetryService != null)
        {
            exception.LogException(correlationId, _telemetryService);
        }
        else
        {
            exception.LogException(correlationId);
        }
    }
}