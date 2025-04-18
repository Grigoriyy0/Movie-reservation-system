using MediatR;

namespace MovieReservationSystem.Application.Movies.CreateMovie;

public record CreateMovieCommand(string Name, string Description, string Category, int TicketPrice, int NumberOfSeats, string ImageUrl, DateTime ShowTime) : IRequest<Unit>;