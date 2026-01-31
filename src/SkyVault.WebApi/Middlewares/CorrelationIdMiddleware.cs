namespace SkyVault.WebApi.Middlewares;

public sealed class CorrelationIdMiddleware(RequestDelegate next)
{
    private const string CorrelationIdHeaderKey = "X-Correlation-ID";

    public async Task InvokeAsync(HttpContext context)
    {
        var correlationId = context.Request.Headers[CorrelationIdHeaderKey].ToString();

        if (string.IsNullOrEmpty(correlationId))
        {
            correlationId = Guid.NewGuid().ToString();
            context.Request.Headers[CorrelationIdHeaderKey] = correlationId;
        }

        context.Items[CorrelationIdHeaderKey] = correlationId;
        context.Response.Headers[CorrelationIdHeaderKey] = correlationId;

        await next(context);
    }
}