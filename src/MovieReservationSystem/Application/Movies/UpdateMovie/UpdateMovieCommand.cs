using MediatR;

namespace MovieReservationSystem.Application.Movies.UpdateMovie;

public record UpdateMovieCommand(Guid MovieId, string Title, string Description, int TicketPrice, int NumberOfSeats, string Category, DateTime ShowTime, string ImageUrl) : IRequest<Unit>;