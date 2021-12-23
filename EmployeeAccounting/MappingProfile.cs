using AutoMapper;
using ServiceModel = EmployeeAccounting.Services.Models;
using EmployeeAccounting.Model;

namespace EmployeeAccounting
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Employee, ServiceModel.Employee>();
            CreateMap<ServiceModel.Employee, Employee>();

            CreateMap<Department, ServiceModel.Department>();
            CreateMap<ServiceModel.Department, Department>();

            CreateMap<Course, ServiceModel.Course>();
            CreateMap<ServiceModel.Course, Course>();

            CreateMap<Membership, ServiceModel.Membership>();
            CreateMap<ServiceModel.Membership, Membership>();
        }
    }
}