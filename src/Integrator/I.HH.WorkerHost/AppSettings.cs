using CG4.DataAccess;
using Microsoft.Extensions.Configuration;

namespace I.HH.WorkerHost;

public class AppSettings : IConnectionSettings
{
    public string ConnectionString { get; set; }
    public AppSettings(IConfiguration config)
    {
        ConnectionString = config.GetConnectionString("I");
    }
}
