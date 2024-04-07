namespace SkyVault.WebApp;

public class CorrelationIdDelegatingHandler(IHttpContextAccessor httpContextAccessor) : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        //Fail safe implementation to continue even if the HttpContext is not available.
        if (httpContextAccessor.HttpContext?.Items == null) return await base.SendAsync(request, cancellationToken);
        
        var correlationId = (httpContextAccessor.HttpContext.Items["X-Correlation-ID"] ??= "Not Available") as string;
        
        request.Headers.Add("X-Correlation-ID", correlationId);
        
        return await base.SendAsync(request, cancellationToken);
    }
}