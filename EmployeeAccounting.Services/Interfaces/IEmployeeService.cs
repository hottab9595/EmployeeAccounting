using EmployeeAccounting.UI.Model;

namespace EmployeeAccounting.Services.Interfaces
{
    public interface IEmployeeService<T> : ICoreService, ICoreCrud<T> where T : BaseModel
    {

    }
}