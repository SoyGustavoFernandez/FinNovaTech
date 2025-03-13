using Microsoft.AspNetCore.Mvc;
using TransactionService.Application.Interfaces;
using TransactionService.Application.Services;
using TransactionService.Domain.Entities;

namespace TransactionService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionServices _transactionServices;

        public TransactionController(ITransactionServices transactionServices)
        {
            _transactionServices = transactionServices;
        }

        /// <summary>
        /// Obtiene el balance de una cuenta
        /// </summary>
        /// <param name="accountId">Identificador de la cuenta</param>
        /// <returns>Estado de la operación.</returns>
        [HttpGet("balance/{accountId}")]
        public async Task<IActionResult> GetBalance(int accountId)
        {
            var balance = await _transactionServices.GetAccountBalanceAsync(accountId);
            return Ok(new { AccountId = accountId, Balance = balance });
        }

        /// <summary>
        /// Realiza un deposito en una cuenta
        /// </summary>
        /// <param name="request">Datos de la transacción.</param>
        /// <returns>Estado de la operación.</returns>
        [HttpPost("deposit")]
        public async Task<IActionResult> Deposit([FromBody] TransactionRequest request)
        {
            await _transactionServices.SaveTransactionAsync(request.AccountId, request.Amount, "Deposit");
            return Ok("Depósito realizado");
        }

        /// <summary>
        /// Realiza un retiro en una cuenta
        /// </summary>
        /// <param name="request">Datos de la transacción.</param>
        /// <returns>Estado de la operación.</returns>
        [HttpPost("withdraw")]
        public async Task<IActionResult> Withdraw([FromBody] TransactionRequest request)
        {
            await _transactionServices.SaveTransactionAsync(request.AccountId, request.Amount, "Withdraw");
            return Ok("Retiro realizado");
        }
    }
}