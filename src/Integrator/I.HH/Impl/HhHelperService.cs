using CG4.DataAccess;
using I.Domain.Entity;
using I.Service;
using AutoMapper;
using System.Text;

namespace I.HH.Impl;

public class HhHelperService : IHhHelperService
{
    private readonly ICrudService _crudService;
    private readonly IHhHttpClient _hhHttpClient;
    private readonly IMapper _mapper;

    public HhHelperService(
        ICrudService crudService,
        IHhHttpClient hhHttpClient,
        IMapper mapper
        )
    {
        _crudService = crudService;
        _hhHttpClient = hhHttpClient;
        _mapper = mapper;
    }

    public async Task CreateInfoVacancies()
    {
        var areasToAdd = new List<Area>();
        var salariesToAdd = new List<Salary>();
        var employersToAdd = new List<Employer>();

        var headHunterVacancyResponse = await _hhHttpClient.VacanciesInfoAsyns();

        foreach (var vacancy in headHunterVacancyResponse.Items)
        {
            if (vacancy.Area != null)
                areasToAdd.Add(_mapper.Map<Area>(vacancy.Area));

            if (vacancy.Salary != null)
                salariesToAdd.Add(_mapper.Map<Salary>(vacancy.Salary));

            if (vacancy.Employer != null)
                employersToAdd.Add(_mapper.Map<Employer>(vacancy.Employer));
        }

        areasToAdd = areasToAdd.DistinctBy(a => a.AreaId).ToList();
        employersToAdd = employersToAdd.DistinctBy(e => e.EmployerId).ToList();
        salariesToAdd = salariesToAdd.DistinctBy(s => new { s.From, s.To, s.Currency }).ToList();

        await CreateArea(areasToAdd);
        await CreateSalary(salariesToAdd);
        await CreateEmployer(employersToAdd);
    }

    private async Task CreateArea(List<Area> areasToAdd)
    {
        if (!areasToAdd.Any()) return;

        var sqlBuilder = new StringBuilder();
        sqlBuilder.AppendLine("INSERT INTO core_area (name, area_id) VALUES");

        sqlBuilder.AppendLine(string.Join(", ", areasToAdd.Select(a =>
            $"('{a.Name.Replace("'", "''")}', {a.AreaId})")));

        sqlBuilder.AppendLine("ON CONFLICT (area_id) DO NOTHING;");

        await _crudService.QueryAsync<Area>(sqlBuilder.ToString());
    }

    private async Task CreateEmployer(List<Employer> employersToAdd)
    {
        if (!employersToAdd.Any()) return;

        var sqlBuilder = new StringBuilder();
        sqlBuilder.AppendLine("INSERT INTO core_employer (name, url, logo_url, employer_Id) VALUES");

        sqlBuilder.AppendLine(string.Join(", ", employersToAdd.Select(e =>
            $"('{e.Name.Replace("'", "''")}', " +
            $"{(e.Url != null ? $"'{e.Url.Replace("'", "''")}'" : "NULL")}, " +
            $"{(e.LogoUrls != null ? $"'{e.LogoUrls.Replace("'", "''")}'" : "NULL")}, " +
            $"{e.EmployerId})")));

        sqlBuilder.AppendLine("ON CONFLICT (employer_Id) DO NOTHING;");

        await _crudService.QueryAsync<Employer>(sqlBuilder.ToString());
    }

    private async Task CreateSalary(List<Salary> salariesToAdd)
    {
        var sqlBuilder = new StringBuilder();
        sqlBuilder.AppendLine("INSERT INTO core_salary (from_selary, to_selary, currency) VALUES");

        bool first = true;
        foreach (var item in salariesToAdd)
        {
            // Проверяем и заменяем пустые значения на NULL
            var fromValue = item.From.HasValue ? item.From.ToString() : "NULL";
            var toValue = item.To.HasValue ? item.To.ToString() : "NULL";
            var currencyValue = item.Currency != null ? $"'{item.Currency}'" : "NULL";

            if (!first)
            {
                sqlBuilder.AppendLine(",");
            }
            first = false;

            sqlBuilder.Append($"({fromValue}, {toValue}, {currencyValue})");
        }

        var sqlQuery = sqlBuilder.ToString();

        await _crudService.QueryAsync<Salary>(sqlQuery);
    }
}
