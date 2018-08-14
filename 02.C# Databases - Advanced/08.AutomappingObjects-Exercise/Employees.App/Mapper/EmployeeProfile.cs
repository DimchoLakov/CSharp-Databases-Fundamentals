using AutoMapper;
using Employees.App.DTOs;
using Employees.Models;

namespace Employees.App.Mapper
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeDto>();
            CreateMap<Employee, ManagerDto>();

            CreateMap<EmployeeDto, Employee>();
            CreateMap<ManagerDto, Employee>();
            //CreateMap<Employee, EmployeeDto>()
            //    .ForMember(
            //        dest => dest.Id,
            //        opt => opt.MapFrom(
            //            src => src.EmployeeId
            //            ))
            //    .ReverseMap();

            //CreateMap<Employee, ManagerDto>()
            //    .ForMember(
            //        dest => dest.EmployeesCount, 
            //        opt => opt.MapFrom(
            //            src => src.ManagedEmployees.Count
            //            ))
            //    .ReverseMap();
        }
    }
}
