using MediatR;
using System.Net;
using UserService.Application.DTOs;

namespace UserService.Application.Commands.Users
{
    public class UpdateUserPasswordCommand: IRequest<ResponseDTO<string>>
    {
        public int Id { get; set; }
        public string Password { get; set; }

        public UpdateUserPasswordCommand(int id, string password)
        {
            Id = id;
            Password = password;
        }
    }
}
