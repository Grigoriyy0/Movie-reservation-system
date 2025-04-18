using MovieReservationSystem.Domain.Entities;

namespace MovieReservationSystem.Application.Users.AuthUser;

public class AuthResponse(Guid userId, IEnumerable<Role> roles)
{
    public Guid UserId { get; } = userId;
    
    public IEnumerable<Role> Roles { get; } = roles;
}