using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TemperoDaVovo.Infrastructure.DataAccess;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

        optionsBuilder.UseNpgsql(
            "Host=dpg-d7a42tnkijhs73dgtnlg-a.oregon-postgres.render.com;Port=5432;Database=temperodavovo_db_96b7;Username=temperodavovo_db_96b7_user;Password=8ri8hhClzgYbZn1JRXIhBeFPuGMmyylZ;SSL Mode=Require;Trust Server Certificate=true"
        );

        return new AppDbContext(optionsBuilder.Options);
    }
}