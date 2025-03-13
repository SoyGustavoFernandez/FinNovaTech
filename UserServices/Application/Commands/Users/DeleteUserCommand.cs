using MediatR;
using UserService.Application.DTOs;

namespace UserService.Application.Commands.Users
{
    public class DeleteUserCommand : IRequest<ResponseDTO<string>>
    {
        public int Id { get; set; }

        public DeleteUserCommand(int id)
        {
            Id = id;
        }
    }
}