using System.Security.Cryptography;
using System.Text;
using MovieReservationSystem.Infrastructure.Services.Abstract;

namespace MovieReservationSystem.Infrastructure.Services;

public class HashProvider : IHashProvider
{
    public string Hash(string pasword)
    {
        var bytes = Encoding.UTF8.GetBytes(pasword);
        var hash = SHA256.HashData(bytes);
        return Convert.ToBase64String(hash);

    }
}