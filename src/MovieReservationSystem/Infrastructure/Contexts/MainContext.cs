using Microsoft.EntityFrameworkCore;
using MovieReservationSystem.Domain.Entities;

namespace MovieReservationSystem.Infrastructure.Contexts;

public class MainContext : DbContext
{
    public MainContext(DbContextOptions<MainContext> options) : base(options){ }
    
    public DbSet<User> Users { get; set; }
    
    public DbSet<Movie> Movies { get; set; }
    
    public DbSet<Booking> Bookings { get; set; }
    
    public DbSet<Role> Roles { get; set; }
    
    public DbSet<UserRole> UserRoles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MainContext).Assembly);
    }
}