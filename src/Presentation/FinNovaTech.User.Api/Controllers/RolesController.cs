using FinNovaTech.User.Application.Commands.Roles;
using FinNovaTech.User.Application.Queries.Roles;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FinNovaTech.User.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RolesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Lista todos los roles registrados en la aplicación.
        /// </summary>
        /// <returns>Devuelve una lista de roles</returns>
        [HttpGet()]
        public async Task<IActionResult> GetAllRoles()
        {
            var result = await _mediator.Send(new GetAllRolesQuery());
            if (!result.Success)
            {
                return NotFound(result);
            }
            return Ok(result);
        }

        /// <summary>
        /// Lista un rol por su Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Devuelve un rol</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoleById(int id)
        {
            var result = await _mediator.Send(new GetRoleByIdQuery(id));
            if (!result.Success)
            {
                return NotFound(result);
            }
            return Ok(result);
        }

        /// <summary>
        /// Registra un nuevo rol en la aplicación.
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Devuelve 201 si el rol se creó correctamente</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Created("", result);
        }

        /// <summary>
        /// Actualiza un rol en la aplicación.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns>Devuelve 200 si el rol se actualizó correctamente</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateRole(int id, [FromBody] UpdateRoleCommand command)
        {
            command.Id = id;
            var result = await _mediator.Send(command);
            if (!result.Success)
            {
                return NotFound(result);
            }
            return Ok(result);
        }

        /// <summary>
        /// Elimina un rol de la aplicación.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Devuelve 200 si el rol se eliminó correctamente</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteRole(int id)
        {
            var result = await _mediator.Send(new DeleteRoleCommand(id));
            if (!result.Success)
            {
                return NotFound(result);
            }
            return Ok(result);
        }
    }
}