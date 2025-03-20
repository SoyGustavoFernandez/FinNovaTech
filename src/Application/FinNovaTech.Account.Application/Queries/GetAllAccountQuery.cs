using FinNovaTech.Account.Application.DTOs;
using FinNovaTech.Common.Domain.Entities;
using MediatR;

namespace FinNovaTech.Account.Application.Queries
{
    /// <summary>
    /// Query para obtener todas las cuentas.
    /// </summary>
    public class GetAllAccountQuery : IRequest<Response<List<AccountDto>>>
    {
    }
}