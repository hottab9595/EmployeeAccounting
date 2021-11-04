using System;
using System.Threading.Tasks;
using EmployeeAccounting.Db.Model;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAccounting.Db.Interfaces
{
    public interface IUnitOfWork 
    {
        IRepository<Employee> Employees { get;}
        IRepository<Department> Departments { get; }
        Task SaveAsync();
    }
}