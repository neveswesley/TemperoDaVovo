using Microsoft.EntityFrameworkCore;
using TemperoDaVovo.Domain.Entities;

namespace TemperoDaVovo.Infrastructure.DataAccess;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<Restaurant> Restaurants { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<SideDish> SideDishes { get; set; }
    public DbSet<SideDishGroup> SideDishesGroups { get; set; }
    public DbSet<ProductSideDishGroup> ProductSideDishGroups { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        modelBuilder.Entity<ProductSideDishGroup>()
            .HasKey(x => new { x.ProductId, x.SideDishGroupId });
        
        base.OnModelCreating(modelBuilder);
        

    }
}