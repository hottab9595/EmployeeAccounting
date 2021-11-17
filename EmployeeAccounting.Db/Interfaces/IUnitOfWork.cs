using EmployeeAccounting.Db.Model;
using System.Threading.Tasks;

namespace EmployeeAccounting.Db.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<Employee> Employees { get; }
        IRepository<Department> Departments { get; }
        Task SaveAsync();
    }
}