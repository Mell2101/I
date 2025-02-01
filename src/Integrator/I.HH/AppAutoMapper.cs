using System;
using AutoMapper;
using I.Domain.Entity;
using I.HH.Models;

namespace I.HH;

public class AppAutoMapper : Profile
{
     public AppAutoMapper()
     {
        CreateMap<Salary, SalaryModel>().ReverseMap();

        CreateMap<Area, AreaModel>()
            .ForMember(d => d.Id, o => o.MapFrom(be => be.AreaId))
            .ReverseMap()
            .ForMember(d => d.AreaId, o => o.MapFrom(be => be.Id));

        CreateMap<Employer, EmployerModel>()
            .ForMember(d => d.Id, o => o.MapFrom(be => be.EmployerId))
            .ReverseMap()
            .ForMember(d => d.EmployerId, o => o.MapFrom(be => be.Id));
     }

}
