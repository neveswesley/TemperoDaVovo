using Microsoft.EntityFrameworkCore;
using TemperoDaVovo.Domain.Entities;
using TemperoDaVovo.Infrastructure.Configuration;

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
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<OrderItemSideDish> OrderItemSideDishes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        modelBuilder.Entity<ProductSideDishGroup>()
            .HasKey(x => new { x.ProductId, x.SideDishGroupId });
        
        modelBuilder.ApplyConfiguration(new OrderItemConfiguration());
        modelBuilder.ApplyConfiguration(new OrderItemSideDishConfiguration());
        modelBuilder.ApplyConfiguration(new RestaurantConfiguration());
        
        base.OnModelCreating(modelBuilder);
        
        
    }
}