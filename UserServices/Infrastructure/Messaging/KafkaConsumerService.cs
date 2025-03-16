using Confluent.Kafka;
using System.Text.Json;
using UserService.Application.Interfaces;

namespace UserService.Infrastructure.Messaging
{
    public class KafkaConsumerService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IConsumer<Ignore, string> _consumer;
        private readonly IProducer<Null, string> _producer;

        public KafkaConsumerService(IServiceProvider serviceProvider, IConsumer<Ignore, string> consumer, IProducer<Null, string> producer)
        {
            _serviceProvider = serviceProvider;
            _consumer = consumer;
            _producer = producer;
        }

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

                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var userRepository = scope.ServiceProvider.GetRequiredService<IUserRepository>();

                        var userId = int.Parse(consumeResult.Message.Value);

                        var user = await userRepository.GetUserByIdAsync(userId);

                        if (user == null)
                        {
                            var responseMessage = new Message<Null, string> { Value = JsonSerializer.Serialize(false) };

                            await _producer.ProduceAsync("user-validation-response", responseMessage);

                            continue;
                        }
                        else
                        {

                            var responseMessage = new Message<Null, string> { Value = JsonSerializer.Serialize(true) };

                            await _producer.ProduceAsync("user-validation-response", responseMessage);
                        }
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