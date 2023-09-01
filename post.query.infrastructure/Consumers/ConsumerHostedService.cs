using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace post.query.infrastructure.Consumers
{
    public class ConsumerHostedService : IHostedService
    {
        private readonly ILogger<ConsumerHostedService> _logger;
        private readonly IServiceProvider _serviceProvider;

        public ConsumerHostedService(ILogger<ConsumerHostedService> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Event consumer service running");

            using(IServiceScope scope = _serviceProvider.CreateScope()) 
            {
                var eventConsumer = scope.ServiceProvider.GetRequiredService<EventConsumer>();
                var topic = Environment.GetEnvironmentVariable("KAFKA_TOPIC");

                Task.Run(() => eventConsumer.Consume(topic), cancellationToken);

                return Task.CompletedTask;
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Event consumer service running");

            return Task.CompletedTask;
        }
    }
}
