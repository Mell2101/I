using System;

namespace I.Kafka;

public class KafkaSettings
{
    public string BootstrapServers { get; set; } = "localhost:9092";
    public string ConsumerGroup { get; set; } = "default-group";
    public List<string> Topics { get; set; } = new();
}

