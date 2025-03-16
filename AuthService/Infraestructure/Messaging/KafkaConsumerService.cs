using AuthService.Application.DTOs;
using AuthService.Application.Interfaces;
using AuthService.Domain.Entities;
using Confluent.Kafka;
using System.Text.Json;

namespace AuthService.Infraestructure.Messaging
{
    public class KafkaConsumerService : BackgroundService
    {
        private readonly IServiceScopeFactory _ScopeFactory;
        private readonly IConsumer<string, string> _consumer;

        public KafkaConsumerService(IServiceScopeFactory scopeFactory)
        {
            _ScopeFactory = scopeFactory;

            var config = new ConsumerConfig
            {
                BootstrapServers = "localhost:9092",
                GroupId = "auth-service-group",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            _consumer = new ConsumerBuilder<string, string>(config).Build();
            _consumer.Subscribe("user-created");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var consumeResult = _consumer.Consume(stoppingToken);
                    var userEvent = JsonSerializer.Deserialize<User>(consumeResult.Value);

                    using (var scope = _ScopeFactory.CreateScope())
                    {
                        var context = scope.ServiceProvider.GetRequiredService<ILoginRepository>();

                        var userExist = await context.GetUserByEmailAsync(userEvent.Email);

                        if(userExist == null)
                        {
                            userEvent.Id = 0;
                            await context.AddUserAsync(userEvent);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ Error procesando mensaje de Kafka: {ex.Message}");
                }
            }
        }
    }
}