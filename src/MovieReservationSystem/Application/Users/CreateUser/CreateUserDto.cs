namespace MovieReservationSystem.Application.Users.CreateUser;

public class CreateUserDto
{
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public string Email { get; set; }
    
    public string Password { get; set; }
    
    public string PasswordConfirm { get; set; }
}