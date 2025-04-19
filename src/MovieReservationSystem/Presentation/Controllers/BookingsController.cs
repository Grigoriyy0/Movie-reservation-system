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
            try
            {
                var bookingId = await _mediator.Send(command);

                return Ok(bookingId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "User")]
        [HttpDelete]
        [Route("/cancel/{bookingId:guid}")]
        public async Task<IActionResult> CancelBooking(Guid bookingId)
        {
            try
            {
                var command = new CancelBookingCommand(bookingId);
                
                await _mediator.Send(command);

                return NoContent();
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
