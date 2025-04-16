using MediatR;

namespace MovieReservationSystem.Application.Movies.CreateMovie;

public record CreateMovieCommand(CreateMovieDto Dto) : IRequest;