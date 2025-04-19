using System.Reflection;
using System.Text;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MovieReservationSystem.Application.Authorization;
using MovieReservationSystem.Application.Behaviours;
using MovieReservationSystem.Application.Extensions;
using MovieReservationSystem.Application.Movies.CreateMovie;
using MovieReservationSystem.Infrastructure.Contexts;
using MovieReservationSystem.Infrastructure.Options;
using MovieReservationSystem.Infrastructure.Services;
using MovieReservationSystem.Infrastructure.Services.Abstract;
using MovieReservationSystem.Presentation.Middlewares;

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
        builder.Services.AddSwaggerGen(cfg =>
        {
            cfg.SwaggerDoc("v1", new OpenApiInfo {Title = "Movie reservation system", Version = "v1"});
            
            cfg.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Scheme = "bearer",
                Description = "Input JWT token here"
            });
            
            cfg.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    }, []
                }
                
            });
        });
        
        

        builder.Services.Configure<PicturesStorageOptions>(builder.Configuration.GetSection(nameof(PicturesStorageOptions)));
        builder.Services.AddSingleton<PhysicalDirectoryResolver>();

        builder.Services.AddTransient<IPhotoManager, PhotoManager>();
        
        builder.Services.Configure<FormOptions>(options =>
        {
            options.MultipartBodyLengthLimit = 1024 * 1024 * 10;
        });
        
        
        
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                var signingKeyBytes = Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Jwt:Key").Value!);

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = "CsharpAPI",
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
            cfg.AddOpenBehavior(typeof(ValidationBehaviors<,>));
        });
        
        builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        
        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        
        var directoryResolver = app.Services.GetRequiredService<PhysicalDirectoryResolver>();

        Directory.CreateDirectory(directoryResolver.PathToPicturesDir);

        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(directoryResolver.PathToPicturesDir),
            RequestPath = "/images"
        });

        
        app.UseMiddleware<ValidationExceptionHandlerMiddleware>();
        app.UseHttpsRedirection();
        app.UseAuthentication();
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