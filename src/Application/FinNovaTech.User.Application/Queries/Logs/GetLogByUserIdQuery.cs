using FinNovaTech.Common.Domain.Entities;
using FinNovaTech.User.Application.DTOs;
using MediatR;

namespace FinNovaTech.User.Application.Queries.Logs
{
    /// <summary>
    /// Query para obtener los logs de un usuario.
    /// </summary>
    /// <remarks>
    /// Constructor.
    /// </remarks>
    /// <param name="userId"></param>
    public class GetLogByUserIdQuery(int userId) : IRequest<Response<List<UserLogsDto>>>
    {
        /// <summary>
        /// Id del usuario.
        /// </summary>
        public int UserId { get; set; } = userId;
    }
}