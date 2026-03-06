using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace SkyVault.WebApi.Services;

public class DatabaseConnectionService : IDatabaseConnectionService
{
    private readonly IConfiguration _configuration;
    private readonly IWebHostEnvironment _environment;

    public DatabaseConnectionService(IConfiguration configuration, IWebHostEnvironment environment)
    {
        _configuration = configuration;
        _environment = environment;
    }

    public bool UseManagedIdentity => _environment.IsProduction() || 
                                   !string.IsNullOrEmpty(_configuration["AZURE_CLIENT_ID"]) ||
                                   !string.IsNullOrEmpty(_configuration["AZURE_DB_SERVER"]);

    public DatabaseProvider GetProvider()
    {
        if (UseManagedIdentity)
        {
            return DatabaseProvider.SqlServer;
        }
        return DatabaseProvider.MySql;
    }

    public string GetConnectionString()
    {
        if (UseManagedIdentity)
        {
            // For Azure Database with Managed Identity
            var server = _configuration["AZURE_DB_SERVER"] ?? throw new InvalidOperationException("Azure DB server not configured. Set AZURE_DB_SERVER environment variable.");
            var database = _configuration["AZURE_DB_DATABASE"] ?? throw new InvalidOperationException("Azure DB name not configured. Set AZURE_DB_DATABASE environment variable.");
            
            return $"Server={server};Database={database};Authentication=Active Directory Default;";
        }
        else
        {
            // Local development with .env file
            var mysqlHost = Environment.GetEnvironmentVariable("MYSQL_HOST") ?? "mysql";
            var mysqlPort = Environment.GetEnvironmentVariable("MYSQL_PORT") ?? "3306";
            var mysqlDatabase = Environment.GetEnvironmentVariable("MYSQL_DATABASE")
                                   ?? throw new InvalidOperationException("No database name configured. Set MYSQL_DATABASE environment variable.");
            var mysqlUser = Environment.GetEnvironmentVariable("MYSQL_USER")
                                   ?? throw new InvalidOperationException("No database user configured. Set MYSQL_USER environment variable.");
            var mysqlPassword = Environment.GetEnvironmentVariable("MYSQL_PASSWORD")
                                   ?? throw new InvalidOperationException("No database password configured. Set MYSQL_PASSWORD environment variable.");
            
            return $"Server={mysqlHost};Port={mysqlPort};Database={mysqlDatabase};User={mysqlUser};Password={mysqlPassword};";
        }
    }
}
