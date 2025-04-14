using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieReservationSystem.Domain.Entities;

namespace MovieReservationSystem.Infrastructure.Configurations;

public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(p => p.Id);
        
        builder.Property(x => x.Name)
            .IsRequired();
        
        builder.Property(x => x.Surname)
            .IsRequired();
        
        builder.Property(x => x.Email)
            .IsRequired();
        
        builder.Property(x => x.Surname)
            .IsRequired();
        
        builder.Property(x => x.PasswordHash)
            .IsRequired();
        
        builder.ToTable("Users");
    }
}