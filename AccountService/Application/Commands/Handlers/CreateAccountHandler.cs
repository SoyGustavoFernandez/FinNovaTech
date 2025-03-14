using AccountService.Application.DTOs;
using AccountService.Application.Interfaces;
using AccountService.Domain.Entities;
using Confluent.Kafka;
using MediatR;
using System.Net;
using System.Text.Json;

namespace AccountService.Application.Commands.Handlers
{
    public class CreateAccountHandler : IRequestHandler<CreateAccountCommand, ResponseDTO<int>>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IProducer<Null, string> _kafkaProducer;
        private readonly IConsumer<Ignore, string> _kafkaConsumer;

        public CreateAccountHandler(IAccountRepository accountRepository, IProducer<Null, string> kafkaProducer, IConsumer<Ignore, string> kafkaConsumer)
        {
            _accountRepository = accountRepository;
            _kafkaProducer = kafkaProducer;
            _kafkaConsumer = kafkaConsumer;
        }

        public async Task<ResponseDTO<int>> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            var requestMessage = new Message<Null, string> { Value = request.UserId.ToString() };
            await _kafkaProducer.ProduceAsync("user-validation-request", requestMessage);

            _kafkaConsumer.Subscribe("user-validation-response");

            var consumeResult = _kafkaConsumer.Consume(cancellationToken);
            var userExists = JsonSerializer.Deserialize<bool>(consumeResult.Message.Value);

            if (!userExists)
            {
                return new ResponseDTO<int>(false, "El usuario no existe", 0, (int)HttpStatusCode.BadRequest);
            }

            var account = new Account
            {
                UserId = request.UserId,
                AccountType = request.AccountType
            };

            await _accountRepository.CreateAsync(account);

            return new ResponseDTO<int>(true, "Cuenta creada correctamente", account.Id, (int)HttpStatusCode.Created);
        }
    }
}