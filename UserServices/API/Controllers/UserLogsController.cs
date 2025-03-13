using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserService.Application.Queries.Logs;

namespace UserService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserLogsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserLogsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Obtiene los logs de un usuario.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Devuelve una lista de logs de un usuario</returns>
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserLogs(int userId)
        {
            var result = await _mediator.Send(new GetLogByUserIdQuery(userId));
            if (!result.Success)
            {
                return NotFound(result);
            }
            return Ok(result);
        }
    }
}