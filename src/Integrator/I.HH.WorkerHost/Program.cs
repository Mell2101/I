using I.HH.Impl;
using I.HH.WorkerHost;
using I.HH.WorkerHost.Service;
using I.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((context, config) =>
            {
                config.AddJsonFile("Configs/connectionStrings.Development.json", optional: false, reloadOnChange: true);
                config.AddEnvironmentVariables();
            })
            .ConfigureServices((context, services) =>
            {
                services.AddScoped<IHhHelperService, HhHelperService>();
                services.AddHostedService<HhWorkerService>();
                AppDIConfigure.Configure(services);
            }).Build();


await builder.RunAsync();