using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieReservationSystem.Infrastructure.Contexts;

namespace MovieReservationSystem.Application.Movies.DeleteMovie;

public class DeleteMovieCommandHandler : IRequestHandler<DeleteMovieCommand>
{
    private readonly MainContext _context;

    public DeleteMovieCommandHandler(MainContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteMovieCommand request, CancellationToken cancellationToken)
    {
        var movie = await _context.Movies.FirstOrDefaultAsync(x => x.Id == request.MovieId, cancellationToken);

        if (movie == null)
        {
            throw new ArgumentNullException(nameof(movie), "Movie not found");
        }
        
        _context.Movies.Remove(movie);
        await _context.SaveChangesAsync(cancellationToken);
    }
}