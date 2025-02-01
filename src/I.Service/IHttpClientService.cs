using System.Threading.Tasks;

namespace I.Service;

public interface IHttpClientService
{
    public Task<string> GetQuery(string query);
}
