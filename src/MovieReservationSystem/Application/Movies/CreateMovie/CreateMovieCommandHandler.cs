using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieReservationSystem.Domain.Entities;
using MovieReservationSystem.Infrastructure.Contexts;

namespace MovieReservationSystem.Application.Movies.CreateMovie;

public class CreateMovieCommandHandler : IRequestHandler<CreateMovieCommand, Unit>
{
    private readonly MainContext _context;

    public CreateMovieCommandHandler(MainContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(CreateMovieCommand request, CancellationToken cancellationToken)
    {
        var movie = await _context.Movies.FirstOrDefaultAsync(x => x.Name == request.Name, cancellationToken);

        if (movie is not null)
        {
            throw new Exception($"Movie with name '{request.Name}' is already exists.");
        }

        var newMovie = new Movie
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Description = request.Description,
            AvailableSeats = request.NumberOfSeats,
            NumberOfSeats = request.NumberOfSeats,
            Genre = request.Category,
            TicketPrice = request.TicketPrice,
            ShowTime = request.ShowTime,
            ImageUrl = request.ImageUrl,
        };
        
        await _context.Movies.AddAsync(newMovie, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}