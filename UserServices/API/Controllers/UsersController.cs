using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserService.Application.Commands.Users;
using UserService.Application.Queries.Users;

namespace UserService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _mediator.Send(new GetUserQueryById(id));

            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpGet()]
        public async Task<IActionResult> GetUser()
        {
            var users = await _mediator.Send(new GetAllUsersQuery());
            return Ok(users);
        }
    }
}