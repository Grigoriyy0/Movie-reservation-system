using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieReservationSystem.Domain.Entities;

namespace MovieReservationSystem.Infrastructure.Configurations;

public class MovieEntityTypeConfiguration : IEntityTypeConfiguration<Movie>
{
    public void Configure(EntityTypeBuilder<Movie> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Name).IsRequired();
        
        builder.Property(x => x.Description).IsRequired();
        
        builder.Property(x => x.ImageUrl).IsRequired();
        
        builder.Property(x => x.Genre).IsRequired();
        
        builder.Property(x => x.AvailableSeats).IsRequired();
        
        builder.Property(x => x.NumberOfSeats).IsRequired();
        
        builder.Property(x => x.TicketPrice).IsRequired();
        
        builder.Property(x => x.ShowTime).IsRequired();
        
        builder.ToTable("Movies");
    }
}