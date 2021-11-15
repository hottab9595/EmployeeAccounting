using System.Threading.Tasks;
using EmployeeAccounting.Db.Model;

namespace EmployeeAccounting.Db.Interfaces
{
    public interface IUnitOfWork 
    {
        IRepository<Employee> Employees { get;}
        IRepository<Department> Departments { get; }
        Task SaveAsync();
    }
}