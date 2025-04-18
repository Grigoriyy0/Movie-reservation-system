using FluentValidation;

namespace MovieReservationSystem.Application.Movies.CreateMovie;

public class CreateMovieCommandValidator : AbstractValidator<CreateMovieCommand>
{
    public CreateMovieCommandValidator()
    {
        RuleFor(m => m.TicketPrice).Must(price => price > 0).WithMessage("Ticket price must be greater than zero");
        RuleFor(m => m.Name).NotEmpty();
        RuleFor(m => m.Description).NotEmpty();
        RuleFor(m => m.NumberOfSeats).GreaterThan(0);
        RuleFor(m => m.Category).NotEmpty();
    }
}