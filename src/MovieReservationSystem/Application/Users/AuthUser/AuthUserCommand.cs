using MediatR;
using MovieReservationSystem.Application.Shared;

namespace MovieReservationSystem.Application.Users.AuthUser;

public record AuthUserCommand(string Email, string Password) : IRequest<Result<AuthResponse>>;