using System;
using System.ComponentModel.DataAnnotations.Schema;
using CG4.DataAccess.Domain;

namespace I.Domain.Entity;

[Table("core_employer")]
public class Employer : EntityBase
{
    [Column("name")]
    public string Name { get; set; } 
    
    [Column("url")]
    public string Url { get; set; } 
    
    [Column("logo_url")]
    public string LogoUrls { get; set; } 
    
    [Column("employer_id")]
     public string EmployerId { get; set; } 
}
