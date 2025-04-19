using MovieReservationSystem.Infrastructure.Services.Abstract;

namespace MovieReservationSystem.Infrastructure.Services;

public class PhotoManager(PhysicalDirectoryResolver resolver) : IPhotoManager
{
    public async Task<string> UploadPhotoAsync(IFormFile image)
    {
        Directory.CreateDirectory(resolver.PathToPicturesDir);
        
        var imageName = $"{Guid.NewGuid()}{Path.GetExtension(image.FileName)}";
        
        var filePath = Path.Combine(resolver.PathToPicturesDir, imageName);
        
        await using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await image.CopyToAsync(stream);
        }
        
        return filePath;
    }
    
}