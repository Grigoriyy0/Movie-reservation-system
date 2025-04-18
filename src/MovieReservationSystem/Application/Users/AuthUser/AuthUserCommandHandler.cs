using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieReservationSystem.Application.Shared;
using MovieReservationSystem.Infrastructure.Contexts;
using MovieReservationSystem.Infrastructure.Services.Abstract;

namespace MovieReservationSystem.Application.Users.AuthUser;

public class AuthUserCommandHandler : IRequestHandler<AuthUserCommand, Result<AuthResponse>>
{
    private readonly MainContext _context;
    private readonly IHashProvider _hashProvider;

    public AuthUserCommandHandler(MainContext context, IHashProvider hashProvider)
    {
        _context = context;
        _hashProvider = hashProvider;
    }

    public async Task<Result<AuthResponse>> Handle(AuthUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == request.Email, cancellationToken);

        if (user == null)
        {
            return Result<AuthResponse>.AsFailure("User with this email address does not exist.");
        }

        var dtoPasswordHash = _hashProvider.Hash(request.Password);

        if (dtoPasswordHash != user.PasswordHash)
        {
            return Result<AuthResponse>.AsFailure("Password or email is incorrect");
        }

        var roles = await _context.UserRoles
            .Where(ur => ur.UserId == user.Id)
            .Select(ur => ur.Role)
            .ToListAsync(cancellationToken);

        var response = new AuthResponse(user.Id, roles);
        
        return Result<AuthResponse>.AsSuccess(response);
    }
}