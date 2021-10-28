using System.Threading.Tasks;
using EmployeeAccounting.UI.Model;

namespace EmployeeAccounting.Services.Interfaces
{
    public interface IEmployeeService<T> : ICoreService, ICoreCrud<T> where T : BaseModel
    {
        Task<T> AddEmployeeAsync(int id, int courseId);
        Task<T> RemoveEmployeeAsync(int id, int courseId);
    }
}