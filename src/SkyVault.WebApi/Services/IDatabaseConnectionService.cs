namespace SkyVault.WebApi.Services;

public interface IDatabaseConnectionService
{
    string GetConnectionString();
    bool UseManagedIdentity { get; }
    DatabaseProvider GetProvider();
}

public enum DatabaseProvider
{
    MySql,
    SqlServer
}
