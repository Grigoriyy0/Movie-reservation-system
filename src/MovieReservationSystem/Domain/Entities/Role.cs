namespace MovieReservationSystem.Domain.Entities;

public class Role
{
    public Guid Id { get; set; }
    
    public string RoleName { get; set; }

    public ICollection<UserRole> UserRoles { get; set; } = [];
}