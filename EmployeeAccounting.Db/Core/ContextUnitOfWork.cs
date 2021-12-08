using EmployeeAccounting.Db.Interfaces;
using EmployeeAccounting.Db.Model;
using System;
using System.Threading.Tasks;

namespace EmployeeAccounting.Db.Core
{
    public class ContextUnitOfWork : IUnitOfWork
    {
        private Context _db { get; set; }
        private readonly IServiceProvider _serviceProvider;

        public ContextUnitOfWork(Context context, IServiceProvider serviceProvider)
        {
            this._db = context;
            this._serviceProvider = serviceProvider;
        }

        public IRepository<Employee> Employees => GetRepository<Employee>();
        public IRepository<Department> Departments => GetRepository<Department>();
        public IRepository<Course> Courses => GetRepository<Course>();
        public IRepository<CourseEmployee> CourseEmployees => GetRepository<CourseEmployee>();

        public async Task SaveAsync() => await _db.SaveChangesAsync();

        private IRepository<T> GetRepository<T>() where T : class
        {
            return (IRepository<T>)_serviceProvider.GetService(typeof(IRepository<T>));
        }
    }
}