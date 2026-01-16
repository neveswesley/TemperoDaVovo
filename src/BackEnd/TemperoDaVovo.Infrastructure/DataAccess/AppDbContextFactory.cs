using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TemperoDaVovo.Infrastructure.DataAccess;

public class AppDbContextFactory
    : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

        optionsBuilder.UseSqlite(
            "Data Source=../TemperoDaVovo.Api/Data/restaurant.db"
        );

        return new AppDbContext(optionsBuilder.Options);
    }
}