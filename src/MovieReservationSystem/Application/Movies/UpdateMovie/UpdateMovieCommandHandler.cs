using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieReservationSystem.Infrastructure.Contexts;

namespace MovieReservationSystem.Application.Movies.UpdateMovie;

public class UpdateMovieCommandHandler : IRequestHandler<UpdateMovieCommand, Unit>
{
    private readonly MainContext _context;

    public UpdateMovieCommandHandler(MainContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateMovieCommand request, CancellationToken cancellationToken)
    {
        var movie = await _context.Movies.FirstOrDefaultAsync(x => x.Id == request.MovieId, cancellationToken);

        if (movie == null)
        {
            throw new ArgumentNullException(nameof(request), "Movie not found");
        }
        
        movie.Name = request.Title;
        movie.Description = request.Description;
        movie.TicketPrice = request.TicketPrice;
        movie.Genre = request.Category;
        movie.ShowTime = request.ShowTime;
        movie.ImageUrl = request.ImageUrl;

        if (movie.AvailableSeats == movie.NumberOfSeats)
        {
            movie.AvailableSeats = request.NumberOfSeats;
            movie.NumberOfSeats = request.NumberOfSeats;
        }
        
        // 300 мест 150 занято
        // апдейт - 500 мест = 150 + 500 - 300
        // if anyone has booked a sit
        movie.NumberOfSeats = request.NumberOfSeats;
        movie.AvailableSeats = (movie.NumberOfSeats - movie.AvailableSeats) + request.NumberOfSeats - movie.NumberOfSeats;
        
        
        await _context.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}