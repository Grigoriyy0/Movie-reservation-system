using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieReservationSystem.Domain.Constants;
using MovieReservationSystem.Domain.Entities;
using MovieReservationSystem.Infrastructure.Contexts;
using MovieReservationSystem.Infrastructure.Services.Abstract;

namespace MovieReservationSystem.Application.Users.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand>
{
    private readonly MainContext _context;
    private readonly IHashProvider _hashProvider;
    private readonly RolesIdsConstants _roleId = new();

    public CreateUserCommandHandler(MainContext context, IHashProvider hashProvider)
    {
        _context = context;
        _hashProvider = hashProvider;
    }

    public async Task Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        if (request.Dto.Password != request.Dto.PasswordConfirm)
        {
            throw new Exception("Passwords do not match");
        }
        
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == request.Dto.Email, cancellationToken);

        if (user != null)
        {
            throw new Exception("Email already exists");
        }
        
        var passwordHash = _hashProvider.Hash(request.Dto.Password);

        var newUser = new User
        {
            Id = Guid.NewGuid(),
            Email = request.Dto.Email,
            Name = request.Dto.FirstName,
            Surname = request.Dto.LastName,
            PasswordHash = passwordHash,
            UserRoles = [
                new UserRole
                {
                    RoleId = _roleId.UserRoleIdConstant
                }
            ]
            
        
        };
        
        await _context.Users.AddAsync(newUser, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}