using Microsoft.Extensions.Options;
using MovieReservationSystem.Infrastructure.Options;

namespace MovieReservationSystem.Infrastructure.Services;

public class PhysicalDirectoryResolver(IHostEnvironment hostEnvironment, IOptions<PicturesStorageOptions> options)
{
    private string _pathToPicturesDir = Path.Combine(hostEnvironment.ContentRootPath, options.Value.RelativeDirectoryPath);
    
    public string PathToPicturesDir => _pathToPicturesDir;

}