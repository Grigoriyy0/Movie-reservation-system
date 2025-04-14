namespace MovieReservationSystem.Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public string Surname { get; set; }
    
    public string PasswordHash { get; set; }
    
    public string Email { get; set; }

    public ICollection<UserRole> UserRoles { get; set; } = [];
    
    public ICollection<Booking> Bookings { get; set; } = [];
}