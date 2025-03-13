using MediatR;
using UserService.Application.DTOs;

namespace UserService.Application.Queries.Users
{
    public class GetUserQueryById : IRequest<ResponseDTO<UserDTO>>
    {
        public int Id { get; set; }

        public GetUserQueryById(int id)
        {
            Id = id;
        }
    }
}