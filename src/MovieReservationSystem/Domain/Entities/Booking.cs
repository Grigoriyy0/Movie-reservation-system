namespace MovieReservationSystem.Domain.Entities;

public class Booking
{
    public Guid UserId { get; set; }
    
    public User User { get; set; }
    
    public Guid MovieId { get; set; }
    
    public Movie Movie { get; set; }
    
    public BookingStatus Status { get; set; }
    
    public int SeatNumber { get; set; }
}