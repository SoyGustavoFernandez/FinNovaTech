using FinNovaTech.Account.Application.Commands;
using FinNovaTech.Account.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FinNovaTech.Account.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Devuelve todas las cuentas.
        /// </summary>
        /// <returns>Datos de las cuentas.</returns>
        [HttpGet()]
        public async Task<IActionResult> GetAllAccount()
        {
            var result = await _mediator.Send(new GetAllAccountQuery());
            if (!result.Success)
            {
                return NotFound(result);
            }
            return Ok(result);
        }

        /// <summary>
        /// Devuelve una cuenta por su id.
        /// </summary>
        /// <param name="id">Identificador de la cuenta.</param>
        /// <returns>Datos de la cuenta.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAccountById(int id)
        {
            var result = await _mediator.Send(new GetAccountByIdQuery(id));
            if (!result.Success)
            {
                return NotFound(result);
            }
            return Ok(result);
        }

        /// <summary>
        /// Crea una cuenta.
        /// </summary>
        /// <param name="command">Datos de la cuenta.</param>
        /// <returns>Devuelve el resultado de la operación.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateAccount([FromBody] CreateAccountCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        /// <summary>
        /// Actualiza una cuenta.
        /// </summary>
        /// <param name="command">Datos de la cuenta.</param>
        /// <returns>Devuelve el resultado de la operación.</returns>
        [HttpPut]
        public async Task<IActionResult> UpdateAccount([FromBody] UpdateAccountCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}