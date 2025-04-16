using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieReservationSystem.Application.Movies.CreateMovie;
using MovieReservationSystem.Application.Movies.GetMovieById;
using MovieReservationSystem.Application.Movies.GetMovies;

namespace MovieReservationSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MoviesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("create/")]
        public async Task<IActionResult> CreateMovie(CreateMovieCommand command)
        {
            await _mediator.Send(command);
            
            return Created();
        }

        [HttpGet]
        public async Task<IActionResult> GetMovies()
        {
            var result = await _mediator.Send(new GetMoviesCommand());
            
            return Ok(result);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetMovieById(Guid id)
        {
            try
            {
                var movie = await _mediator.Send(new GetMovieByIdCommand(id));
                return Ok(movie);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
