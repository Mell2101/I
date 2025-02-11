using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace I.Kafka;
public class KafkaConsumer : BackgroundService
{
    private readonly ILogger<KafkaConsumer> _logger;
    private readonly IConsumer<string, string> _consumer;
    private readonly Dictionary<string, Func<string, Task>> _handlers;

    public KafkaConsumer(ILogger<KafkaConsumer> logger, IOptions<KafkaSettings> kafkaSettings)
    {
        _logger = logger;
        _handlers = new Dictionary<string, Func<string, Task>>();

        var config = new ConsumerConfig
        {
            BootstrapServers = kafkaSettings.Value.BootstrapServers,
            GroupId = kafkaSettings.Value.ConsumerGroup,
            AutoOffsetReset = AutoOffsetReset.Earliest
        };

        _consumer = new ConsumerBuilder<string, string>(config).Build();
        _consumer.Subscribe(kafkaSettings.Value.Topics);
    }

    public void RegisterHandler(string key, Func<string, Task> handler)
    {
        _handlers[key] = handler;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var message = _consumer.Consume(stoppingToken);
                _logger.LogInformation($"Received message: {message.Message.Value}");

                if (_handlers.TryGetValue(message.Message.Key, out var handler))
                {
                    await handler(message.Message.Value);
                }
                else
                {
                    _logger.LogWarning($"No handler found for key: {message.Message.Key}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kafka consumption error");
            }
        }
    }

    public override Task StopAsync(CancellationToken cancellationToken)
    {
        _consumer.Close();
        return base.StopAsync(cancellationToken);
    }
}
