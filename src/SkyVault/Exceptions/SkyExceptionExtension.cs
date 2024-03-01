namespace SkyVault.Exceptions;

public static class SkyExceptionExtension
{
    //Placeholder code for the actual Azure Application Insight code so I wont forget what to do.
    private static object? _telemetryClient;

    private static object GetTelemetryClient()
    {
        return _telemetryClient ??= new object();
    }

    public static void SendToLog(this Exception exception)
    {
        //Need to implement connectivity to Azure Application Insights for central log management
        Console.WriteLine(exception.Message);
    }
}