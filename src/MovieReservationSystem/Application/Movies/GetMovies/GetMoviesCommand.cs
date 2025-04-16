using MediatR;
using MovieReservationSystem.Domain.Entities;

namespace MovieReservationSystem.Application.Movies.GetMovies;

public record GetMoviesCommand() : IRequest<List<Movie>>
{
}