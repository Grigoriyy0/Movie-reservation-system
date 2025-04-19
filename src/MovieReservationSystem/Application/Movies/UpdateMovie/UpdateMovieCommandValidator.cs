using FluentValidation;

namespace MovieReservationSystem.Application.Movies.UpdateMovie;

public class UpdateMovieCommandValidator : AbstractValidator<UpdateMovieCommand>
{
    public UpdateMovieCommandValidator()
    {
        RuleFor(m => m.NumberOfSeats).Must(n => n > 0).WithMessage("Number of seats must be greater than zero");
        RuleFor(m => m.TicketPrice).Must(price => price > 0).WithMessage("Price must be greater than zero");
        RuleFor(m => m.Title).NotEmpty();
        RuleFor(m => m.Description).NotEmpty();
    }
}