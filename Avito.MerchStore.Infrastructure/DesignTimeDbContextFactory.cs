using Avito.MerchStore.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Avito.MerchStore.Infrastructure;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<MerchDbContext>
{
    public MerchDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), ".."))
            .AddJsonFile("Avito.MerchStore.API/appsettings.json")
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<MerchDbContext>();
        optionsBuilder.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
        
        return new MerchDbContext(optionsBuilder.Options);
    }
}