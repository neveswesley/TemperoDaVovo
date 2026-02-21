using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TemperoDaVovo.Domain.Entities;

namespace TemperoDaVovo.Infrastructure.Configuration;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.ToTable("OrderItems");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.ProductName).IsRequired().HasMaxLength(150);
        builder.Property(x => x.UnitPrice).HasPrecision(18, 2);
        builder.Property(x => x.TotalPrice).HasPrecision(18, 2);

        builder.HasMany(x => x.SideDishes)
            .WithOne()
            .HasForeignKey(sd => sd.OrderItemId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}