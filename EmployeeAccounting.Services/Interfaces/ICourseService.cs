using System.Threading.Tasks;
using EmployeeAccounting.UI.Model;

namespace EmployeeAccounting.Services.Interfaces
{
    public interface ICourseService<T> : ICoreService, ICoreCrud<T> where T : BaseModel
    {
        Task<T> AddEmployeeAsync(int id, int employeeId);
        Task<T> RemoveEmployeeAsync(int id, int employeeId);
    }
}