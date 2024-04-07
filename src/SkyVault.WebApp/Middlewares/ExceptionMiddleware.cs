using System.Net;
using SkyVault.Exceptions;

namespace SkyVault.WebApp.Middlewares;

public class ExceptionMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await next(httpContext);
        }
        catch (Exception ex)
        {
            var correlationId = httpContext.Items["X-Correlation-ID"]?.ToString();
            
            //Send to central exception handler
            ex.LogException(correlationId);
            
            await HandleException(httpContext, ex);
        }
    }

    private static Task HandleException(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "text/plain";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        return context.Response.WriteAsync("An unexpected exception had occured. " +
                                           "Details of the exception has been sent to the developers for further investigation.");
    }
    
}