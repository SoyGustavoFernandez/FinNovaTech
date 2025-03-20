using accountEntity = FinNovaTech.Account.Domain.Entities;
using Confluent.Kafka;
using FinNovaTech.Account.Application.Interfaces;
using FinNovaTech.Common.Domain.Entities;
using MediatR;
using System.Net;
using System.Text.Json;

namespace FinNovaTech.Account.Application.Commands.Handlers
{
    public class CreateAccountHandler(IAccountRepository accountRepository, IProducer<Null, string> kafkaProducer, IConsumer<Ignore, string> kafkaConsumer) : IRequestHandler<CreateAccountCommand, Response<int>>
    {
        private readonly IAccountRepository _accountRepository = accountRepository;
        private readonly IProducer<Null, string> _kafkaProducer = kafkaProducer;
        private readonly IConsumer<Ignore, string> _kafkaConsumer = kafkaConsumer;

        public async Task<Response<int>> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            var requestMessage = new Message<Null, string> { Value = request.UserId.ToString() };
            await _kafkaProducer.ProduceAsync("user-validation-request", requestMessage);

            _kafkaConsumer.Subscribe("user-validation-response");

            var consumeResult = _kafkaConsumer.Consume(cancellationToken);
            var userExists = JsonSerializer.Deserialize<bool>(consumeResult.Message.Value);

            if (!userExists)
            {
                return new Response<int>(false, "El usuario no existe", 0, (int)HttpStatusCode.BadRequest);
            }

            var account = new accountEntity.Account
            {
                UserId = request.UserId,
                AccountType = request.AccountType
            };

            await _accountRepository.CreateAsync(account);

            return new Response<int>(true, "Cuenta creada correctamente", account.Id, (int)HttpStatusCode.Created);
        }
    }
}