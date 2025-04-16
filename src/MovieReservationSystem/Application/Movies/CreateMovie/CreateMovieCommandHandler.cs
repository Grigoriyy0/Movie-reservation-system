using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieReservationSystem.Domain.Entities;
using MovieReservationSystem.Infrastructure.Contexts;

namespace MovieReservationSystem.Application.Movies.CreateMovie;

public class CreateMovieCommandHandler : IRequestHandler<CreateMovieCommand>
{
    private readonly MainContext _context;

    public CreateMovieCommandHandler(MainContext context)
    {
        _context = context;
    }

    public async Task Handle(CreateMovieCommand request, CancellationToken cancellationToken)
    {
        var movie = await _context.Movies.FirstOrDefaultAsync(x => x.Name == request.Dto.Name, cancellationToken);

        if (movie is not null)
        {
            throw new Exception($"Movie with name '{request.Dto.Name}' is already exists.");
        }

        var newMovie = new Movie
        {
            Id = Guid.NewGuid(),
            Name = request.Dto.Name,
            Description = request.Dto.Description,
            AvailableSeats = request.Dto.NumberOfSeats,
            NumberOfSeats = request.Dto.NumberOfSeats,
            Genre = request.Dto.Category,
            TicketPrice = request.Dto.TicketPrice,
            ShowTime = request.Dto.ShowTime,
            ImageUrl = request.Dto.ImageUrl,
        };
        
        await _context.Movies.AddAsync(newMovie, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}