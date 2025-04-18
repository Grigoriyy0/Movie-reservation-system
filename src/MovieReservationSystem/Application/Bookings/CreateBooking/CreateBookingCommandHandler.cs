using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieReservationSystem.Domain.Entities;
using MovieReservationSystem.Infrastructure.Contexts;

namespace MovieReservationSystem.Application.Bookings.CreateBooking;

public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, Guid>
{
    private readonly MainContext _context;

    public CreateBookingCommandHandler(MainContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
    {
        var movie = await _context.Movies.FirstOrDefaultAsync(x => x.Id == request.MovieId, cancellationToken);

        if (movie == null)
        {
            throw new Exception($"Movie with id {request.MovieId} does not exist.");
        }

        if (request.SeatNumber > movie.NumberOfSeats)
        {
            throw new Exception($"Seat number {request.SeatNumber} is out of range.");
        }
        
        var booking = await _context.Bookings.FirstOrDefaultAsync(x => x.SeatNumber == request.SeatNumber, cancellationToken);
        
        if (booking != null)
        {
            throw new Exception("Seat is not available.");
        }
        
        var newBooking = new Booking
        {
            BookingId = Guid.NewGuid(),
            UserId = request.UserId,
            SeatNumber = request.SeatNumber,
            MovieId = request.MovieId,
            Status = BookingStatus.Confirmed
        };

        if (movie.AvailableSeats > 1)
        {
            movie.AvailableSeats--;
        }
        
        await _context.Bookings.AddAsync(newBooking, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        
        return newBooking.BookingId;
    }
}