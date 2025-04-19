using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieReservationSystem.Application.Movies;
using MovieReservationSystem.Application.Movies.CreateMovie;
using MovieReservationSystem.Application.Movies.DeleteMovie;
using MovieReservationSystem.Application.Movies.GetMovieById;
using MovieReservationSystem.Application.Movies.GetMovies;
using MovieReservationSystem.Application.Movies.UpdateMovie;

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
        
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("create/")]
        public async Task<IActionResult> CreateMovie([FromBody] CreateMovieCommand command)
        {
            await _mediator.Send(command);
            
            return Created();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("images/upload/")]
        public async Task<IActionResult> UploadPhotograph(Guid movieId, IFormFile image)
        {
            var command = new UploadMoviePhotographCommand(movieId, image);
            
            var result = await _mediator.Send(command);
            
            return Ok(result);
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

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteMovie(Guid id)
        {
            var command = new DeleteMovieCommand(id);
            
            await _mediator.Send(command);
            
            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        [Route("update/")]
        public async Task<IActionResult> UpdateMovie(UpdateMovieCommand command)
        {
            var result = await _mediator.Send(command);

            return Ok(result);
        }
    }
}
