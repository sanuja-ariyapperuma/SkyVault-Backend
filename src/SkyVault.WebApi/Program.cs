using DotNetEnv;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Serilog;
using Serilog.Events;
using SkyVault.WebApi.Backend;
using SkyVault.WebApi.Backend.Models;
using SkyVault.WebApi.Endpoints;
using SkyVault.WebApi.Helper;
using SkyVault.WebApi.MappingProfiles;
using SkyVault.WebApi.Middlewares;
using System.Globalization;

namespace SkyVault.WebApi;

public static class Program
{
    public static void Main(string[] args)
    {
        // Load .env file variables into Environment variables
        Env.Load();

        var builder = WebApplication.CreateBuilder(args);

        // Configure Serilog
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .MinimumLevel.Information()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .CreateLogger();

        builder.Host.UseSerilog();

        var env = builder.Environment.EnvironmentName;
        Log.Information($"{env} : API is starting up");

        var isDevOrLocal = builder.Environment.IsDevelopment() || env == "Local";

        // Database Context
        builder.Services.AddDbContext<SkyvaultContext>(options =>
        {
            // Checking for DB_CONNECTION_STRING from .env or DefaultConnection from appsettings
            var connectionString = builder.Configuration["DB_CONNECTION_STRING"] 
                                   ?? builder.Configuration.GetConnectionString("DefaultConnection");
            
            options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0)));
        });

        builder.Services.AddHealthChecks().AddDbContextCheck<SkyvaultContext>(name: "Database");
        builder.Services.AddAutoMapper(typeof(Program).Assembly, typeof(MappingProfile).Assembly);
        
        // Azure AD Authentication
        // AddMicrosoftIdentityWebApi binds the "AzureAd" section to MicrosoftIdentityOptions.
        // Ensure your .env variables use double underscores for nesting, e.g., AZUREAD__TENANTID
        // Use Configuration to get connection string (supports .env via AZUREAD__... or direct keys if mapped)
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

        // Configure additional TokenValidationParameters
        builder.Services.Configure<JwtBearerOptions>(JwtBearerDefaults.AuthenticationScheme, options =>
        {
            options.TokenValidationParameters.ValidateIssuer = true;
            options.TokenValidationParameters.ValidateAudience = true;
            options.TokenValidationParameters.ValidateLifetime = true;
            options.TokenValidationParameters.ClockSkew = TimeSpan.Zero;
        });

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("DefaultCorsPolicy", corsBuilder =>
            {
                corsBuilder.AllowAnyHeader()
                           .AllowAnyMethod()
                           .AllowCredentials();

                if (isDevOrLocal)
                {
                    corsBuilder.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost");
                }
                else
                {
                    var allowedOrigins = CorsHelper.GetAllowedOrigins();
                    corsBuilder.WithOrigins(allowedOrigins);
                }
            });
        });

        builder.Services.AddMemoryCache();
        builder.Services.AddScoped<CacheService>();
        builder.Services.AddHttpContextAccessor();

        builder.Services.AddHttpClient<IApiClient, ApiClient>(client =>
        {
            var baseUrl = builder.Configuration["AZURE_FUNCTION_BASE_URL"];
            if (!string.IsNullOrEmpty(baseUrl))
            {
                client.BaseAddress = new Uri(baseUrl);
            }
        });

        builder.Services.AddControllers();
        
        var app = builder.Build();
        
        app.UseMiddleware<ExceptionMiddleware>();
        app.UseMiddleware<CorrelationIdMiddleware>();
        
        // CORS must be before Authentication and Authorization
        app.UseCors("DefaultCorsPolicy");
        
        app.UseAuthentication();
        app.UseAuthorization();
        
        /* Route Mapping Start*/
        app.MapLoginEndpoints();
        app.MapCustomEndpoints();
        app.MapMessageEndpoints();
        app.MapProfileEndpoints();
        app.MapTransferProfileEndpoints();
        /* Route Mapping End*/
        
        app.MapGet("/secure", () => "Hello from protected API")
            .RequireAuthorization();
        
        var supportedCultures = new[] { new CultureInfo("en-GB") };

        app.UseRequestLocalization(new RequestLocalizationOptions
        {
            DefaultRequestCulture = new RequestCulture("en-GB"),
            SupportedCultures = supportedCultures,
            SupportedUICultures = supportedCultures
        });

        app.Start();

        foreach (var url in app.Urls)
        {
            Log.Information($"Listening on: {url}");
        }

        app.WaitForShutdown();
    }
}
