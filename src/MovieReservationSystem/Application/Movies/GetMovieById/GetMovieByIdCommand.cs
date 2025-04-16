using MediatR;
using MovieReservationSystem.Application.Movies.GetMovies;
using MovieReservationSystem.Domain.Entities;

namespace MovieReservationSystem.Application.Movies.GetMovieById;

public record GetMovieByIdCommand(Guid Id) : IRequest<Movie>
{
}