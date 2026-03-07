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
using SkyVault.WebApi.Services;
using System.Globalization;
using Azure.Identity;

namespace SkyVault.WebApi;

public static class Program
{
    public static void Main(string[] args)
    {
        // Load .env file variables into Environment variables
        Env.Load();

        var builder = WebApplication.CreateBuilder(args);

        // Clear default configuration sources and add only environment variables
        builder.Configuration.Sources.Clear();
        builder.Configuration.AddEnvironmentVariables();
        
        // Add Azure Key Vault for production
        if (!builder.Environment.IsDevelopment())
        {
            var keyVaultUri = Environment.GetEnvironmentVariable("KEYVAULT_URI");
            if (!string.IsNullOrEmpty(keyVaultUri))
            {
                builder.Configuration.AddAzureKeyVault(
                    new Uri(keyVaultUri),
                    new DefaultAzureCredential());
            }
        }

        // Configure Serilog using environment variables
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .CreateLogger();

        builder.Host.UseSerilog();

        var env = builder.Environment.EnvironmentName;
        Log.Information($"{env} : API is starting up");

        // Dump Entra ID configuration from environment variables
        Log.Information("=== Entra ID Configuration ===");
        Log.Information($"Instance: {Environment.GetEnvironmentVariable("AZUREAD__INSTANCE")}");
        Log.Information($"TenantId: {Environment.GetEnvironmentVariable("AZUREAD__TENANTID")}");
        Log.Information($"ClientId: {Environment.GetEnvironmentVariable("AZUREAD__CLIENTID")}");
        Log.Information($"Audience: {Environment.GetEnvironmentVariable("AZUREAD__AUDIENCE")}");
        Log.Information("================================");

        var isDevOrLocal = builder.Environment.IsDevelopment() || env == "Local";

        // Register database connection service
        builder.Services.AddSingleton<IDatabaseConnectionService, DatabaseConnectionService>();

        // Database Context
        builder.Services.AddDbContext<SkyvaultContext>((serviceProvider, options) =>
        {
            var dbConnectionService = serviceProvider.GetRequiredService<IDatabaseConnectionService>();
            var connectionString = dbConnectionService.GetConnectionString();
            
            // Always use MySQL (both for local development and Azure Database for MySQL)
            options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0)));
            
            if (dbConnectionService.UseManagedIdentity)
            {
                Log.Information("Using Azure Database for MySQL with credentials from Azure Key Vault");
            }
            else
            {
                Log.Information($"Using MySQL connection: Server={Environment.GetEnvironmentVariable("MYSQL_HOST")}, Database={Environment.GetEnvironmentVariable("MYSQL_DATABASE")}");
            }
        });

        builder.Services.AddHealthChecks()
            .AddDbContextCheck<SkyvaultContext>(name: "Database");
        
        builder.Services.AddAutoMapper(typeof(Program).Assembly, typeof(MappingProfile).Assembly);
        
        // Azure AD Authentication
        // Configure Azure AD from environment variables
        var azureAdConfig = new Dictionary<string, string?>
        {
            ["Instance"] = Environment.GetEnvironmentVariable("AZUREAD__INSTANCE") ?? "",
            ["TenantId"] = Environment.GetEnvironmentVariable("AZUREAD__TENANTID") ?? "",
            ["ClientId"] = Environment.GetEnvironmentVariable("AZUREAD__CLIENTID") ?? "",
            ["Audience"] = Environment.GetEnvironmentVariable("AZUREAD__AUDIENCE") ?? ""
        };
        
        // Log Azure AD configuration values
        Log.Information("=== Azure AD Configuration ===");
        foreach (var config in azureAdConfig)
        {
            var displayValue = string.IsNullOrEmpty(config.Value) ? "NULL" : config.Value;
            Log.Information($"{config.Key}: {displayValue}");
        }
        Log.Information("==============================");
        
        builder.Configuration.AddInMemoryCollection(azureAdConfig);
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAD"));

        builder.Logging.AddFilter("Microsoft.AspNetCore.Authentication", LogLevel.Debug);
        // Configure TokenValidationParameters
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
            var baseUrl = Environment.GetEnvironmentVariable("AZURE_FUNCTION_BASE_URL");
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
        app.MapHealthChecks("/health");
        app.MapLoginEndpoints();
        app.MapCustomEndpoints();
        app.MapMessageEndpoints();
        app.MapProfileEndpoints();
        app.MapTransferProfileEndpoints();
        /* Route Mapping End*/
        
        app.MapGet("/secure", () => "Hello from protected API")
            .RequireAuthorization();
        
        // Debug endpoint to test JWT decoding without authentication
        app.MapPost("/debug-token", (HttpContext context) =>
        {
            var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();
            var token = authHeader?.Split(" ").Last();
            
            if (string.IsNullOrEmpty(token))
            {
                return Results.BadRequest(new { error = "No token provided" });
            }
            
            try
            {
                // Decode token without validation (for debugging only)
                var handler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(token);
                
                var tokenInfo = new
                {
                    Header = new
                    {
                        Algorithm = jwtToken.Header.Alg,
                        Type = jwtToken.Header.Typ,
                        Kid = jwtToken.Header.Kid
                    },
                    Payload = new
                    {
                        Issuer = jwtToken.Issuer,
                        Audience = jwtToken.Audiences,
                        Subject = jwtToken.Subject,
                        IssuedAt = jwtToken.IssuedAt,
                        Expires = jwtToken.ValidTo,
                        NotBefore = jwtToken.ValidFrom,
                        Claims = jwtToken.Claims.ToDictionary(c => c.Type, c => c.Value)
                    }
                };
                
                return Results.Ok(tokenInfo);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { error = "Failed to decode token", message = ex.Message });
            }
        });
        
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
