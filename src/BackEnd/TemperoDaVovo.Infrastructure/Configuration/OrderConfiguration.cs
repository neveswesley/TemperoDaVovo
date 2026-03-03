using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TemperoDaVovo.Domain.Entities;

namespace TemperoDaVovo.Infrastructure.Configuration;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasMany(x => x.Items)
            .WithOne()
            .HasForeignKey(i => i.OrderId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder
            .HasOne(o => o.Neighborhood)
            .WithMany()
            .HasForeignKey(o => o.NeighborhoodId)
            .IsRequired(false);
        
        builder.Property(o => o.AddressName)
            .HasConversion<string>();
        
        builder
            .HasOne(o => o.Payment)
            .WithOne()
            .HasForeignKey<Order>(o => o.PaymentId)
            .IsRequired(false);
    }
}