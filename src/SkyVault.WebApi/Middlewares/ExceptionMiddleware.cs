using System.Net;
using SkyVault.Exceptions;

namespace SkyVault.WebApi.Middlewares
{
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
                //Send to central exception handler
                ex.LogException();

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
}
