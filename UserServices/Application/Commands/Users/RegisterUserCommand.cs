using MediatR;
using System.Net;

namespace UserService.Application.Commands.Users
{
    public class RegisterUserCommand : IRequest<HttpStatusCode>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public RegisterUserCommand(string name, string email, string password)
        {
            Name = name;
            Email = email;
            Password = password;
        }
    }
}