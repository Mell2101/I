using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

const string ENVIRONMENT_VARIABLE_NAME = "ASPNETCORE_ENVIRONMENT";
const string DATABASE_NAME = "I";

var env = Environment.GetEnvironmentVariable(ENVIRONMENT_VARIABLE_NAME);

var config = new ConfigurationBuilder()
    .AddJsonFile($"Configs/connectionStrings.Development.json")
    .AddEnvironmentVariables()
    .Build();

    var serviceProvider = new ServiceCollection()
        .AddSingleton<IConfiguration>(new ConfigurationBuilder()
            .AddJsonFile($"Configs/connectionStrings.Development.json")
            .AddEnvironmentVariables()
            .Build())
        .AddFluentMigratorCore()
        .ConfigureRunner(rb => rb
            .AddPostgres10_0()
            .WithGlobalConnectionString(x => x.GetRequiredService<IConfiguration>().GetConnectionString(DATABASE_NAME))
            .WithGlobalCommandTimeout(TimeSpan.FromMinutes(5))
            .ScanIn(typeof(Program).Assembly).For.Migrations())
        .AddLogging(lb => lb.AddFluentMigratorConsole())
        .BuildServiceProvider(false);

using (var scope = serviceProvider.CreateScope())
{
    var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
    runner.MigrateUp();
    //runner.MigrateDown(0);
}