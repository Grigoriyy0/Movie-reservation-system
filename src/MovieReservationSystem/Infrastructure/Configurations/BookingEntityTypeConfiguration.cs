using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieReservationSystem.Domain.Entities;

namespace MovieReservationSystem.Infrastructure.Configurations;

public class BookingEntityTypeConfiguration : IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        builder.HasKey(x => new {x.UserId, x.MovieId});
        
        builder.HasOne(x => x.Movie)
            .WithMany(y => y.Bookings)
            .OnDelete(DeleteBehavior.SetNull);
        
        builder.HasOne(x => x.User)
            .WithMany(y => y.Bookings)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.ToTable("Bookings");
    }
}