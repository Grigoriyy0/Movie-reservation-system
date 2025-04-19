using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieReservationSystem.Infrastructure.Contexts;

namespace MovieReservationSystem.Application.Movies.GetTotalRevenue;

public class GetTotalRevenueCommandHandler : IRequestHandler<GetTotalRevenueCommand, decimal>
{
    private readonly MainContext _context;

    public GetTotalRevenueCommandHandler(MainContext context)
    {
        _context = context;
    }

    public async Task<decimal> Handle(GetTotalRevenueCommand request, CancellationToken cancellationToken)
    {
        var movie = await _context.Movies.FirstOrDefaultAsync(x => x.Id == request.MovieId, cancellationToken);

        if (movie == null)
        {
            throw new ArgumentNullException("Movie not found");
        }
        
        var totalRevenue = movie.TicketPrice * (movie.NumberOfSeats - movie.AvailableSeats);
        
        
        return totalRevenue;
    }
}