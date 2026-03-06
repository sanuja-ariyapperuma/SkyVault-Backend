using DotNetEnv;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
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
            // Explicitly prioritize DB_CONNECTION_STRING environment variable
            var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING") 
                                   ?? builder.Configuration["DB_CONNECTION_STRING"]
                                   ?? builder.Configuration.GetConnectionString("DefaultConnection")
                                   ?? throw new InvalidOperationException("No database connection string configured. Set DB_CONNECTION_STRING environment variable or configure DefaultConnection.");
            
            Log.Information($"Using connection string: Server={ExtractServerFromConnectionString(connectionString)}");
            options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0)));
        });

        builder.Services.AddHealthChecks()
            .AddDbContextCheck<SkyvaultContext>(name: "Database");
        
        builder.Services.AddAutoMapper(typeof(Program).Assembly, typeof(MappingProfile).Assembly);
        
        // Azure AD Authentication
        // AddMicrosoftIdentityWebApi binds the "AzureAD" section to MicrosoftIdentityOptions.
        // Ensure your .env variables use double underscores for nesting, e.g., AZUREAD__TENANTID
        // Use Configuration to get connection string (supports .env via AZUREAD__... or direct keys if mapped)
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAD"));

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
                corsBuilder.WithExposedHeaders("X-Correlation-ID")
                           .WithHeaders("Authorization", "Content-Type", "Accept", "X-Correlation-ID")
                           .WithMethods("GET", "POST", "PUT", "DELETE", "PATCH", "OPTIONS");

                if (isDevOrLocal)
                {
                    corsBuilder.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost")
                               .AllowCredentials();
                }
                else
                {
                    var allowedOrigins = CorsHelper.GetAllowedOrigins();
                    corsBuilder.WithOrigins(allowedOrigins)
                               .AllowCredentials();
                }
            });
        });

        builder.Services.AddMemoryCache(options =>
        {
            // Limit the cache size to prevent unbounded memory growth
            options.SizeLimit = 1024; // Number of entries (adjust as needed)

            // Default expiration for entries (can be overridden per entry)
            options.CompactionPercentage = 0.05; // Remove 5% of entries when limit is reached

            // Set a sliding expiration default (optional)
            // options.SlidingExpiration = TimeSpan.FromMinutes(5);
        });
        builder.Services.AddScoped<CacheService>();

        // Add OpenAPI/Swagger support (development only)
        if (builder.Environment.IsDevelopment())
        {
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.OpenApiInfo
                {
                    Title = "SkyVault API",
                    Version = "v1",
                    Description = "SkyVault backend API"
                });
                
                // Add JWT Bearer authentication to Swagger
                options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.OpenApiSecurityScheme
                {
                    In = Microsoft.OpenApi.ParameterLocation.Header,
                    Description = "Please enter JWT with Bearer into field",
                    Name = "Authorization",
                    Type = Microsoft.OpenApi.SecuritySchemeType.Http,
                    Scheme = "bearer"
                });
            });
        }

        builder.Services.AddHttpClient<IApiClient, ApiClient>(client =>
        {
            var baseUrl = builder.Configuration["AZURE_FUNCTION_BASE_URL"];
            if (!string.IsNullOrEmpty(baseUrl))
            {
                client.BaseAddress = new Uri(baseUrl);
            }
        });
        
        var app = builder.Build();
        
        // Correlation ID should be first so all downstream logs/middleware get the ID
        app.UseMiddleware<CorrelationIdMiddleware>();
        app.UseMiddleware<AuthExceptionMiddleware>();
        app.UseMiddleware<ExceptionMiddleware>();
        
        // Request localization should be before routing/auth
        app.UseRequestLocalization(new RequestLocalizationOptions
        {
            DefaultRequestCulture = new RequestCulture("en-GB"),
            SupportedCultures = new[] { new CultureInfo("en-GB") },
            SupportedUICultures = new[] { new CultureInfo("en-GB") }
        });
        
        // CORS must be before Authentication and Authorization
        app.UseCors("DefaultCorsPolicy");
        
        app.UseAuthentication();
        app.UseAuthorization();

        // Swagger UI (development only)
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        
        /* Route Mapping Start*/
        app.MapLoginEndpoints();
        app.MapCustomEndpoints();
        app.MapMessageEndpoints();
        app.MapProfileEndpoints();
        app.MapTransferProfileEndpoints();
        /* Route Mapping End*/
        
        app.MapGet("/secure", () => "Hello from protected API")
            .RequireAuthorization();
        
        Log.Information("API starting up. Listening on:");
        foreach (var url in app.Urls)
        {
            Log.Information($"  {url}");
        }

        app.Run();
    }
    
    // Helper method to extract server info from connection string for logging
    private static string ExtractServerFromConnectionString(string connectionString)
    {
        try
        {
            var parts = connectionString.Split(';');
            var serverPart = parts.FirstOrDefault(p => p.StartsWith("Server="));
            return serverPart ?? "unknown";
        }
        catch
        {
            return "unknown";
        }
    }
}
