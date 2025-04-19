using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieReservationSystem.Domain.Entities;
using MovieReservationSystem.Infrastructure.Contexts;

namespace MovieReservationSystem.Application.Bookings.CancelBooking;

public class CancelBookingCommandHandler : IRequestHandler<CancelBookingCommand, Unit>
{
    private readonly MainContext _context;

    public CancelBookingCommandHandler(MainContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(CancelBookingCommand request, CancellationToken cancellationToken)
    {
        var booking = await _context.Bookings.FirstOrDefaultAsync(x => x.BookingId == request.BookingId, cancellationToken);

        if (booking == null)
        {
            throw new ArgumentNullException($"Booking with booking id {request.BookingId} does not exist");
        }
        
        var movie = await _context.Movies.FirstOrDefaultAsync(x => x.Id == booking.MovieId, cancellationToken);

        movie.AvailableSeats++;

        booking.Status = BookingStatus.Cancelled;
        
        await _context.SaveChangesAsync(cancellationToken);
        
        
        return Unit.Value;
    }
}