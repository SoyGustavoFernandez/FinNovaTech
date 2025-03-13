using MediatR;
using Microsoft.AspNetCore.Mvc;
using TransactionService.Application.Command;
using TransactionService.Application.Queries;

namespace TransactionService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TransactionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Obtiene el balance de una cuenta
        /// </summary>
        /// <param name="accountId">Identificador de la cuenta</param>
        /// <returns>Estado de la operación.</returns>
        [HttpGet("balance/{accountId}")]
        public async Task<IActionResult> GetBalance(int accountId)
        {
            var balance = await _mediator.Send(new GetBalanceQuery(accountId));
            return Ok(balance);
        }

        /// <summary>
        /// Realiza un deposito en una cuenta
        /// </summary>
        /// <param name="command">Datos de la transacción.</param>
        /// <returns>Estado de la operación.</returns>
        [HttpPost("deposit")]
        public async Task<IActionResult> Deposit([FromBody] DepositCommand command)
        {
            var result = await _mediator.Send(command);
            return result ? Ok("Depósito realizado") : BadRequest("Error al procesar el depósito");
        }

        /// <summary>
        /// Realiza un retiro en una cuenta
        /// </summary>
        /// <param name="command">Datos de la transacción.</param>
        /// <returns>Estado de la operación.</returns>
        [HttpPost("withdraw")]
        public async Task<IActionResult> Withdraw([FromBody] WithdrawCommand command)
        {
            var result = await _mediator.Send(command);
            return result ? Ok("Retiro realizado") : BadRequest("Error al procesar el retiro");
        }
    }
}