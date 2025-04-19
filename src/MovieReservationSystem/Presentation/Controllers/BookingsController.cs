using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieReservationSystem.Application.Bookings.CreateBooking;
using MovieReservationSystem.Domain.Entities;

namespace MovieReservationSystem.Controllers
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

        [HttpPost]
        public async Task<IActionResult> Book(CreateBookingCommand command)
        {
            var bookingId = await _mediator.Send(command);
            
            return Ok(bookingId);
        }
    }
}
