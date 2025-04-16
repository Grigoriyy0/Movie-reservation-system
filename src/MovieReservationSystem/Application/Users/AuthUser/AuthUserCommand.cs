using MediatR;
using MovieReservationSystem.Application.Shared;

namespace MovieReservationSystem.Application.Users.AuthUser;

public record AuthUserCommand(AuthUserDto Dto) : IRequest<Result<AuthResponse>>;