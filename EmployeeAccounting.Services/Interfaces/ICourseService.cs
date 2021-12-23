using EmployeeAccounting.Services.Models;

namespace EmployeeAccounting.Services.Interfaces
{
    public interface ICourseService<T> : ICoreService, ICoreCrud<T> where T : BaseModel
    {
    }
}