using Confluent.Kafka;
using FinNovaTech.Common;
using FinNovaTech.Common.Domain.Entities;
using FinNovaTech.User.Application.Interfaces;
using userDomain = FinNovaTech.User.Domain.Entities;
using MediatR;
using System.Net;
using System.Text.Json;

namespace FinNovaTech.User.Application.Commands.Users.Handler
{
    /// <summary>
    /// Handler para registrar un usuario.
    /// </summary>
    public class CreateUserHandler(IUserRepository repository, IUserValidation userService, IUserLogRepository userRepository, IProducer<Null, string> kafkaConsumer, Argon2Hasher argon2Hasher) : IRequestHandler<CreateUserCommand, Response<string>>
    {
        private readonly IUserRepository _repository = repository;
        private readonly IUserValidation _userService = userService;
        private readonly IUserLogRepository _userLogRepository = userRepository;
        private readonly IProducer<Null, string> _kafkaConsumer = kafkaConsumer;
        private readonly Argon2Hasher _argon2Hasher = argon2Hasher;

        public async Task<Response<string>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            if (!await _userService.ValidateUserFormatEmailAsync(request.Email))
            {
                return new Response<string>(false, "Formato de correo incorrecto", null, (int)HttpStatusCode.BadRequest);
            }

            var emailExists = await _repository.EmailExistsAsync(request.Email);
            if (emailExists)
            {
                return new Response<string>(false, "El email ya está registrado", null, (int)HttpStatusCode.BadRequest);
            }

            string salt = Guid.NewGuid().ToString();

            userDomain.User user = new()
            {
                Name = request.Name,
                Email = request.Email,
                PasswordHash = _argon2Hasher.HashPassword(request.Password, salt),
                Salt = salt,
                RoleId = request.Role
            };

            await _repository.AddUserAsync(user);

            await _userLogRepository.AddLogAsync(new userDomain.UserLog
            {
                UserId = user.Id,
                Action = "Usuario registrado",
            });

            var message = new Message<Null, string> { Value = JsonSerializer.Serialize(user) };

            var userEventJson = JsonSerializer.Serialize(user);

            await _kafkaConsumer.ProduceAsync("user-created", message);
            return new Response<string>(true, "Usuario registrado exitosamente", null, (int)HttpStatusCode.Created);
        }
    }
}