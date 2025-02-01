using System;
using I.Service;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace I.HH.WorkerHost.Service;

public class HhWorkerService : BackgroundService
{
    private readonly IHhHelperService _hhHelperService;
    private readonly ILogger<HhWorkerService> _logger;
    private TimeSpan _interval = TimeSpan.FromDays(1);

    public HhWorkerService(
        IHhHelperService hhHelperService,
        ILogger<HhWorkerService> logger
        )
    {
        _hhHelperService = hhHelperService;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Start");

        while(!stoppingToken.IsCancellationRequested)
        {
            try
            {
                _logger.LogInformation("Start hh info");
                await _hhHelperService.CreateInfoVacancies();
                _logger.LogInformation("Success");
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Ошибка при получении данных с HH");
            }
            await Task.Delay(_interval, stoppingToken);
        }
    }
}
