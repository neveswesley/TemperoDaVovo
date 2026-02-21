using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TemperoDaVovo.Domain.Entities;

namespace TemperoDaVovo.Infrastructure.Configuration;

public class OrderItemSideDishConfiguration : IEntityTypeConfiguration<OrderItemSideDish>
{
    public void Configure(EntityTypeBuilder<OrderItemSideDish> builder)
    {
        builder.ToTable("OrderItemSideDishes");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).IsRequired().HasMaxLength(150);
        builder.Property(x => x.UnitPrice).HasPrecision(18, 2).IsRequired();
        builder.Property(x => x.Quantity).IsRequired();
        builder.Property(x => x.TotalPrice).HasPrecision(18, 2).IsRequired();
        builder.Property(x => x.OriginalSideDishId).IsRequired(false);
        builder.Property(x => x.OrderItemId).IsRequired();

        builder.HasIndex(x => new { x.OrderItemId, x.OriginalSideDishId }).IsUnique(false);
        
    }
}