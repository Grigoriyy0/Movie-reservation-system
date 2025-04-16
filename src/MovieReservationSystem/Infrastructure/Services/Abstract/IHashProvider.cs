namespace MovieReservationSystem.Infrastructure.Services.Abstract;

public interface IHashProvider
{
    string Hash(string pasword);
}