using Microsoft.EntityFrameworkCore;
using SkyVault.WebApi.Authentication;
using SkyVault.WebApi.Backend.Models;
using SkyVault.WebApi.Endpoints;
using SkyVault.WebApi.MappingProfiles;
using SkyVault.WebApi.Middlewares;

namespace SkyVault.WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddHealthChecks();
        builder.Services.AddDbContext<SkyvaultContext>(options =>
        {
            options.UseMySql(builder.Configuration.GetConnectionString("MySQLConnection"), new MySqlServerVersion(new Version(8, 0)));
        });
        builder.Services.AddAutoMapper(typeof(Program).Assembly,typeof(MappingProfile).Assembly);

        var app = builder.Build();
        app.UseMiddleware<BasicAuthMiddleware>();
        app.MapLoginEndpoints();
        app.MapCustomerEndpoints();
        app.MapProfileEndpoints();
        app.MapHealthChecks("health");
        app.UseMiddleware<ExceptionMiddleware>();
        app.Run();
    }
}
