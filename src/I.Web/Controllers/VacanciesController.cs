using System.Text.Json;
using System.Threading.Tasks;
using I.Service;
using I.Web.Models;
using I.Web.Models.HHModels;
using Microsoft.AspNetCore.Mvc;

namespace I.Web.Controllers;

[Route("[controller]")]
public class VacanciesController : Controller
{
    private readonly IHHClientService _hhClientService;
    public VacanciesController(IHHClientService hhClientService)
    {
        _hhClientService = hhClientService;
    }

    [HttpGet("view/all")]
    public async Task<IActionResult> ViewAll()
    {
        var hhModelService = await _hhClientService.GetVacancies();

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true 
        };
        var vacaniesCount = JsonSerializer.Deserialize<VacanciesMetaModel>(hhModelService.Json,options);

        var viewModel = new VacanciesModel
        {
            VacaniesCount = vacaniesCount.Found
        };

        return View("ViewAll",viewModel);
    }
}
