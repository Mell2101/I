using System;
using System.ComponentModel.DataAnnotations.Schema;
using CG4.DataAccess.Domain;

namespace I.Domain.Entity;

[Table("core_salary")]
public class Salary : EntityBase
{
    [Column("from_selary")]
    public decimal? From { get; set; } 
    
    [Column("to_selary")]
    public decimal? To { get; set; } 

    [Column("currency")]
    public string Currency { get; set; } 
}
