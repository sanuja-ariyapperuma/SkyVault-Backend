using Microsoft.EntityFrameworkCore;
using SkyVault.WebApi.Backend.Models;

namespace SkyVault.WebApi.Helper;

internal static class DbContextExtension
{
    public static SkyvaultContext CreateDbContext(this DbContext context)
    {
        var builder = new DbContextOptionsBuilder<SkyvaultContext>();
        builder.UseMySql("Server=ayubdevmysql.southeastasia.cloudapp.azure.com;Database=skyvault;Uid=skyapp;Pwd=2NMc5*3Ee%Kxa^K3;", 
            new MySqlServerVersion(new Version(8, 0))); // Or your preferred database provider
        return new SkyvaultContext(builder.Options);
    }
}