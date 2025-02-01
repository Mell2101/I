using System.Collections.Generic;

namespace I.Web.Models.HHModels;

public class VacanciesMetaModel
{
    public int Found { get; set; }
    public List<Vacancy> Items { get; set; }
}

public class Vacancy
{
}