using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeAccounting.Db.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAsync();
        Task<T> GetAsync(int id);
        void AddAsync(T entity);
        void UpdateAsync(T entity);
        void UpdateAsync(int id);
        void DeleteAsync();
        void DeleteAsync(T entity);
        void DeleteAsync(int id);
    }
}