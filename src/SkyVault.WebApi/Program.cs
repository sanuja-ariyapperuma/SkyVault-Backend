
using DotNetEnv;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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

namespace SkyVault.WebApi;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddHttpContextAccessor();

        Env.Load();

        var env = builder.Environment.EnvironmentName;
        var isDevOrLocal = env == "Development" || env == "Local";


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
            options.AddPolicy("DefaultCorsPolicy", corsBuilder =>
            {
                if (isDevOrLocal)
                {
                    corsBuilder.WithOrigins("http://localhost:5173")
                               .AllowAnyHeader()
                               .AllowAnyMethod()
                               .AllowCredentials();
                }
                else
                {
                    corsBuilder.WithOrigins("https://travelchannelcrm.com", "https://www.travelchannelcrm.com")
                               .AllowAnyHeader()
                               .AllowAnyMethod()
                               .AllowCredentials();
                }
            });
        });

        builder.Services.AddMemoryCache();
        builder.Services.AddScoped<CacheService>();
        builder.Services.AddHttpContextAccessor();

        builder.Services.AddHttpClient<IApiClient, ApiClient>(client =>
        {
            client.BaseAddress = new Uri(Environment.GetEnvironmentVariable("AZURE_FUNCTION_BASE_URL"));
        });

        builder.Services.AddControllers();
        builder.Services.AddHttpClient<IApiClient, ApiClient>();
        builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .CreateLogger();

        Log.Information($"{env} : API is starting up");

        builder.Host.UseSerilog();

        var app = builder.Build();
        app.UseMiddleware<CorrelationIdMiddleware>();
        app.MapLoginEndpoints();
        app.MapCustomEndpoints();
        app.MapMessageEndpoints();
        app.MapProfileEndpoints();
        app.MapTransferProfileEndpoints();
        app.UseCors("DefaultCorsPolicy");
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseMiddleware<ExceptionMiddleware>();

        var supportedCultures = new[] { new CultureInfo("en-GB") };

        app.UseRequestLocalization(new RequestLocalizationOptions
        {
            DefaultRequestCulture = new RequestCulture("en-GB"),
            SupportedCultures = supportedCultures,
            SupportedUICultures = supportedCultures
        });

        app.Run();
    }
}
