namespace MovieReservationSystem.Application.Movies.CreateMovie;

public class CreateMovieDto
{
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public string Category { get; set; }
    
    public decimal TicketPrice { get; set; }
    
    public int NumberOfSeats { get; set; }
    
    public string ImageUrl { get; set; }
    
    public DateTime ShowTime { get; set; }
}