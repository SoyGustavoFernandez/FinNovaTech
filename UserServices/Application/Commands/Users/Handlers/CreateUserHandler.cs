using Confluent.Kafka;
using MediatR;
using System.Net;
using System.Text.Json;
using UserService.Application.DTOs;
using UserService.Application.Interfaces;
using UserService.Domain.Entities;
using UserService.Infrastructure.Messaging;
using UserService.Shared;

namespace UserService.Application.Commands.Users.Handlers
{
    /// <summary>
    /// Handler para registrar un usuario.
    /// </summary>
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, ResponseDto<string>>
    {
        private readonly IUserRepository _repository;
        private readonly IUserValidation _userService;
        private readonly IUserLogRepository _userLogRepository;
        private readonly KafkaProducerService _kafkaProducer;
        private readonly IProducer<Null, string> _kafkaConsumer;
        private readonly Argon2Hasher _argon2Hasher;

        public CreateUserHandler(IUserRepository repository, IUserValidation userService, IUserLogRepository userRepository, KafkaProducerService kafkaProducer, IProducer<Null, string> kafkaConsumer, Argon2Hasher argon2Hasher)
        {
            _repository = repository;
            _userService = userService;
            _userLogRepository = userRepository;
            _kafkaProducer = kafkaProducer;
            _kafkaConsumer = kafkaConsumer;
            _argon2Hasher = argon2Hasher;
        }

        public async Task<ResponseDto<string>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            if (!await _userService.ValidateUserFormatEmailAsync(request.Email))
            {
                return new ResponseDto<string>(false, "Formato de correo incorrecto", null, (int)HttpStatusCode.BadRequest);
            }

            var emailExists = await _repository.EmailExistsAsync(request.Email);
            if (emailExists)
            {
                return new ResponseDto<string>(false, "El email ya está registrado", null, (int)HttpStatusCode.BadRequest);
            }

            string salt = Guid.NewGuid().ToString();

            User user = new()
            {
                Name = request.Name,
                Email = request.Email,
                PasswordHash = _argon2Hasher.HashPassword(request.Password, salt),
                Salt = salt,
                RoleId = request.Role
            };

            await _repository.AddUserAsync(user);

            await _userLogRepository.AddLogAsync(new UserLogs
            {
                UserId = user.Id,
                Action = "Usuario registrado",
            });

            var message = new Message<Null, string> { Value = JsonSerializer.Serialize(user) };

            var userEventJson = JsonSerializer.Serialize(user);

            await _kafkaProducer.SendMessageAsync("user-created", userEventJson);

            await _kafkaConsumer.ProduceAsync("user-created", message);
            return new ResponseDto<string>(true, "Usuario registrado exitosamente", null, (int)HttpStatusCode.Created);
        }
    }
}