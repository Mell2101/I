using System;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace KafkaWorker;

public class KafkaWorker : BackgroundService
{
    private readonly ILogger<KafkaWorker> _logger;

    public KafkaWorker(
        ILogger<KafkaWorker> logger
        )
    {
        _logger = logger;
    }
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        throw new NotImplementedException();
    }
}
