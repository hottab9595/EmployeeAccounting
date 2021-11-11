using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeeAccounting.UI.Model;

namespace EmployeeAccounting.Services.Interfaces
{
    public interface IDepartmentService
    {
        Task<IEnumerable<Department>> GetDepartmentsAsync();
        Task<Department> GetDepartmentAsync(int id);
        Task<Department> AddNewDepartmentAsync(Department department);
        Task<Department> UpdateDepartmentAsync(int id, Department department);
        Task DeleteAsync(int id);
        Task DeleteAsync(Department department);
        Task FullDeleteAsync(int id);
    }
}