namespace MovieReservationSystem.Domain.Entities;

public class Movie
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public DateTime ShowTime { get; set; }
    
    public int AvailableSeats { get; set; }
    
    public int NumberOfSeats { get; set; }
    
    public string ImageUrl { get; set; }
    
    public string Genre { get; set; }
    
    public decimal TicketPrice { get; set; }
    
    public ICollection<Booking> Bookings { get; set; }
}