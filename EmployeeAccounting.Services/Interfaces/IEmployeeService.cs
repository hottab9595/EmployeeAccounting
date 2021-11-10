using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeeAccounting.UI.Model;

namespace EmployeeAccounting.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetEmployeesAsync();
        Task<Employee> GetEmployeeAsync(int id);

        Task<Employee> AddNewEmployeeAsync(Employee employee);
        Task<Employee> UpdateEmployeeAsync(int id, Employee employee);
        Task DeleteAsync(int id);
        Task DeleteAsync(Employee employee);
        Task FullDeleteAsync(int id);
    }
}