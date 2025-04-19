using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieReservationSystem.Infrastructure.Contexts;
using MovieReservationSystem.Infrastructure.Services.Abstract;

namespace MovieReservationSystem.Application.Movies.UploadImage;

public class UploadMoviePhotographCommandHandler : IRequestHandler<UploadMoviePhotographCommand, string>
{
    private readonly MainContext _context;
    private readonly IPhotoManager _photoManager;
    
    public UploadMoviePhotographCommandHandler(MainContext context, IPhotoManager photoManager)
    {
        _context = context;
        _photoManager = photoManager;
    }

    public async Task<string> Handle(UploadMoviePhotographCommand request, CancellationToken cancellationToken)
    {
        var movie = await _context.Movies.FirstOrDefaultAsync(x => x.Id == request.MovieId, cancellationToken);

        if (movie == null)
        {
            throw new Exception("Movie not found");
        }

        var imagePath = await _photoManager.UploadPhotoAsync(request.Image);
        movie.ImageUrl = imagePath;
        
        await _context.SaveChangesAsync(cancellationToken);
        
        return imagePath;
    }
}