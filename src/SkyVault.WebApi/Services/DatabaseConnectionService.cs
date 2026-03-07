using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace SkyVault.WebApi.Services;

public class DatabaseConnectionService : IDatabaseConnectionService
{
    private static readonly string MySqlPortDefault = "3306";
    private static readonly string MySqlHostDefault = "mysql";
    
    private readonly IConfiguration _configuration;
    private readonly IWebHostEnvironment _environment;
    private readonly bool _useManagedIdentity;

    public DatabaseConnectionService(IConfiguration configuration, IWebHostEnvironment environment)
    {
        _configuration = configuration;
        _environment = environment;
        _useManagedIdentity = CalculateUseManagedIdentity();
    }
    
    private bool CalculateUseManagedIdentity() => 
        _environment.IsProduction() || 
        !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("AZUREAD__CLIENTID")) ||
        !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("KEYVAULT_URI"));

    public bool UseManagedIdentity => _useManagedIdentity;

    public DatabaseProvider GetProvider() => DatabaseProvider.MySql;

    public string GetConnectionString()
    {
        var connectionConfig = _useManagedIdentity 
            ? GetProductionConnectionConfig() 
            : GetDevelopmentConnectionConfig();
            
        return BuildConnectionString(connectionConfig);
    }
    
    private ConnectionConfig GetProductionConnectionConfig()
    {
        return new ConnectionConfig
        {
            Host = GetRequiredEnvironmentVariable("MYSQL_HOST", "production"),
            Port = Environment.GetEnvironmentVariable("MYSQL_PORT") ?? MySqlPortDefault,
            Database = GetRequiredEnvironmentVariable("MYSQL_DATABASE", "production"),
            User = GetRequiredConfigurationValue("MYSQL_USER", "Azure Key Vault"),
            Password = GetRequiredConfigurationValue("MYSQL_PASSWORD", "Azure Key Vault")
        };
    }
    
    private ConnectionConfig GetDevelopmentConnectionConfig()
    {
        return new ConnectionConfig
        {
            Host = Environment.GetEnvironmentVariable("MYSQL_HOST") ?? MySqlHostDefault,
            Port = Environment.GetEnvironmentVariable("MYSQL_PORT") ?? MySqlPortDefault,
            Database = GetRequiredEnvironmentVariable("MYSQL_DATABASE"),
            User = GetRequiredEnvironmentVariable("MYSQL_USER"),
            Password = GetRequiredEnvironmentVariable("MYSQL_PASSWORD")
        };
    }
    
    private static string BuildConnectionString(ConnectionConfig config) => 
        $"Server={config.Host};Port={config.Port};Database={config.Database};User={config.User};Password={config.Password};";
    
    private string GetRequiredEnvironmentVariable(string variableName, string? context = null)
    {
        var value = Environment.GetEnvironmentVariable(variableName);
        var contextMsg = string.IsNullOrEmpty(context) ? "" : $" for {context}";
        return value ?? throw new InvalidOperationException($"{variableName} environment variable not set{contextMsg}.");
    }
    
    private string GetRequiredConfigurationValue(string key, string source)
    {
        return _configuration[key] ?? throw new InvalidOperationException($"{key} secret not found in {source}.");
    }
    
    private class ConnectionConfig
    {
        public string Host { get; init; } = string.Empty;
        public string Port { get; init; } = string.Empty;
        public string Database { get; init; } = string.Empty;
        public string User { get; init; } = string.Empty;
        public string Password { get; init; } = string.Empty;
    }
}
