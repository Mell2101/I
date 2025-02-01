using System;
using System.ComponentModel.DataAnnotations.Schema;
using CG4.DataAccess.Domain;

namespace I.Domain.Entity;

[Table("core_area")]
public class Area : EntityBase
{
    [Column("name")]
    public string Name { get; set; } 

    [Column("area_id")]
    public string AreaId { get; set; } 

}
