using MediatR;

namespace MovieReservationSystem.Application.Bookings.CancelBooking;

public record CancelBookingCommand(Guid BookingId) : IRequest<Unit>
{
    
}