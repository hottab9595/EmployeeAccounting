using AutoMapper;
using UIModel = EmployeeAccounting.UI.Model;
using DbModel = EmployeeAccounting.Db.Model;

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
        }
    }
}