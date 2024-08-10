
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
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
        var MyAllowSpecificOrigins = "AllowLocalhost";

        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddHealthChecks();
        builder.Services.AddDbContext<SkyvaultContext>(options =>
        {
            options.UseMySql(builder.Configuration.GetConnectionString("Localconnection"), new MySqlServerVersion(new Version(8, 0)));
        });
        builder.Services.AddAutoMapper(typeof(Program).Assembly,typeof(MappingProfile).Assembly);

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(options =>
    {
        builder.Configuration.Bind("AzureAd", options);
    }, options =>
    {
        builder.Configuration.Bind("AzureAd", options);
    });

        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("RequireAppRole", policy => policy.RequireClaim("roles", "YourAppRole"));
        });



        builder.Services.AddCors(options =>
        {
            options.AddPolicy(MyAllowSpecificOrigins,
                builder =>
                {
                    builder.WithOrigins("http://localhost:5173")
                           .AllowAnyHeader()
                           .AllowAnyMethod()
                           .AllowCredentials();
                });
        });

        builder.Services.AddAuthorization();

        var app = builder.Build();
        app.UseMiddleware<CorrelationIdMiddleware>();
        //app.UseMiddleware<BasicAuthMiddleware>();
        app.MapLoginEndpoints();
        app.MapCustomEndpoints();
        app.MapProfileEndpoints();
        app.MapHealthChecks("/health");
        app.UseCors(MyAllowSpecificOrigins);
        app.UseAuthentication();
        app.UseAuthorization();
        //app.UseMiddleware<ExceptionMiddleware>();
        app.Run();
    }
}
