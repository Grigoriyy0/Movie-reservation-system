using MediatR;

namespace MovieReservationSystem.Application.Movies.DeleteMovie;

public record DeleteMovieCommand(Guid MovieId) : IRequest;