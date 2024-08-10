using Microsoft.EntityFrameworkCore;
using SkyVault.WebApi.Backend.Models;

namespace SkyVault.WebApi.Helper;

public static class DbContextExtension
{
    public static SkyvaultContext CreateDbContext(this DbContext context, string connectionString)
    {
        var builder = new DbContextOptionsBuilder<SkyvaultContext>();
        builder.UseMySql(connectionString, 
            new MySqlServerVersion(new Version(8, 0))); // Or your preferred database provider
        return new SkyvaultContext(builder.Options);
    }
}