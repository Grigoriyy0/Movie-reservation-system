using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieReservationSystem.Domain.Entities;
using MovieReservationSystem.Infrastructure.Contexts;

namespace MovieReservationSystem.Application.Movies.GetMovies;

public class GetMoviesCommandHandler : IRequestHandler<GetMoviesCommand, List<Movie>>
{
    private readonly MainContext _context;

    public GetMoviesCommandHandler(MainContext context)
    {
        _context = context;
    }

    public async Task<List<Movie>> Handle(GetMoviesCommand request, CancellationToken cancellationToken)
    {
        var movies = await _context.Movies.ToListAsync(cancellationToken);
        
        return movies;
    }
}