﻿using FinNovaTech.Transaction.Application.Commands;
using FinNovaTech.Transaction.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FinNovaTech.Transaction.Api.Controllers
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
            var response = await _mediator.Send(new GetBalanceQuery(accountId));
            return StatusCode(response.StatusCode, response);
        }

        /// <summary>
        /// Realiza un deposito en una cuenta
        /// </summary>
        /// <param name="command">Datos de la transacción.</param>
        /// <returns>Estado de la operación.</returns>
        [HttpPost("deposit")]
        public async Task<IActionResult> Deposit([FromBody] DepositCommand command)
        {
            var response = await _mediator.Send(command);
            return StatusCode(response.StatusCode, response);
        }

        /// <summary>
        /// Realiza un retiro en una cuenta
        /// </summary>
        /// <param name="command">Datos de la transacción.</param>
        /// <returns>Estado de la operación.</returns>
        [HttpPost("withdraw")]
        public async Task<IActionResult> Withdraw([FromBody] WithdrawCommand command)
        {
            var response = await _mediator.Send(command);
            return StatusCode(response.StatusCode, response);
        }
    }
}