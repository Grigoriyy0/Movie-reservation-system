using MediatR;

namespace MovieReservationSystem.Application.Movies.GetTotalRevenue;

public record GetTotalRevenueCommand(Guid MovieId) : IRequest<decimal>
{
    
}