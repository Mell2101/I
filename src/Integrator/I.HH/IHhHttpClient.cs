using System.Text.Json;
using I.Domain.Entity;
using I.HH.Models;

namespace I.HH;

public interface IHhHttpClient
{
    public Task<HeadHunterVacancyResponse> VacanciesInfoAsyns();
}

public class HhHttpClient : IHhHttpClient
{

    private readonly HttpClient _httpClient;

    public HhHttpClient(HttpClient client)
    {
        _httpClient = client;
    }
    public async Task<HeadHunterVacancyResponse> VacanciesInfoAsyns()
    {
        string Url = "https://api.hh.ru/vacancies?area=113";
        return await GetRequestAnsyc<HeadHunterVacancyResponse>(Url);
    }

    public async Task<T> GetRequestAnsyc<T>(string url) where T : class
    {
        _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("I/1.0 (tkagochkin@list.ru)");
        
        var baseUrl = url;

        try
        {
            var response = await _httpClient.GetAsync(baseUrl);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Ошибка при запросе: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}"); 
            }

            var result = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<T>(result, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true 
            });
        }
        catch (HttpRequestException ex)
        {
            throw new Exception($"Ошибка при отправке запроса: {ex.Message}");
        }
        catch (JsonException ex)
        {
            throw new Exception($"Ошибка десериализации JSON: {ex.Message}");
        }
        catch (Exception ex)
        {
            throw new Exception($"Произошла неизвестная ошибка: {ex.Message}");
        }

    }

}