using AutoMapper;
using EmployeeAccounting.Services.Models;
using DbModel = EmployeeAccounting.Db.Model;

namespace EmployeeAccounting.Services
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Employee, DbModel.Employee>();
            CreateMap<DbModel.Employee, Employee>();

            CreateMap<Department, DbModel.Department>();
            CreateMap<DbModel.Department, Department>();

            CreateMap<Course, DbModel.Course>();
            CreateMap<DbModel.Course, Course>();

            CreateMap<Membership, DbModel.CourseEmployee>();
            CreateMap<DbModel.CourseEmployee, Membership>();
        }
    }
}