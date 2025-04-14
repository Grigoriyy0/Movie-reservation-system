using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieReservationSystem.Domain.Entities;

namespace MovieReservationSystem.Infrastructure.Configurations;

public class UserRoleEntityTypeConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.HasKey(x => new { x.UserId, x.RoleId });
        
        builder.HasOne(x => x.User)
            .WithMany(y => y.UserRoles)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(x => x.Role)
            .WithMany(y => y.UserRoles)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.ToTable("UserRoles");
    }
}