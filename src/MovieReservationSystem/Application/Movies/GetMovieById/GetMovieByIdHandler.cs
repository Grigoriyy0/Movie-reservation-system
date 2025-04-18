using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieReservationSystem.Domain.Entities;
using MovieReservationSystem.Infrastructure.Contexts;

namespace MovieReservationSystem.Application.Movies.GetMovieById;

public class GetMovieByIdHandler : IRequestHandler<GetMovieByIdCommand, Movie>
{
    private readonly MainContext _context;

    public GetMovieByIdHandler(MainContext context)
    {
        _context = context;
    }

    public async Task<Movie> Handle(GetMovieByIdCommand request, CancellationToken cancellationToken)
    {
        var movie = await _context.Movies.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        
        if(movie is null)
        {
           throw new ArgumentException($"No movie found with id {request.Id}", nameof(request));
        }
        
        return movie;
    }
}