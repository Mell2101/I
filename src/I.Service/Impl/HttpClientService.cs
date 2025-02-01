using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace I.Service.Impl;
public class HttpClientService : IHttpClientService
{
    private readonly HttpClient _httpClient;

    public HttpClientService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<string> GetQuery(string url)
    {
        //header
        _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("I/1.0 (tkagochkin@list.ru)");
        
        var baseUrl = url;

        try
        {
            var response = await _httpClient.GetAsync(baseUrl);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Ошибка при запросе: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}"); 
            }

            var content = await response.Content.ReadAsStringAsync();
            
            return content;
             
        }
        catch (HttpRequestException ex)
        {
            throw new Exception($"Ошибка при отправке запроса: {ex.Message}"); 
        }

    }
}
