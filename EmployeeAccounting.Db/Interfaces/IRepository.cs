using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeAccounting.Db.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IQueryable<T>> GetAsync();
        IQueryable<T> Get();
        Task<T> GetAsync(int id);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<T> UpdateAsync(int id);
        void DeleteAsync();
        void DeleteAsync(T entity);
        void DeleteAsync(int id);
    }
}