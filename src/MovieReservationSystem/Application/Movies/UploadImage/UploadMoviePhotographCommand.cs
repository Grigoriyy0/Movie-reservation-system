using MediatR;

namespace MovieReservationSystem.Application.Movies;

public record UploadMoviePhotographCommand(Guid MovieId, IFormFile Image) : IRequest<string>
{ }