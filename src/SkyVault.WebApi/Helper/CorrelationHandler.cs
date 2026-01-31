namespace SkyVault.WebApi.Helper;

internal static class CorrelationHandler
{
    private const string CorrelationIdHeaderKey = "X-Correlation-ID";
    
    public static string Get(HttpContext context)
    {
        if (context.Items.TryGetValue(CorrelationIdHeaderKey, out var correlationId) && 
            correlationId is string id && !string.IsNullOrEmpty(id))
        {
            return id;
        }

        // Fallback to request headers if not in Items (shouldn't happen if middleware is properly ordered)
        if (context.Request.Headers.TryGetValue(CorrelationIdHeaderKey, out var headerCorrelationId))
        {
            return headerCorrelationId!;
        }

        return "Not Available";
    }
}