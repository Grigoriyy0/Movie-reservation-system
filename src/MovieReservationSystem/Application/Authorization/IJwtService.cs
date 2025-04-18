using MovieReservationSystem.Domain.Entities;

namespace MovieReservationSystem.Application.Authorization;

public interface IJwtService
{
    public string GenerateToken(string userName, IEnumerable<Role> roles, Guid userId);
}