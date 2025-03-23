using SkyVault.Exceptions;
using System.Net;

namespace SkyVault.WebApi.Middlewares
{
    public sealed class ExceptionMiddleware(RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch (Exception ex)
            {
                var correlationId = (httpContext.Items["X-Correlation-ID"] ??= "Not Available") as string;

                //Send to central exception handler
                ex.LogException(correlationId);

                await HandleException(httpContext, ex);
            }
        }

        private static Task HandleException(HttpContext context, Exception exception)
        {
            var correlationId = (context.Items["X-Correlation-ID"] ??= "Not Available") as string;

            context.Response.ContentType = "text/plain";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            return context.Response.WriteAsync("An unexpected exception had occured. " +
                                               "Details of the exception has been sent to the developers for further investigation. CorrelationId: " +
                                               correlationId);
        }
    }
}