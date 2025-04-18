namespace MovieReservationSystem.Infrastructure.Services.Abstract;

public interface IPhotoManager
{
    public Task<string> UploadPhotoAsync(IFormFile image);
}