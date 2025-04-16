using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MovieReservationSystem.Application.Authorization;
using MovieReservationSystem.Application.Extensions;
using MovieReservationSystem.Infrastructure.Contexts;
using MovieReservationSystem.Infrastructure.Services;
using MovieReservationSystem.Infrastructure.Services.Abstract;

namespace MovieReservationSystem;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<MainContext>(opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("Default")));
        builder.Services.AddSingleton<IJwtService, JwtService>();
        builder.Services.AddScoped<IHashProvider, HashProvider>();
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                var signingKeyBytes = Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Jwt:Key").Value!);

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidIssuer = "AuthServer",
                    ValidateAudience = false,
                    ValidAudience = "Client",
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(signingKeyBytes)
                };
            });
        builder.Services.AddAuthorization();

        
        builder.Services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
        });
        
        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        using (var scope = app.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<MainContext>();
            var hashProvider = scope.ServiceProvider.GetRequiredService<IHashProvider>();
            context.Seed(builder.Configuration, hashProvider);
        }

        
        app.MapControllers();

        app.Run();
    }
}