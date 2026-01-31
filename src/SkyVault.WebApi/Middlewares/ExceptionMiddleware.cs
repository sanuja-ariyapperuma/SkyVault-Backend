using SkyVault.Exceptions;
using System.Net;
using System.Text.Json;

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

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = new
            {
                error = "An unexpected exception has occurred. Details of the exception have been sent to the developers for further investigation.",
                correlationId = correlationId
            };

            var jsonResponse = JsonSerializer.Serialize(response, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            return context.Response.WriteAsync(jsonResponse);
        }
    }
}