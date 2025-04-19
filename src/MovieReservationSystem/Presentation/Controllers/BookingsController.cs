using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieReservationSystem.Application.Bookings.CancelBooking;
using MovieReservationSystem.Application.Bookings.CreateBooking;

namespace MovieReservationSystem.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BookingsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        public async Task<IActionResult> Book(CreateBookingCommand command)
        {
            var bookingId = await _mediator.Send(command);
            
            return Ok(bookingId);
        }

        [Authorize(Roles = "User")]
        [HttpDelete]
        public async Task<IActionResult> CancelBooking(CancelBookingCommand command)
        {
            await _mediator.Send(command);
            
            return NoContent();
        }
    }
}
