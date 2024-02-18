using SkyVault.WebApi.Authentication;
using SkyVault.WebApi.Endpoints;

namespace SkyVault.WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddHealthChecks();
        
        var app = builder.Build();
        app.UseMiddleware<BasicAuthMiddleware>();
        app.MapLoginEndpoints();
        app.MapCustomerEndpoints();
        app.MapHealthChecks("health");

        app.Run();
    }
}