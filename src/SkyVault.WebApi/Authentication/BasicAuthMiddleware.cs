namespace SkyVault.WebApi.Authentication;

internal sealed class BasicAuthMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IConfiguration _configuration;

    private const string EmptyApiAuthKey = "API authentication has not been configured.";

    public BasicAuthMiddleware(RequestDelegate next, IConfiguration configuration)
    {
        _next = next;
        _configuration = configuration;
    }

    public async Task Invoke(HttpContext context)
    {
        var apiKey = _configuration.GetValue<string>(AuthKeys.ApiKey)
            ?? throw new InvalidOperationException(EmptyApiAuthKey);
        
        if (!context.Request.Headers.TryGetValue(AuthKeys.ApiAuthKey, 
                out var extractedApiKey))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return;
        }

        if (apiKey!.Equals(extractedApiKey) == false)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return;
        }

        await _next(context);
    }
}