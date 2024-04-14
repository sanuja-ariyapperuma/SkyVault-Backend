namespace SkyVault.WebApi.Helper;

internal static class CorrelationHandler
{
    public static string Get(HttpContext context)
    {
        if (context.Request.Headers.TryGetValue("X-Correlation-ID", out var correlationId))
        {
            return correlationId!;
        }

        return "Not Available";
    }
}