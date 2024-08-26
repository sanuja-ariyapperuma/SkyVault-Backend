
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Serilog;
using Serilog.Events;
using SkyVault.WebApi.Authentication;
using SkyVault.WebApi.Backend.Models;
using SkyVault.WebApi.Endpoints;
using SkyVault.WebApi.MappingProfiles;
using SkyVault.WebApi.Middlewares;

namespace SkyVault.WebApi;

public static class Program
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

        //builder.Services.AddMicrosoftGraph(options => {
        //    options.Scopes = ["User.Read"];
        //});


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

        Log.Logger = new LoggerConfiguration()
        .ReadFrom.Configuration(builder.Configuration)
        .MinimumLevel.Information()
        .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
        .Enrich.FromLogContext()
        .WriteTo.File(builder.Configuration["Logging:FilePath"]!, rollingInterval: RollingInterval.Day)
        .CreateLogger();

        builder.Host.UseSerilog();

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
