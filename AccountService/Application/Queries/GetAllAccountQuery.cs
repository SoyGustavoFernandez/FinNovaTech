using AccountService.Application.DTOs;
using MediatR;

namespace AccountService.Application.Queries
{
    /// <summary>
    /// Query para obtener todas las cuentas.
    /// </summary>
    public class GetAllAccountQuery : IRequest<ResponseDTO<List<AccountDto>>>
    {
    }
}