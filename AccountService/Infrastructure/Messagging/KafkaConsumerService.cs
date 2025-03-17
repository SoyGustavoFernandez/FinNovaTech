using Confluent.Kafka;

namespace AccountService.Infrastructure.Messagging
{
    public class KafkaConsumerService : BackgroundService
    {
        private readonly IConsumer<Ignore, string> _consumer;

        public KafkaConsumerService(IConsumer<Ignore, string> consumer)
        {
            _consumer = consumer;
        }

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