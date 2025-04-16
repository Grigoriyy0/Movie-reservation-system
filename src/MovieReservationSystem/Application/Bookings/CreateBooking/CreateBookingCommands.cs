using MediatR;

namespace MovieReservationSystem.Application.Bookings.CreateBooking;

public record CreateBookingCommand(Guid UserId, Guid MovieId, int SeatNumber) : IRequest<Guid>
{
}