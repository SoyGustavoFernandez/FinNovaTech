using MediatR;
using UserService.Application.DTOs;

namespace UserService.Application.Queries.Logs
{
    /// <summary>
    /// Query para obtener los logs de un usuario.
    /// </summary>
    public class GetLogByUserIdQuery : IRequest<ResponseDto<List<UserLogsDto>>>
    {
        /// <summary>
        /// Id del usuario.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="userId"></param>
        public GetLogByUserIdQuery(int userId)
        {
            UserId = userId;
        }
    }
}