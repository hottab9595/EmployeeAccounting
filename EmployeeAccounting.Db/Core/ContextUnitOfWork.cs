using System;
using System.Threading.Tasks;
using EmployeeAccounting.Db.Interfaces;
using EmployeeAccounting.Db.Model;

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

        public async Task SaveAsync() => await _db.SaveChangesAsync();

        private IRepository<T> GetRepository<T>() where T : class
        {
            return (IRepository<T>)_serviceProvider.GetService(typeof(IRepository<T>));
        }
    }
}