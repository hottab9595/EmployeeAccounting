using EmployeeAccounting.Services.Models;

namespace EmployeeAccounting.Services.Interfaces
{
    public interface IEmployeeService<T> : ICoreService, ICoreCrud<T> where T : BaseModel
    {
    }
}