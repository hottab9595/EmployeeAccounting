using AutoMapper;
using DbModel = EmployeeAccounting.Db.Model;
using UIModel = EmployeeAccounting.UI.Model;

namespace EmployeeAccounting.Services
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UIModel.Employee, DbModel.Employee>();
            CreateMap<DbModel.Employee, UIModel.Employee>();

            CreateMap<UIModel.Department, DbModel.Department>();
            CreateMap<DbModel.Department, UIModel.Department>();

            CreateMap<UIModel.Course, DbModel.Course>();
            CreateMap<DbModel.Course, UIModel.Course>();
        }
    }
}