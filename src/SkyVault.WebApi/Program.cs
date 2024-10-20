
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Events;
using SkyVault.WebApi.Authentication;
using SkyVault.WebApi.Backend.Models;
using SkyVault.WebApi.Endpoints;
using SkyVault.WebApi.MappingProfiles;
using SkyVault.WebApi.Middlewares;
using System.Text.Json;

namespace SkyVault.WebApi;

public static class Program
{
    public static void Main(string[] args)
    {
        var MyAllowSpecificOrigins = "AllowLocalhost";

        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddHttpContextAccessor();
        
        builder.Services.AddDbContext<SkyvaultContext>(options =>
        {
            options.UseMySql(builder.Configuration.GetConnectionString("Localconnection"), new MySqlServerVersion(new Version(8, 0)));
        });

        builder.Services.AddHealthChecks().AddDbContextCheck<SkyvaultContext>(name: "Database");


        builder.Services.AddAutoMapper(typeof(Program).Assembly,typeof(MappingProfile).Assembly);


        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(options =>
    {
        builder.Configuration.Bind("AzureAd", options);
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    }, options =>
    {
        builder.Configuration.Bind("AzureAd", options);
    });

        //builder.Services.AddAuthorization();

        //builder.Services.AddAuthorization(options =>
        //{
        //    options.AddPolicy("RequireAppRole", policy => policy.RequireClaim("roles", "YourAppRole"));
        //});

        //builder.Services.AddMicrosoftGraph(options => {
        //    options.Scopes = ["User.Read"];
        //});


        builder.Services.AddCors(options =>
        {
            options.AddPolicy(MyAllowSpecificOrigins,
                builder =>
                {
                    builder.WithOrigins("http://localhost:5173", "https://witty-wave-03975c200.5.azurestaticapps.net")
                           .AllowAnyHeader()
                           .AllowAnyMethod()
                           .AllowCredentials();
                });
        });



        //Log.Logger = new LoggerConfiguration()
        //.ReadFrom.Configuration(builder.Configuration)
        //.MinimumLevel.Information()
        //.MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
        //.Enrich.FromLogContext()
        //.WriteTo.File(builder.Configuration["Logging:FilePath"]!, rollingInterval: RollingInterval.Day)
        //.WriteTo.Console()
        //.CreateLogger();

        Log.Logger = new LoggerConfiguration()
            .WriteTo.ApplicationInsights(
            new TelemetryConfiguration { InstrumentationKey = builder.Configuration["AzureAppInsight:InstrumentKey"] },
            TelemetryConverter.Traces
            )
            .MinimumLevel.Information()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .WriteTo.File(builder.Configuration["Logging:FilePath"]!, rollingInterval: RollingInterval.Day)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .CreateLogger();

        Log.Information("API is starting up");

        builder.Host.UseSerilog();

        var app = builder.Build();
        app.UseMiddleware<CorrelationIdMiddleware>();
        //app.UseMiddleware<BasicAuthMiddleware>();
        app.MapLoginEndpoints();
        app.MapCustomEndpoints();
        app.MapProfileEndpoints();
        app.UseCors(MyAllowSpecificOrigins);
        app.UseAuthentication();
        app.UseAuthorization();
        //app.UseMiddleware<ExceptionMiddleware>();

        app.MapHealthChecks("/api/health", new HealthCheckOptions
        {
            ResponseWriter = async (context, report) =>
            {
                context.Response.ContentType = "application/json";
                var result = JsonSerializer.Serialize(new
                {
                    status = report.Status.ToString(),
                    checks = report.Entries.Select(e => new
                    {
                        name = e.Key,
                        status = e.Value.Status.ToString(),
                        exception = e.Value.Exception != null ? e.Value.Exception.Message : null,
                        duration = e.Value.Duration.TotalMilliseconds
                    })
                });
                await context.Response.WriteAsync(result);
            }
        });

        app.Run();
    }
}
