using MovieReservationSystem.Domain.Constants;
using MovieReservationSystem.Domain.Entities;
using MovieReservationSystem.Infrastructure.Contexts;
using MovieReservationSystem.Infrastructure.Services.Abstract;

namespace MovieReservationSystem.Application.Extensions;

public static class Seeder
{
    private static readonly RoleNamesConstants NamesConstants = new();
    private static readonly RolesIdsConstants IDs = new();

    public static void Seed(this MainContext context, IConfiguration config, IHashProvider hashProvider)
    {
        var adminPassword = config["Admin:Password"];
        var adminEmail = config["Admin:Email"];
        var adminFirstName = config["Admin:FirstName"];
        var adminLastName = config["Admin:LastName"];
        var hashedAdminPassword = hashProvider.Hash(adminPassword!);
        var roles = new List<Role>
        {  
            new()
            {
                Id = IDs.AdminRoleIdConstant, 
                RoleName = NamesConstants.AdminNameConstant
            },
            new()
            {
                Id = IDs.UserRoleIdConstant, 
                RoleName = NamesConstants.UserNameConstant
            }
        };
        
        AddRole(context, roles);
        AddUser(context, adminFirstName, hashedAdminPassword, adminLastName, adminEmail);
    }

    private static void AddRole(MainContext context, List<Role> systemRoles)
    {
        if (context.Roles.Any()) return;

        context.AddRange(systemRoles);

        context.SaveChanges();
    }

    private static void AddUser(MainContext context, string name, string hashedPassword, string lastname, string email)
    {
        if (context.Users.Any()) return;

        context.Users.Add(new User
        {
            Id = Guid.NewGuid(),
            Name = name,
            Surname = lastname,
            Email = email,
            PasswordHash = hashedPassword,
            UserRoles = [new UserRole
            {
                RoleId = IDs.AdminRoleIdConstant,
            }]
        });

        context.SaveChanges();
    }
}