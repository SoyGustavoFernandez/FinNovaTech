using FinNovaTech.Common.Domain.Entities;
using FinNovaTech.User.Application.DTOs;
using MediatR;

namespace FinNovaTech.User.Application.Queries.Users
{
    /// <summary>
    /// Query para obtener todos los usuarios.
    /// </summary>
    public class GetAllUsersQuery : IRequest<Response<List<UserDto>>>
    {
    }
}