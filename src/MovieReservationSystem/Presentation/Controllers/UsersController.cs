using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieReservationSystem.Application.Authorization;
using MovieReservationSystem.Application.Users.AuthUser;
using MovieReservationSystem.Application.Users.CreateUser;

namespace MovieReservationSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IJwtService _jwtService;
        
        public UsersController(IMediator mediator, IJwtService jwtService)
        {
            _mediator = mediator;
            _jwtService = jwtService;
        }

        [HttpPost]
        [Route("singup")]
        public async Task<IActionResult> CreateUser(CreateUserCommand command)
        {
            try
            {
                await _mediator.Send(command);
                return Created();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("signin")]
        public async Task<IActionResult> LoginUser(AuthUserCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.Success)
            {
                return BadRequest(result.Errors);
            }
            
            var token = _jwtService.GenerateToken(command.Email, result.Value.Roles ,result.Value.UserId);
            
            return Created((string?)null, new
            {
                AccessToken = token,
            });
            
        }
    }
}
