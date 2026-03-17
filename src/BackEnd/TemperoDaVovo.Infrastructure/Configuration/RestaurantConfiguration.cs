using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TemperoDaVovo.Domain.Entities;

namespace TemperoDaVovo.Infrastructure.Configuration;

public class RestaurantConfiguration : IEntityTypeConfiguration<Restaurant>
{
    public void Configure(EntityTypeBuilder<Restaurant> builder)
    {
        builder.ToTable("Restaurant");
        builder.HasKey(r => r.Id);
        builder.Property(r => r.Id).ValueGeneratedOnAdd();

        builder.Property(r => r.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(r => r.Phone)
            .IsRequired()
            .HasMaxLength(50);

        builder.OwnsOne(r => r.Address, address =>
        {
            address.Property(a => a.ZipCode)
                .IsRequired()
                .HasMaxLength(8)
                .HasColumnName("Cep");

            address.Property(a => a.State)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("State");

            address.Property(a => a.City)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("City");

            address.Property(a => a.Neighborhood)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("Neighborhood");

            address.Property(a => a.Street)
                .IsRequired()
                .HasMaxLength(150)
                .HasColumnName("Street");

            address.Property(a => a.Number)
                .IsRequired()
                .HasMaxLength(20)
                .HasColumnName("Number");

            address.Property(a => a.Complement)
                .HasMaxLength(150)
                .HasColumnName("Complement");
        });
    }
}