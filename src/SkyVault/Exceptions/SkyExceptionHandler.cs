namespace SkyVault.Exceptions;

public static class SkyExceptionHandler
{
    public static void Initialize()
    {
        AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;
    }

    private static void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
        if (e.ExceptionObject is not Exception exception) return;
        
        exception.SendToLog();
    }
}