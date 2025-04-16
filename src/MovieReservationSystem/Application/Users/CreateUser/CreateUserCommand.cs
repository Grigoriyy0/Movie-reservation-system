using MediatR;

namespace MovieReservationSystem.Application.Users.CreateUser;

public record CreateUserCommand(CreateUserDto Dto) : IRequest;