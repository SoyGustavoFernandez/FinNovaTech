using Confluent.Kafka;
using Microsoft.Extensions.Hosting;

namespace FinNovaTech.Account.Infrastructure.Messagging
{
    public class KafkaConsumerService(IConsumer<Ignore, string> consumer) : BackgroundService
    {
        private readonly IConsumer<Ignore, string> _consumer = consumer;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _consumer.Subscribe("user-validation-response");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var consumeResult = _consumer.Consume(stoppingToken);
                }
                catch (ConsumeException e)
                {
                    Console.WriteLine($"Error al consumir mensaje de Kafka: {e.Error.Reason}");
                }
            }
        }
    }
}