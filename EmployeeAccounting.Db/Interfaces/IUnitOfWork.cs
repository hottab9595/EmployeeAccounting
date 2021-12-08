using EmployeeAccounting.Db.Model;
using System.Threading.Tasks;

namespace EmployeeAccounting.Db.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<Employee> Employees { get; }
        IRepository<Department> Departments { get; }
        IRepository<Course> Courses { get; }
        IRepository<CourseEmployee> CourseEmployees { get; }
        Task SaveAsync();
    }
}