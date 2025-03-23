
using DotNetEnv;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Events;
using SkyVault.WebApi.Backend;
using SkyVault.WebApi.Backend.Models;
using SkyVault.WebApi.Endpoints;
using SkyVault.WebApi.MappingProfiles;
using SkyVault.WebApi.Middlewares;
using System.Globalization;
using System.Text.Json;

namespace SkyVault.WebApi;

public static class Program
{
    public static void Main(string[] args)
    {
        var MyAllowSpecificOrigins = "AllowLocalhost";

        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddHttpContextAccessor();

        Env.Load();


        builder.Services.AddDbContext<SkyvaultContext>(options =>
        {
            var connectionString = Environment.GetEnvironmentVariable("TravelChannelCRMConnectionString");
            options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0)));
        });

        builder.Services.AddHealthChecks().AddDbContextCheck<SkyvaultContext>(name: "Database");
        builder.Services.AddAutoMapper(typeof(Program).Assembly, typeof(MappingProfile).Assembly);
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

        builder.Services.AddCors(options =>
        {
            options.AddPolicy(MyAllowSpecificOrigins,
                builder =>
                {
                    builder.WithOrigins("http://localhost:5173", "https://travelchannelcrm.com", "https://www.travelchannelcrm.com")
                           .AllowAnyHeader()
                           .AllowAnyMethod()
                           .AllowCredentials();
                });
        });

        builder.Services.AddMemoryCache();

        builder.Services.AddScoped<CacheService>();
        builder.Services.AddHttpContextAccessor();

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

        Log.Information($"{builder.Environment.EnvironmentName} : API is starting up");

        builder.Host.UseSerilog();

        var app = builder.Build();
        app.UseMiddleware<CorrelationIdMiddleware>();
        app.MapLoginEndpoints();
        app.MapCustomEndpoints();
        app.MapMessageEndpoints();
        app.MapProfileEndpoints();
        app.UseCors(MyAllowSpecificOrigins);
        app.UseAuthentication();
        app.UseAuthorization();
        //app.UseMiddleware<ExceptionMiddleware>();

        var supportedCultures = new[] { new CultureInfo("en-GB") };

        app.UseRequestLocalization(new RequestLocalizationOptions
        {
            DefaultRequestCulture = new RequestCulture("en-GB"),
            SupportedCultures = supportedCultures,
            SupportedUICultures = supportedCultures
        });

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
