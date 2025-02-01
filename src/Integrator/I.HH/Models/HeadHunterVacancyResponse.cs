using System;
using System.Formats.Asn1;

namespace I.HH.Models;

public class HeadHunterVacancyResponse
{
    public List<VacancyModel> Items { get; set; } 
    public long Found { get; set; } 
    public long Pages { get; set; }
    public long PerPage { get; set; } 
    public long Page { get; set; }
    public DateTime? ClustersUpdatedAt { get; set; }
}

public class VacancyModel
{
    public string Id { get; set; } 
    public string Name { get; set; } 
    public AreaModel Area { get; set; } 
    public SalaryModel Salary { get; set; } 
    public EmployerModel Employer { get; set; } 
    public DateTime? PublishedAt { get; set; }
}

public class AreaModel
{
    public string Id { get; set; } 
    public string Name { get; set; } 
}

public class SalaryModel
{
    public decimal? From { get; set; } 
    public decimal? To { get; set; } 
    public string Currency { get; set; } 
    public bool? Gross { get; set; } 
}

public class EmployerModel
{
    public string Id { get; set; } 
    public string Name { get; set; } 
    public string Url { get; set; } 
    public string LogoUrls { get; set; } 
}



