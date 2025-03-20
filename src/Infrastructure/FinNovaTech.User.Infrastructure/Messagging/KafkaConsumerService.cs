using Confluent.Kafka;
using FinNovaTech.User.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Json;

namespace FinNovaTech.User.Infrastructure.Messagging
{
    public class KafkaConsumerService(IServiceProvider serviceProvider, IConsumer<Ignore, string> consumer, IProducer<Null, string> producer) : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider = serviceProvider;
        private readonly IConsumer<Ignore, string> _consumer = consumer;
        private readonly IProducer<Null, string> _producer = producer;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Run(() => ConsumeMessages(stoppingToken), stoppingToken);

        }

        private async Task ConsumeMessages(CancellationToken stoppingToken)
        {
            _consumer.Subscribe("user-validation-request");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var consumeResult = _consumer.Consume(stoppingToken);

                    using var scope = _serviceProvider.CreateScope();

                    var userRepository = scope.ServiceProvider.GetRequiredService<IUserRepository>();

                    var userId = int.Parse(consumeResult.Message.Value);

                    var user = await userRepository.GetUserByIdAsync(userId);

                    if (user == null)
                    {
                        var responseMessage = new Message<Null, string> { Value = JsonSerializer.Serialize(false) };

                        await _producer.ProduceAsync("user-validation-response", responseMessage, stoppingToken);

                        continue;
                    }
                    else
                    {
                        var responseMessage = new Message<Null, string> { Value = JsonSerializer.Serialize(true) };

                        await _producer.ProduceAsync("user-validation-response", responseMessage, stoppingToken);
                    }
                }
                catch (ConsumeException e)
                {
                    Console.WriteLine($"Error al consumir mensaje de Kafka: {e.Error.Reason}");
                }
            }
        }
    }
}