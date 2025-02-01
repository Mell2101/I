using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using I.Service.Model;

namespace I.Service.Impl;

public class HHClientService : IHHClientService
{
    private readonly IHttpClientService _httpClientService;
    private static string Url = "https://api.hh.ru/vacancies?area=113"; // запрос вакансий по России

    public HHClientService(IHttpClientService httpClientService)
    {
        _httpClientService = httpClientService;
    }
    
    public async Task<HHModelService> GetVacancies()
    {
        var vacanciesJson = await _httpClientService.GetQuery(Url);

        return new HHModelService()
        {
            Json = vacanciesJson
        };
    }

}
