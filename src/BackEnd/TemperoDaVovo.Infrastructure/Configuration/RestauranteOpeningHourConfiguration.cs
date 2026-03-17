using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TemperoDaVovo.Domain.Entities;

namespace TemperoDaVovo.Infrastructure.Configuration;

public class RestaurantOpeningHourConfiguration : IEntityTypeConfiguration<RestaurantOpeningHour>
{
    public void Configure(EntityTypeBuilder<RestaurantOpeningHour> builder)
    {
        builder.ToTable("RestaurantOpeningHours");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.DayOfWeek)
            .IsRequired();

        builder.Property(x => x.OpenTime)
            .IsRequired().IsConcurrencyToken();

        builder.Property(x => x.CloseTime)
            .IsRequired().IsConcurrencyToken();

        builder.HasOne(x => x.Restaurant)
            .WithMany(r => r.OpeningHours)
            .HasForeignKey(x => x.RestaurantId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}