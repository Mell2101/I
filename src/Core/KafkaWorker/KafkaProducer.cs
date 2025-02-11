using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace I.Kafka;
public class KafkaProducer
{
    private readonly IProducer<string, string> _producer;
    private readonly ILogger<KafkaProducer> _logger;
    private readonly string _topic;

    public KafkaProducer(ILogger<KafkaProducer> logger, string bootstrapServers, string topic)
    {
        _logger = logger;
        _topic = topic;

        var config = new ProducerConfig
        {
            BootstrapServers = bootstrapServers
        };

        _producer = new ProducerBuilder<string, string>(config).Build();
    }

    public async Task SendMessageAsync(string key, string message)
    {
        try
        {
            var deliveryResult = await _producer.ProduceAsync(_topic, new Message<string, string>
            {
                Key = key,
                Value = message
            });

            _logger.LogInformation($"Message sent to {deliveryResult.TopicPartitionOffset}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error sending message to Kafka");
        }
    }
}
